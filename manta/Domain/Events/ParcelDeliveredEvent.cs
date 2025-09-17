using manta.Domain.Entities;

namespace manta.Domain.Events;

public record ParcelDeliveredEvent
    (Parcel Parcel, DeliveryPoint DeliveryPoint, User ChangedBy) : DomainEvent;