using Manta.Domain.Entities;
using Manta.Domain.Enums;
using Manta.Domain.Interfaces;
using Manta.Domain.ValueObjects;

namespace Manta.Domain.StatusRules;

public class ReaddressRequestedRule : IParcelStatusRule
{
    public RuleResult ShouldApply(Parcel parcel, DeliveryPoint deliveryPoint)
    {
        if (parcel.CurrentLocationDeliveryPointId == deliveryPoint.Id)
            return RuleResult.Failed(ERuleResultError.ParcelAlreadyRightLocation, "Parcel is already in the right location");
        if (parcel.CurrentStatus.Status == EParcelStatus.Delivered)
            return RuleResult.Failed(ERuleResultError.ParcelAlreadyDelivered, "Parcel is already delivered");
        
        return RuleResult.Ok(EParcelStatus.ReaddressRequested);
    }
}