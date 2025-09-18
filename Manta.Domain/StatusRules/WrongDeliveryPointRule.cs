using Manta.Domain.Entities;
using Manta.Domain.Enums;
using Manta.Domain.Interfaces;

namespace Manta.Domain.StatusRules;

public class WrongDeliveryPointRule : IParcelStatusRule
{
    public bool ShouldApply(Parcel parcel, DeliveryPoint deliveryPoint, out EParcelStatus newStatus)
    {
        if (parcel.DeliveryPointId != deliveryPoint.Id)
        {
            newStatus = EParcelStatus.WrongLocation;
            return true; 
        }
        newStatus = parcel.CurrentStatus.Status;
        return false; 
    }
}