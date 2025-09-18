using Manta.Application.Handlers;
using Manta.Domain.Services;

namespace Manta.Application.Events;

public static class EventsLoader
{
    private static List<object> _handlers = new();
    public static void LoadAllEvents(ParcelStatusService parcelStatusService)
    {
        _handlers.Add(new ParcelAddedHandler(parcelStatusService));
        _handlers.Add(new ParcelDeliveredHandler(parcelStatusService));
    }
}