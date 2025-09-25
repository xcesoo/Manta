using Manta.Domain.Entities;
using Manta.Domain.Enums;
using Manta.Domain.Interfaces;
using Manta.Domain.ValueObjects;

namespace Manta.Domain.StatusRules;

public class DeliveredRule : IParcelStatusRule
{
    public RuleResult ShouldApply(Parcel parcel, DeliveryPoint deliveryPoint)
    {
        if (parcel.CurrentLocationDeliveryPointId != deliveryPoint.Id)
            return RuleResult.Failed("PAD", "Parcel is not in the right location to be delivered");
            
        if (parcel.CurrentStatus.Status == EParcelStatus.ReadyForPickup && parcel.DeliveryPointId == deliveryPoint.Id)
        {
            //todo check paid and other
            return RuleResult.Ok(EParcelStatus.Delivered);
        }
        return RuleResult.Failed("PAD", "Parcel is not in the right status");
    }
}