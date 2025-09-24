using Manta.Domain.Interfaces;

namespace Manta.Domain.StatusRules;

public static class RuleLoader
{
    private static List<IParcelStatusRule> _rules = new List<IParcelStatusRule>
    {
        new CanceledToReturnRequestedRule(),
        new WrongDeliveryPointRule(),
        new ReadyForPickupRule(),
        new DeliveredRule(),
        new ReaddressRequestedRule()
    };
    public static List<IParcelStatusRule> LoadAllRules => _rules;
}