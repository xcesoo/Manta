using Manta.Domain.Entities;
using Manta.Domain.Enums;
using Manta.Domain.Interfaces;
using Manta.Domain.ValueObjects;

namespace Manta.Domain.StatusRules;

public class ReadyForPickupRule : IParcelStatusRule
{
    public RuleResult ShouldApply(Parcel parcel, DeliveryPoint deliveryPoint)
    {
        if (parcel.DeliveryPointId == deliveryPoint.Id)
        {
            return RuleResult.Ok(EParcelStatus.ReadyForPickup);
        }
        return RuleResult.Failed(ERuleResultError.Unknown, "Unknown");
    }
}