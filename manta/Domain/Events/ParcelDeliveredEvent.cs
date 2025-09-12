using manta.Domain.Entities;

namespace manta.Domain.Events;

public class ParcelDeliveredEvent : DomainEvent
{
    public Parcel Parcel { get; }
    public DeliveryPoint DeliveryPoint { get; }
    public ParcelDeliveredEvent(Parcel parcel, DeliveryPoint deliveryPoint)
    {
        Parcel = parcel;
        DeliveryPoint = deliveryPoint;
    }
}