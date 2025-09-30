using Manta.Domain.Entities;
using Manta.Domain.Enums;
using Manta.Domain.StatusRules.Interfaces;
using Manta.Domain.ValueObjects;

namespace Manta.Domain.StatusRules;

public class ReadyForPickupRule : IParcelStatusRule
{
    public RuleResult ShouldApply(RuleContext context)
    {
        if (context.DeliveryPoint is null)
            return RuleResult.Failed(ERuleResultError.ArguementInvalid, "DeliveryPoint is null");
        
        return context.Parcel.CurrentStatus.Status switch
        {
            EParcelStatus.Processing or
                EParcelStatus.InTransit or 
                EParcelStatus.WrongLocation when
                context.Parcel.DeliveryPointId == context.DeliveryPoint.Id =>
                RuleResult.Ok(EParcelStatus.ReadyForPickup),
            
            _ => RuleResult.Failed(
                ERuleResultError.Unknown, 
                "Failed to pick up a parcel")
        };
    }
}