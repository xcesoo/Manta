using System.Reflection;
using System.Text.Json;
using Manta.Application.Interfaces;
using Manta.Contracts;
using Manta.Infrastructure.Persistence;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Manta.Infrastructure.Messaging;

public class JanitorWorker : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<JanitorWorker> _logger;
    private readonly TimeSpan _checkInterval = TimeSpan.FromSeconds(30); 
    private readonly TimeSpan _messageTimeout = TimeSpan.FromSeconds(60);

    public JanitorWorker(IServiceProvider serviceProvider, ILogger<JanitorWorker> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("[JANITOR] started");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await PerformCleanupAndRescueAsync(stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[JANITOR] Error");
            }

            await Task.Delay(_checkInterval, stoppingToken);
        }
    }

    private async Task PerformCleanupAndRescueAsync(CancellationToken ct)
    {
        using var scope = _serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<MantaDbContext>();
        var publishEndpoint = scope.ServiceProvider.GetRequiredService<IPublishEndpoint>();
        
        var deletedCount = await dbContext.OutboxMessages
            .Where(outbox => dbContext.ProcessedLogs.Any(log => log.MessageId == outbox.MessageId))
            .ExecuteDeleteAsync(ct);

        if (deletedCount > 0)
        {
            _logger.LogInformation($"[JANITOR] Cleaned {deletedCount}");
        }
        
        var thresholdDate = DateTime.UtcNow.Subtract(_messageTimeout);

        var stuckMessages = await dbContext.OutboxMessages
            // Правильное условие:
            .Where(o => o.CreatedAt < thresholdDate) // Сообщение В ЛЮБОМ СЛУЧАЕ должно быть старше 60 сек
            .Where(o => o.SentAt == null || o.SentAt < thresholdDate)
            .Where(o => !dbContext.ProcessedLogs.Any(log => log.MessageId == o.MessageId))
            .OrderBy(o => o.CreatedAt)
            .Take(1000)
            .ToListAsync(ct);

        if (stuckMessages.Any())
        {
            _logger.LogWarning($"[JANITOR] {stuckMessages.Count} lost messages were found. Resending...");

            var groupedStuckMessages = stuckMessages.GroupBy(x => x.MessageType);

            foreach (var group in groupedStuckMessages)
            {
                var targetType = Type.GetType(group.Key);
                if (targetType == null) continue;

                var listType = typeof(List<>).MakeGenericType(targetType);
                var typedList = (System.Collections.IList)Activator.CreateInstance(listType)!;

                foreach (var stuckMsg in group)
                {
                    var deserializedMessage = JsonSerializer.Deserialize(stuckMsg.Payload, targetType);
                    if (deserializedMessage != null)
                    {
                        typedList.Add(deserializedMessage);
                    }
                    // ИСПРАВЛЕНИЕ: Обновляем время отправки при спасении
                    stuckMsg.SentAt = DateTime.UtcNow; 
                }

                if (typedList.Count > 0)
                {
                    var genericMethod = typeof(OutboxBatchProcessor)
                        .GetMethod("PublishTypedBatchAsync", BindingFlags.NonPublic | BindingFlags.Static)! 
                        .MakeGenericMethod(targetType);

                    var task = (Task)genericMethod.Invoke(null, new object[] { publishEndpoint, typedList, ct })!;
                    await task;
                }
            }
            await dbContext.SaveChangesAsync(ct);
        }
    }
}