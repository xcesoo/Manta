using Manta.Domain.Entities;
using Manta.Domain.Events;
using Manta.Domain.Services;
using Manta.Infrastructure.EventDispatcher;

namespace Manta.Application.Handlers;

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
        
    }
}