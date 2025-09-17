using manta.Domain.Events;

namespace manta.Infrastructure.EventDispatcher;

public static class DomainEvents
{
    private static readonly Dictionary<Type, List<Delegate>> _handlers = new Dictionary<Type, List<Delegate>>();

    public static void Register<T>(Action<T> handler) where T : IDomainEvent
    {
        if (!_handlers.ContainsKey(typeof(T))) 
            _handlers.Add(typeof(T), new List<Delegate>());
        _handlers[typeof(T)].Add(handler);
    }

    public static void Raise<T>(T domainEvent) where T : IDomainEvent
    {
        foreach (var handler in _handlers[typeof(T)].OfType<Action<T>>())
        {
            handler(domainEvent);
        }
    }
}