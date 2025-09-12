using manta.Application.Handlers;
using manta.Domain.Services;

namespace manta.Domain.Events;

public static class EventsLoader
{ 
    public static void LoadAllEvents(ParcelStatusService parcelStatusService)
    {
        new ParcelAddedHandler(parcelStatusService);
        new ParcelDeliveredHandler(parcelStatusService);
    }
}