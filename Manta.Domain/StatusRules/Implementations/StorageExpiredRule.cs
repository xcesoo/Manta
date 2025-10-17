using Manta.Domain.Enums;
using Manta.Domain.StatusRules.Interfaces;
using Manta.Domain.ValueObjects;

namespace Manta.Domain.StatusRules.Implementations;

public sealed class StorageExpiredRule : IParcelStatusRule
{
    public RuleResult ShouldApply(RuleContext context) => context switch
    {
        {Parcel:{CurrentStatus:{Status: 
            EParcelStatus.ReadyForPickup, 
            ChangedAt: var arrivalDate}}} 
            when (DateTime.UtcNow - arrivalDate).TotalDays >=3  => 
            RuleResult.Ok(EParcelStatus.StorageExpired),
        _ => RuleResult.Failed(ERuleResultError.WrongParcelStatus, "Storage is not expired yet")
    };
}