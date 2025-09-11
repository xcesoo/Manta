using manta.Domain.Entities;
using manta.Domain.Enums;
using manta.Domain.Interfaces;

namespace manta.Domain.StatusRules;

public class ReadyForPickupRule : IParcelStatusRule
{
    public bool ShouldApply(Parcel parcel, DeliveryPoint deliveryPoint, out EParcelStatus newStatus)
    {
        if (parcel.DeliveryPointId == deliveryPoint.Id)
        {
            newStatus = EParcelStatus.ReadyForPickup;
            return true;
        }
        newStatus = parcel.CurrentStatus.Status;
        return false;
    }
}