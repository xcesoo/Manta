using MediatR;
using Microsoft.Extensions.Logging;

namespace Manta.Application.Events.Parcel;

public class ParcelDeliveredHandler : INotificationHandler<ParcelDeliveredEvent>
{
    private readonly ILogger<ParcelDeliveredHandler> _logger;
    public ParcelDeliveredHandler(ILogger<ParcelDeliveredHandler> logger)
    {
        _logger = logger;
    }
    public Task Handle(ParcelDeliveredEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Parcel with id {notification.ParcelId} Delivered");
        return Task.CompletedTask;
    }
}