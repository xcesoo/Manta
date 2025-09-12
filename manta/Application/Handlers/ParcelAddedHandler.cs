using manta.Domain.Entities;
using manta.Domain.Events;
using manta.Domain.Services;
using manta.Infrastructure.EventDispatcher;

namespace manta.Application.Handlers;

public class ParcelAddedHandler
{
    private readonly ParcelStatusService _statusService;

    public ParcelAddedHandler(ParcelStatusService statusService)
    {
        _statusService = statusService;
        DomainEvents.Register<ParcelAddedToDeliveryPointEvent>(Handle);
    }

    private void Handle(ParcelAddedToDeliveryPointEvent e)
    {
        _statusService.UpdateStatus(e.Parcel,SystemUser.Instance, e.DeliveryPoint);
    }
}