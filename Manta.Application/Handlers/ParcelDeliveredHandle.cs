using Manta.Domain.Events;
using Manta.Domain.Services;
using Manta.Application.Common.Events;

namespace Manta.Application.Handlers;

public class ParcelDeliveredHandler : IHandle<ParcelDeliveredEvent>
{
    private readonly ParcelStatusService _statusService;
    
    public ParcelDeliveredHandler(ParcelStatusService statusService)
    {
        _statusService = statusService;
    }
    
    public Task Handle(ParcelDeliveredEvent e)
    {
        return Task.CompletedTask;
    }
}