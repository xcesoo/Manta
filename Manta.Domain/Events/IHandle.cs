using Manta.Domain.Events;

namespace Manta.Domain.Events;

public interface IHandle<TEvent> where TEvent : IDomainEvent
{
    Task Handle(TEvent domainEvent);
}