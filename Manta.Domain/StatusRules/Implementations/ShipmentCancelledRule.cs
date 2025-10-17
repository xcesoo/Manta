using Manta.Domain.Enums;
using Manta.Domain.StatusRules.Interfaces;
using Manta.Domain.ValueObjects;

namespace Manta.Domain.StatusRules.Implementations;

public class ShipmentCancelledRule : IParcelStatusRule
{
    public RuleResult ShouldApply(RuleContext context) => context.Parcel.CurrentStatus.Status switch
        {
            EParcelStatus.Delivered => 
                RuleResult.Failed(
                    ERuleResultError.WrongParcelStatus, 
                    "Cannot cancel a delivered parcel"),
            
            EParcelStatus.ReturnRequested or 
                EParcelStatus.InReturnTransit or 
                EParcelStatus.Returned or 
                EParcelStatus.ShipmentCancelled => 
                RuleResult.Failed(
                ERuleResultError.WrongParcelStatus, 
                "Parcel is currently involved in a return process"),
            
            _ => RuleResult.Ok(EParcelStatus.ShipmentCancelled)
        };

}