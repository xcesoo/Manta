using Manta.Application.Interfaces;
using Manta.Application.Services;
using Manta.Contracts.Messages;
using Manta.Domain.Exceptions;
using Manta.Domain.Interfaces;
using Manta.Domain.StatusRules.Interfaces;
using Manta.Domain.ValueObjects;
using MassTransit;
using MediatR;

namespace Manta.Application.Commands.Parcel;

public class AcceptParcelAtDeliveryPointCommandHandler(ICurrentUserService currentUser, 
    IIntegrationMessageQueue queue, IParcelRepository parcelRepository, 
    IDeliveryPointRepository deliveryPointRepository) : IRequestHandler<AcceptParcelAtDeliveryPointCommand, Guid>
{
    private readonly ICurrentUserService _currentUser = currentUser;
    private readonly IIntegrationMessageQueue _queue = queue;
    private readonly IParcelRepository _parcelRepository = parcelRepository;
    private readonly IDeliveryPointRepository _deliveryPointRepository= deliveryPointRepository;
    
    
    public async Task<Guid> Handle(AcceptParcelAtDeliveryPointCommand request, CancellationToken cancellationToken)
    {
        var dp = await _deliveryPointRepository.GetByIdAsync(request.DeliveryPointId, cancellationToken)
                 ?? throw new ArgumentException("delivery_point_not_found");

        int activeCount = await _parcelRepository.GetActiveParcelsCountAtPointAsync(request.DeliveryPointId, cancellationToken);

        if (activeCount >= dp.Capacity)
        {
            throw new InvalidOperationException("slot_unavailable");
        }
        var message = new AcceptParcelAtDeliveryPointMessage(
            MessageId: NewId.NextGuid(),
            ParcelId: request.ParcelId,
            DeliveryPointId: request.DeliveryPointId,
            UserId: _currentUser.UserId);
        await _queue.EnqueueAsync(message, message.MessageId, cancellationToken);
        return request.ParcelId;
    }
}