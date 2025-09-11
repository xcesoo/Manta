using manta.Domain.Entities;
using manta.Domain.Enums;
using manta.Domain.Interfaces;

namespace manta.Domain.StatusRules;

public class WrongDeliveryPointRule : IParcelStatusRule
{
    public bool ShouldApply(Parcel parcel, DeliveryPoint deliveryPoint, out EParcelStatus newStatus)
    {
        if (parcel.DeliveryPointId != deliveryPoint.Id)
        {
            newStatus = EParcelStatus.WrongLocation;
            return true; // правило спрацювало
        }
        newStatus = parcel.CurrentStatus.Status;
        return false; 
    }
}