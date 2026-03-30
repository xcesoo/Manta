using System.Collections.Concurrent;
using System.Reflection;
using System.Text.Json;
using Manta.Application.Interfaces;
using Manta.Domain.Entities;
using Manta.Infrastructure.Entities;
using Manta.Infrastructure.Persistence;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Manta.Infrastructure.Messaging;

public class OutboxBatchProcessor : BackgroundService
{
    private readonly IIntegrationMessageQueue _queue;
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<OutboxBatchProcessor> _logger;
    private const int MaxBatchSize = 500;
    private static readonly ConcurrentDictionary<Type, MethodInfo> MethodCache = new();

    public OutboxBatchProcessor(
        IIntegrationMessageQueue queue,
        IServiceProvider serviceProvider,
        ILogger<OutboxBatchProcessor> logger)
    {
        _queue = queue;
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken ct)
    {
        while (!ct.IsCancellationRequested)
        {
            var batch = new List<(object Message, Guid MessageId)>();

            try
            {
                var firstItem = await _queue.ReadAsync(ct);
                batch.Add(firstItem);

                while (batch.Count < MaxBatchSize && _queue.TryRead(out var nextItem))
                {
                    batch.Add(nextItem);
                }
                await ProcessBatchAsync(batch, ct);
            }
            catch (OperationCanceledException)
            {
                break;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[OUTBOX] Batch processing failed, retrying in 2s");
                await Task.Delay(2000, ct);
            }
        }
    }

    private async Task ProcessBatchAsync(
        List<(object Message, Guid MessageId)> batch, 
        CancellationToken ct)
    {
        using var scope = _serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<MantaDbContext>();
        var publishEndpoint = scope.ServiceProvider.GetRequiredService<IPublishEndpoint>();

        var outboxMessages = batch.Select(x => new OutboxMessage
        {
            MessageId = x.MessageId,
            MessageType = x.Message.GetType().AssemblyQualifiedName!,
            Payload = JsonSerializer.Serialize(x.Message, x.Message.GetType()),
            CreatedAt = DateTime.UtcNow,
        }).ToList();

        dbContext.OutboxMessages.AddRange(outboxMessages);
        await dbContext.SaveChangesAsync(ct);
        
        var groupedMessages = batch.GroupBy(x => x.Message.GetType());
        foreach (var group in groupedMessages)
        {
            var messageType = group.Key;
            var messagesList = group.Select(x => x.Message).ToList();

            var genericMethod = MethodCache.GetOrAdd(messageType, t =>
                typeof(OutboxBatchProcessor)
                    .GetMethod(nameof(PublishTypedBatchAsync), BindingFlags.NonPublic | BindingFlags.Static)! 
                    .MakeGenericMethod(t));
            var task = (Task)genericMethod.Invoke(null, new object[] { publishEndpoint, messagesList, ct })!;
            await task;
        }
    }
    internal static Task PublishTypedBatchAsync<T>(IPublishEndpoint endpoint, IEnumerable<object> messages, CancellationToken ct) where T : class
    {
        var typedMessages = messages.Cast<T>().ToList();

        return endpoint.PublishBatch(typedMessages, context =>
        {
            context.Durable = false;
        }, ct);
    }
}