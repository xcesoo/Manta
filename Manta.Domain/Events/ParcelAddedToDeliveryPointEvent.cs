using Manta.Domain.Entities;

namespace Manta.Domain.Events;

public record ParcelAddedToDeliveryPointEvent
    (Parcel Parcel, DeliveryPoint DeliveryPoint, User ChangedBy) : DomainEvent;