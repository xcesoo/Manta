using Manta.Domain.Entities;
using Manta.Domain.Enums;
using Manta.Domain.Interfaces;

namespace Manta.Domain.StatusRules;

public class DeliveredRule : IParcelStatusRule
{
    public bool ShouldApply(Parcel parcel, DeliveryPoint deliveryPoint, out EParcelStatus newStatus)
    {
        if (parcel.CurrentLocationDeliveryPointId != deliveryPoint.Id)
            throw new ArgumentException("Parcel does not exist in this delivery point", nameof(parcel));
            
        if (parcel.CurrentStatus.Status == EParcelStatus.ReadyForPickup && parcel.DeliveryPointId == deliveryPoint.Id)
        {
            //todo check paid and other
            newStatus = EParcelStatus.Delivered;
            return true; 
        }
        newStatus = parcel.CurrentStatus.Status;
        return false;
    }
}