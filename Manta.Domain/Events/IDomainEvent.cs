using Manta.Domain.Entities;

namespace Manta.Domain.Events;

public interface IDomainEvent
{
    DateTime OccurredOn { get; }
    User ChangedBy { get; }
}