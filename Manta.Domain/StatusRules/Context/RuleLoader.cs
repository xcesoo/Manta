using Manta.Domain.StatusRules.Implementations;
using Manta.Domain.StatusRules.Interfaces;
using Manta.Domain.StatusRules.Policies;

namespace Manta.Domain.StatusRules.Context;

public static class RuleLoader
{
    private static List<IParcelStatusRule> _rules = new List<IParcelStatusRule>
    {
        new ReturnRequestedRule(),
        new WrongDeliveryPointRule(),
        new ReadyForPickupRule(),
        new DeliveredRule(),
        new ReaddressRequestedRule(),
        new AcceptAtDeliveryPointPolicy(),
        new ShipmentCancelledRule()
    };
    public static List<IParcelStatusRule> LoadAllRules => _rules;
}
