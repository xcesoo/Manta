using Manta.Domain.Entities;
using Manta.Domain.Enums;
using Manta.Domain.Interfaces;

namespace Manta.Domain.StatusRules;

public class ReaddressRequestedRule : IParcelStatusRule
{
    public bool ShouldApply(Parcel parcel, DeliveryPoint deliveryPoint, out EParcelStatus newStatus)
    {
        if (parcel.CurrentLocationDeliveryPointId == deliveryPoint.Id ||
            parcel.CurrentStatus.Status == EParcelStatus.Delivered)
        {
            newStatus = parcel.CurrentStatus.Status;
            return false;
        }
        newStatus = EParcelStatus.ReaddressRequested;
        return true;
    }
}