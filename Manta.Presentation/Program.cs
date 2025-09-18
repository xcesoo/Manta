using Manta.Application.Events;
using Manta.Application.Services;
using Manta.Domain.Services;
using Manta.Domain.Entities;
using Manta.Domain.Enums;
using Manta.Domain.Events;
using Manta.Domain.Services;

namespace Manta.Presentation;

public static class Manta
{
    public static void Main(string[] args)
    {
        ParcelStatusService statusService = new ParcelStatusService();
        EventsLoader.LoadAllEvents(statusService);
        ParcelDeliveryService deliveryService = new ParcelDeliveryService(statusService);
        var dp = DeliveryPointFactory.Create(1, "Kyiv");
        var parcel1 = ParcelFactory.Create(1, 1, SystemUser.Instance);
        var parcel2 = ParcelFactory.Create(2, 2, SystemUser.Instance);
        deliveryService.AcceptedAtDeliveryPoint(dp,parcel1,SystemUser.Instance);
        deliveryService.AcceptedAtDeliveryPoint(dp,parcel2,SystemUser.Instance);
        deliveryService.DeliverParcel(dp,parcel1,SystemUser.Instance);
    }
}