using Manta.Domain.Interfaces;

namespace Manta.Domain.StatusRules;

public static class RuleLoader
{
    private static List<IParcelStatusRule> _rules = new List<IParcelStatusRule>
    {
        new WrongDeliveryPointRule(),
        new ReadyForPickupRule(),
        new DeliveredRule()
    };
    public static List<IParcelStatusRule> LoadAllRules => _rules;
}