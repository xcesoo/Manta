using Manta.Domain.Events;
using Manta.Domain.Services;
using Manta.Infrastructure.EventDispatcher;

namespace Manta.Application.Handlers;

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
        
    }
}