using Manta.Application.Events;
using Manta.Application.Factories;
using Manta.Application.Services;
using Manta.Domain.Services;
using Manta.Domain.Entities;

namespace Manta.Presentation;

public static class Manta
{
    public static void Main(string[] args)
    {
        //todo create failed ParcelDeliveryService events
        ParcelStatusService statusService = new ParcelStatusService(); //todo move to Application layer + commands in Application layer
        EventsLoader.LoadAllEvents(statusService); //todo move to Application layer + commands in Application layer
        ParcelDeliveryService deliveryService = new ParcelDeliveryService(statusService); //todo commands in Application layer
        var dp = DeliveryPointFactory.Create(1, "Kyiv"); //todo record options + move to public API Application layer
        var dp2 = DeliveryPointFactory.Create(2, "Lviv");
        var parcel = ParcelFactory.Create(new ParcelCreationOptions(
            Id: 1,
            DeliveryPointId: 1,
            AmountDue: 1000m,
            RecipientName: "John",
            RecipientPhoneNumber: "+380987654321",
            RecipientEmail: "popa@gmail.com",
            Weight: 10,
            CreatedBy: SystemUser.Instance));
        var parcel2 = ParcelFactory.Create(new ParcelCreationOptions(
            Id: 2,
            DeliveryPointId: 2,
            AmountDue: 1000m,
            RecipientName: "John",
            RecipientPhoneNumber: "+380987654321",
            RecipientEmail: "popa@gmail.com",
            Weight: 10,
            CreatedBy: SystemUser.Instance));
        try
        {
            deliveryService.CancelParcel(parcel, SystemUser.Instance);
            deliveryService.AcceptedAtDeliveryPoint(dp, parcel, SystemUser.Instance);
            deliveryService.AcceptedAtDeliveryPoint(dp2, parcel2, SystemUser.Instance);
            deliveryService.ReaddressParcel(dp, parcel2, SystemUser.Instance);
            deliveryService.DeliverParcel(dp2, parcel2, SystemUser.Instance);
        }
        catch (Exception e){ Console.WriteLine(e.Message);}
        // var parcel1 = ParcelFactory.Create(1, 1, SystemUser.Instance);
        // var parcel2 = ParcelFactory.Create(2, 2, SystemUser.Instance);
        // deliveryService.AcceptedAtDeliveryPoint(dp,parcel1,SystemUser.Instance);
        // deliveryService.AcceptedAtDeliveryPoint(dp,parcel2,SystemUser.Instance);
        // deliveryService.DeliverParcel(dp,parcel1,SystemUser.Instance);
    }
}