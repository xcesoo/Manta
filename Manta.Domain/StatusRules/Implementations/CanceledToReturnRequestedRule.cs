using Manta.Domain.Entities;
using Manta.Domain.Enums;
using Manta.Domain.Exceptions;
using Manta.Domain.StatusRules.Interfaces;
using Manta.Domain.ValueObjects;

namespace Manta.Domain.StatusRules;

public class CanceledToReturnRequestedRule : IParcelStatusRule
{
    public RuleResult ShouldApply(RuleContext context)
    {
        return context.Parcel.CurrentStatus.Status switch
        {
            EParcelStatus.ShipmentCancelled =>
                RuleResult.Ok(EParcelStatus.ReturnRequested),
            
            _ => RuleResult.Failed(
                ERuleResultError.WrongParcelStatus, 
                $"Unable to return request. Parcel -> {context.Parcel.CurrentStatus.Status}")
        };
    }
}