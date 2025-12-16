using Manta.Domain.Events;
using Manta.Domain.Services;
using Manta.Application.Common.Events;

namespace Manta.Application.Handlers;

public class ParcelAddedHandler : IHandle<ParcelAddedToDeliveryPointEvent>
{
    private readonly ParcelStatusService _statusService;

    public ParcelAddedHandler(ParcelStatusService statusService)
    {
        _statusService = statusService;
    }

    public Task Handle(ParcelAddedToDeliveryPointEvent e)
    {
        return Task.CompletedTask;
    }
}