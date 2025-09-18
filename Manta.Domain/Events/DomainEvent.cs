using Manta.Domain.Entities;

namespace Manta.Domain.Events;

public abstract record DomainEvent : IDomainEvent
{
    public DateTime OccurredOn { get; } = DateTime.UtcNow;
    public User ChangedBy { get; } = SystemUser.Instance;
}