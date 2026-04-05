using Manta.Application.Handlers;
using Manta.Contracts.Messages;
using Manta.Infrastructure.Entities;
using Manta.Infrastructure.Persistence;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace Manta.Infrastructure.Consumers;

public class AcceptParcelAtDeliveryPointConsumer(AcceptParcelAtDeliveryPointHandler handler, MantaDbContext dbContext) : IConsumer<AcceptParcelAtDeliveryPointMessage>
{
    private readonly AcceptParcelAtDeliveryPointHandler _handler = handler;
    private readonly MantaDbContext _dbContext = dbContext;

    public async Task Consume(ConsumeContext<AcceptParcelAtDeliveryPointMessage> context)
    {
        bool alreadyProcessed = await _dbContext.ProcessedLogs
            .AnyAsync(l => l.MessageId == context.Message.MessageId, context.CancellationToken);
            
        if (alreadyProcessed) return;

        await _handler.HandleAsync(context.Message, context.CancellationToken);

        _dbContext.ProcessedLogs.Add(new ProcessedLog 
        { 
            MessageId = context.Message.MessageId, 
            ProcessedAt = DateTime.UtcNow 
        });

        await _dbContext.SaveChangesAsync(context.CancellationToken);
    }
}