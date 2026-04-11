using Manta.Application.Handlers;
using Manta.Contracts.Messages;
using Manta.Infrastructure.Entities;
using Manta.Infrastructure.Hubs;
using Manta.Infrastructure.Persistence;
using MassTransit;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace Manta.Infrastructure.Consumers;

public class AcceptParcelAtDeliveryPointConsumer(AcceptParcelAtDeliveryPointHandler handler, MantaDbContext dbContext, 
    IHubContext<ParcelNotificationHub> hubContext) : IConsumer<AcceptParcelAtDeliveryPointMessage>
{
    private readonly AcceptParcelAtDeliveryPointHandler _handler = handler;
    private readonly MantaDbContext _dbContext = dbContext;
    private readonly IHubContext<ParcelNotificationHub> _hubContext = hubContext;

    public async Task Consume(ConsumeContext<AcceptParcelAtDeliveryPointMessage> context)
    {
        bool alreadyProcessed = await _dbContext.ProcessedLogs
            .AnyAsync(l => l.MessageId == context.Message.MessageId, context.CancellationToken);
            
        if (alreadyProcessed) return;
        
        string parcelRoomId = context.Message.ParcelId.ToString();

        try
        {
            await _handler.HandleAsync(context.Message, context.CancellationToken);
            await hubContext.Clients.Group(parcelRoomId)
                .SendAsync("ParcelAccepted", context.Message.MessageId, context.CancellationToken);
        }
        catch (Exception ex)
        {
            await  _hubContext.Clients.Group(parcelRoomId)
                .SendAsync("ParcelRejected", context.Message.MessageId, ex.Message, context.CancellationToken);
        }

        _dbContext.ProcessedLogs.Add(new ProcessedLog 
        { 
            MessageId = context.Message.MessageId, 
            ProcessedAt = DateTime.UtcNow 
        });

        await _dbContext.SaveChangesAsync(context.CancellationToken);
    }
}