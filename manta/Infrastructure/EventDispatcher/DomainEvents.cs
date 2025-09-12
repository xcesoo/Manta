using manta.Domain.Events;

namespace manta.Infrastructure.EventDispatcher;

public static class DomainEvents
{
    private static readonly List<Delegate> _handlers = new List<Delegate>(); 

    public static void Register<T>(Action<T> handler) where T : IDomainEvent
    {
        _handlers.Add(handler); 
    }

    public static void Raise<T>(T domainEvent) where T : IDomainEvent
    {
        foreach (var handler in _handlers.OfType<Action<T>>())
        {
            handler(domainEvent);
        }
    }
}