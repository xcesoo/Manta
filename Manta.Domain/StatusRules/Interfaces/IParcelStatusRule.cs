using Manta.Domain.Entities;
using Manta.Domain.ValueObjects;

namespace Manta.Domain.StatusRules.Interfaces;

public interface IParcelStatusRule
{
    RuleResult ShouldApply(RuleContext context);
}