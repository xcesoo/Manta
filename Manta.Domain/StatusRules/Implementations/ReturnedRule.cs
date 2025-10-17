using Manta.Domain.Enums;
using Manta.Domain.StatusRules.Interfaces;
using Manta.Domain.ValueObjects;

namespace Manta.Domain.StatusRules.Implementations;

public sealed class ReturnedRule : IParcelStatusRule
{
    public RuleResult ShouldApply(RuleContext context) => context switch
    {
        {Parcel: {CurrentStatus:{Status:EParcelStatus.InReturnTransit}}} => RuleResult.Ok(EParcelStatus.Returned),
        _ => RuleResult.Failed(ERuleResultError.WrongParcelStatus, "Parcel is not in return transit")
    };
}