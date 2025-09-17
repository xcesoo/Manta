using manta.Domain.Entities;
using manta.Domain.Enums;
using manta.Domain.Interfaces;

namespace manta.Domain.StatusRules;

public class DeliveredRule : IParcelStatusRule
{
    public bool ShouldApply(Parcel parcel, DeliveryPoint deliveryPoint, out EParcelStatus newStatus)
    {
        if (parcel.CurrentStatus.Status == EParcelStatus.ReadyForPickup && parcel.DeliveryPointId == deliveryPoint.Id)
        {
            newStatus = EParcelStatus.Delivered;
            return true; 
        }
        newStatus = parcel.CurrentStatus.Status;
        return false;
    }
}