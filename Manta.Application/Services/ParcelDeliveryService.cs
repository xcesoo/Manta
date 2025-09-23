using Manta.Domain.Entities;
using Manta.Domain.Events;
using Manta.Domain.Services;
using Manta.Domain.StatusRules;
using Manta.Infrastructure.EventDispatcher;

namespace Manta.Application.Services;

public class ParcelDeliveryService
{
    private readonly ParcelStatusService _statusService;
    public ParcelDeliveryService(ParcelStatusService statusService)
    {
        _statusService = statusService;
    }

    public void DeliverParcel(DeliveryPoint deliveryPoint, Parcel parcel, User changeBy)
    {
        if (_statusService.ApplyRule<DeliveredRule>(parcel, deliveryPoint, changeBy))
        {
            deliveryPoint.RemoveParcel(parcel);
            DomainEvents.Raise(new ParcelDeliveredEvent(parcel, deliveryPoint, changeBy));
        }
        else throw new ArgumentException("Failed to deliver the parcel", nameof(parcel));
    }

    public void AcceptedAtDeliveryPoint(DeliveryPoint deliveryPoint, Parcel parcel, User changedBy)
    {
        if (_statusService.UpdateStatus(parcel, deliveryPoint, changedBy))
        {
            deliveryPoint.AddParcel(parcel);
            DomainEvents.Raise(new ParcelAddedToDeliveryPointEvent(parcel, deliveryPoint, changedBy));
        }
    }
    
    public void CancelParcel(Parcel parcel, User cancelledBy)
    {
        parcel.Cancel(cancelledBy);
    }
}