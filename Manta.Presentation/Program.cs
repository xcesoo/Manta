using Manta.Application.Events;
using Manta.Application.Services;
using Manta.Domain.Services;
using Manta.Domain.Entities;

namespace Manta.Presentation;

public static class Manta
{
    public static void Main(string[] args)
    {
        ParcelStatusService statusService = new ParcelStatusService();
        EventsLoader.LoadAllEvents(statusService);
        ParcelDeliveryService deliveryService = new ParcelDeliveryService(statusService);
        var dp = DeliveryPointFactory.Create(1, "Kyiv");
        var dp2 = DeliveryPointFactory.Create(2, "Lviv");
        var parcel = ParcelFactory.Create(new ParcelCreationOptions(
            Id: 1,
            DeliveryPointId: 1,
            AmountDue: 1000,
            RecipientName: "John",
            RecipientPhoneNumber: "+380987654321",
            RecipientEmail: "popa@gmail.com",
            Weight: 10,
            CreatedBy: SystemUser.Instance));
        var parcel2 = ParcelFactory.Create(new ParcelCreationOptions(
            Id: 2,
            DeliveryPointId: 2,
            AmountDue: 1000,
            RecipientName: "John",
            RecipientPhoneNumber: "+380987654321",
            RecipientEmail: "popa@gmail.com",
            Weight: 10,
            CreatedBy: SystemUser.Instance));
        deliveryService.CancelParcel(parcel, SystemUser.Instance);
        deliveryService.AcceptedAtDeliveryPoint(dp, parcel, SystemUser.Instance);
        deliveryService.AcceptedAtDeliveryPoint(dp2, parcel2, SystemUser.Instance);
        deliveryService.ReaddressParcel(dp, parcel2, SystemUser.Instance);
        deliveryService.DeliverParcel(dp2, parcel2, SystemUser.Instance);
        // var parcel1 = ParcelFactory.Create(1, 1, SystemUser.Instance);
        // var parcel2 = ParcelFactory.Create(2, 2, SystemUser.Instance);
        // deliveryService.AcceptedAtDeliveryPoint(dp,parcel1,SystemUser.Instance);
        // deliveryService.AcceptedAtDeliveryPoint(dp,parcel2,SystemUser.Instance);
        // deliveryService.DeliverParcel(dp,parcel1,SystemUser.Instance);
    }
}