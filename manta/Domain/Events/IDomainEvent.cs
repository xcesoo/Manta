namespace manta.Domain.Events;

public interface IDomainEvent
{
    DateTime OccurredOn { get; }
}