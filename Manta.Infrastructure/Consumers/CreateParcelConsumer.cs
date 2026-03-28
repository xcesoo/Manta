using Manta.Application.Factories;
using Manta.Contracts;
using Manta.Domain.CreationOptions;
using Manta.Domain.Entities;
using Manta.Domain.Enums;
using Manta.Domain.ValueObjects;
using Manta.Infrastructure.Persistence;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Manta.Infrastructure.Consumers;

public class CreateParcelConsumer : IConsumer<Batch<CreateParcelMessage>>
{
    private readonly ILogger<CreateParcelConsumer> _logger;
    private readonly MantaDbContext _dbContext;

    public CreateParcelConsumer(MantaDbContext dbContext, ILogger<CreateParcelConsumer> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }
    
    public async Task Consume(ConsumeContext<Batch<CreateParcelMessage>> context)
    {
    _dbContext.ChangeTracker.AutoDetectChangesEnabled = false;
    
    var incomingMessageIds = context.Message.Select(x => x.Message.MessageId).ToList();
    var alreadyProcessedIds = await _dbContext.ProcessedLogs
        .Where(p => incomingMessageIds.Contains(p.MessageId))
        .Select(p => p.MessageId)
        .ToListAsync(context.CancellationToken);
    var newMessages = context.Message
        .Where(x => !alreadyProcessedIds.Contains(x.Message.MessageId))
        .ToList();
    
    var parcelsToInsert = new List<Parcel>();
    var logsToInsert = new List<ProcessedLog>();
    
    foreach (var msg in newMessages)
    {
        Enum.TryParse<EUserRole>(msg.Message.CreatedByRole, out var role);
        var options = new ParcelCreationOptions(
            Id:msg.Message.ParcelId,
            DeliveryPointId:msg.Message.DeliveryPointId,
            AmountDue:msg.Message.AmountDue,
            Weight:msg.Message.Weight,
            RecipientEmail:msg.Message.RecipientEmail,
            RecipientName:msg.Message.RecipientName,
            RecipientPhoneNumber:msg.Message.RecipientPhoneNumber,
            CreatedBy: new UserInfo(
                id: msg.Message.CreatedById,
                name: msg.Message.CreatedByName,
                email: msg.Message.CreatedByEmail,
                role: role));
        var parcel = ParcelFactory.Create(options);
        parcelsToInsert.Add(parcel);
        logsToInsert.Add(new ProcessedLog
        {
            MessageId = msg.Message.MessageId,
            ProcessedAt = DateTime.UtcNow
        });
    }
    _dbContext.Parcels.AddRange(parcelsToInsert);
    _dbContext.ProcessedLogs.AddRange(logsToInsert);
    await _dbContext.SaveChangesAsync(context.CancellationToken);
    
    //todo signalr websocket
    }
}