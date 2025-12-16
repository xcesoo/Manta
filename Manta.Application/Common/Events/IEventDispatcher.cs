using Manta.Domain.Events;

namespace Manta.Application.Common.Events;

public interface IEventDispatcher
{
    Task DispatchAsync<TEvent>(TEvent domainEvent) 
        where TEvent : IDomainEvent;
}