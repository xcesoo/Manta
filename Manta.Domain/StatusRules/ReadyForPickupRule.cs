using Manta.Domain.Entities;
using Manta.Domain.Enums;
using Manta.Domain.Interfaces;
using Manta.Domain.ValueObjects;

namespace Manta.Domain.StatusRules;

public class ReadyForPickupRule : IParcelStatusRule
{
    public RuleResult ShouldApply(Parcel parcel, DeliveryPoint deliveryPoint)
    {
        return parcel.CurrentStatus.Status switch
        {
            EParcelStatus.Processing or
                EParcelStatus.InTransit when
                parcel.DeliveryPointId == deliveryPoint.Id =>
                RuleResult.Ok(EParcelStatus.ReadyForPickup),
            
            _ => RuleResult.Failed(
                ERuleResultError.Unknown, 
                "Failed to pick up a parcel")
        };
    }
}