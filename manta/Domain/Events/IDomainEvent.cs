using manta.Domain.Entities;

namespace manta.Domain.Events;

public interface IDomainEvent
{
    DateTime OccurredOn { get; }
    User ChangedBy { get; }
}