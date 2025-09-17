using manta.Domain.Entities;

namespace manta.Domain.Events;

public abstract record DomainEvent : IDomainEvent
{
    public DateTime OccurredOn { get; } = DateTime.UtcNow;
    public User ChangedBy { get; } = SystemUser.Instance;
}