using Manta.Application.Interfaces;
using Manta.Application.Services;
using Manta.Contracts.Messages;
using Manta.Domain.Interfaces;
using Manta.Domain.StatusRules.Interfaces;
using Manta.Domain.ValueObjects;
using MassTransit;
using MediatR;

namespace Manta.Application.Commands.Parcel;

public class AcceptParcelAtDeliveryPointCommandHandler(ICurrentUserService currentUser, IIntegrationMessageQueue queue) : IRequestHandler<AcceptParcelAtDeliveryPointCommand, Guid>
{
    private readonly ICurrentUserService _currentUser = currentUser;
    private readonly IIntegrationMessageQueue _queue = queue;
    
    
    public async Task<Guid> Handle(AcceptParcelAtDeliveryPointCommand request, CancellationToken cancellationToken)
    {
        var message = new AcceptParcelAtDeliveryPointMessage(
            MessageId: NewId.NextGuid(),
            ParcelId: request.ParcelId,
            DeliveryPointId: request.DeliveryPointId,
            UserId: _currentUser.UserId);
        await _queue.EnqueueAsync(message, message.MessageId, cancellationToken);
        return request.ParcelId;
    }
}