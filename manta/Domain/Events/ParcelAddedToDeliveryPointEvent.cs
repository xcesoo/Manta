using manta.Domain.Entities;

namespace manta.Domain.Events;

public record ParcelAddedToDeliveryPointEvent
    (Parcel Parcel, DeliveryPoint DeliveryPoint, User ChangedBy) : DomainEvent;