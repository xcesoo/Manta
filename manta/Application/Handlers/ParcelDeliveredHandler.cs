using manta.Domain.Entities;
using manta.Domain.Events;
using manta.Domain.Services;
using manta.Domain.StatusRules;
using manta.Infrastructure.EventDispatcher;

namespace manta.Application.Handlers;

public class ParcelDeliveredHandler
{
    private readonly ParcelStatusService _statusService;
    
    public ParcelDeliveredHandler(ParcelStatusService statusService)
    {
        _statusService = statusService;
        DomainEvents.Register<ParcelDeliveredEvent>(Handle);
    }
    
    private void Handle(ParcelDeliveredEvent e)
    {
        _statusService.ApplyRule<DeliveredRule>(e.Parcel,SystemUser.Instance, e.DeliveryPoint);
    }
}