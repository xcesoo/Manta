using manta.Domain.Entities;

namespace manta.Domain.Events;

public class ParcelAddedToDeliveryPointEvent : DomainEvent
{
    public Parcel Parcel { get; }
    public DeliveryPoint DeliveryPoint { get; }
    public ParcelAddedToDeliveryPointEvent(Parcel parcel, DeliveryPoint deliveryPoint)
    {
        Parcel = parcel;
        DeliveryPoint = deliveryPoint;
    }
}