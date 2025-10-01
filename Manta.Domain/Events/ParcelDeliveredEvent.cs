using Manta.Domain.Entities;

namespace Manta.Domain.Events;

public record ParcelDeliveredEvent
    (Parcel Parcel, User ChangedBy) : DomainEvent;