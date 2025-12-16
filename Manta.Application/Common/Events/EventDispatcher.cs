using Manta.Domain.Events;
using Microsoft.Extensions.DependencyInjection;

namespace Manta.Application.Common.Events;

public class EventDispatcher : IEventDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public EventDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task DispatchAsync<TEvent>(TEvent domainEvent) 
        where TEvent : IDomainEvent
    {
        var handlers = _serviceProvider.GetServices<IHandle<TEvent>>();
        foreach (var handler in handlers) await handler.Handle(domainEvent);
    }
}