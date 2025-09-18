using Manta.Domain.Entities;
using Manta.Domain.Enums;
using Manta.Domain.Interfaces;
using Manta.Domain.StatusRules;

namespace Manta.Domain.Services;

public class ParcelStatusService
{
    private readonly List<IParcelStatusRule> _rules = RuleLoader.LoadAllRules;

    public bool UpdateStatus(Parcel parcel, DeliveryPoint deliveryPoint, User changedBy)
    {
        if (parcel == null) throw new ArgumentNullException(nameof(parcel));
        if (deliveryPoint == null) throw new ArgumentNullException(nameof(deliveryPoint));

        foreach (var rule in _rules)
        {
            if(rule.ShouldApply(parcel, deliveryPoint, out EParcelStatus newStatus)
               && newStatus != parcel.CurrentStatus.Status)
            {
                parcel.ChangeStatus(newStatus, changedBy ?? SystemUser.Instance);
                return true;
            }
        }

        return false;
    }

    public bool ApplyRule<T>(Parcel parcel, DeliveryPoint deliveryPoint, User changedBy) 
        where T : IParcelStatusRule
    {
        var rule = _rules.OfType<T>().FirstOrDefault();
        if (rule == null) throw new Exception($"Rule {typeof(T).Name} does not exist");
        if (rule.ShouldApply(parcel, deliveryPoint, out EParcelStatus newStatus)
            && newStatus != parcel.CurrentStatus.Status)
        {
            parcel.ChangeStatus(newStatus, changedBy ?? SystemUser.Instance);
            return true;
        }
        return false;
    }
}