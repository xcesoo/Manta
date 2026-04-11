using Microsoft.AspNetCore.SignalR;

namespace Manta.Infrastructure.Hubs;

public class ParcelNotificationHub : Hub
{
    public async Task SubscribeToParcel(Guid parcelId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, parcelId.ToString());
    }
}