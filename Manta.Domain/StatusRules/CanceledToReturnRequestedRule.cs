using Manta.Domain.Entities;
using Manta.Domain.Enums;
using Manta.Domain.Interfaces;

namespace Manta.Domain.StatusRules;

public class CanceledToReturnRequestedRule : IParcelStatusRule
{
    public bool ShouldApply(Parcel parcel, DeliveryPoint deliveryPoint, out EParcelStatus newStatus)
    {
        if (parcel.CurrentStatus.Status == EParcelStatus.ShipmentCancelled)
        {
            newStatus = EParcelStatus.ReturnRequested;
            return true;
        }
        newStatus = parcel.CurrentStatus.Status;
        return false;
    }
}