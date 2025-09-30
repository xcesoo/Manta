using Manta.Domain.StatusRules.Implementations;
using Manta.Domain.StatusRules.Interfaces;
using Manta.Domain.StatusRules.Policies;

namespace Manta.Domain.StatusRules;

public static class RuleLoader
{
    private static List<IParcelStatusRule> _rules = new List<IParcelStatusRule>
    {
        new CanceledToReturnRequestedRule(),
        new WrongDeliveryPointRule(),
        new ReadyForPickupRule(),
        new DeliveredRule(),
        new ReaddressRequestedRule(),
        new AcceptAtDeliveryPointPolicy()
    };
    public static List<IParcelStatusRule> LoadAllRules => _rules;
}