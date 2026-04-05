using Manta.Domain.Entities;

namespace Manta.Domain.Services;

public class LogisticsDomainService
{
        public void UnloadParcelFromDeliveryVehicle(DeliveryVehicle vehicle, Parcel parcel)
        {
                vehicle.UnloadParcel(parcel.Id, parcel.Weight);
                parcel.ChangeDeliveryVehicle(null);
        }
}