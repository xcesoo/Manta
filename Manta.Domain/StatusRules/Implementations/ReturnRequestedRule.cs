using Manta.Domain.Enums;
using Manta.Domain.StatusRules.Interfaces;
using Manta.Domain.ValueObjects;

namespace Manta.Domain.StatusRules.Implementations;

public sealed class ReturnRequestedRule : IParcelStatusRule
{
    public RuleResult ShouldApply(RuleContext context) => context.Parcel.CurrentStatus.Status switch
        {
            EParcelStatus.ShipmentCancelled or 
                EParcelStatus.StorageExpired =>
                RuleResult.Ok(EParcelStatus.ReturnRequested),
            
            _ => RuleResult.Failed(
                ERuleResultError.WrongParcelStatus, 
                $"Unable to return request. Parcel -> {context.Parcel.CurrentStatus.Status}")
        };
}