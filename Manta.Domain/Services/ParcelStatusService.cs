using Manta.Domain.Entities;
using Manta.Domain.Enums;
using Manta.Domain.Exceptions;
using Manta.Domain.Interfaces;
using Manta.Domain.StatusRules;
using Manta.Domain.ValueObjects;

namespace Manta.Domain.Services;

public class ParcelStatusService
{
    private readonly List<IParcelStatusRule> _rules = RuleLoader.LoadAllRules;

    public bool UpdateStatus(Parcel parcel, DeliveryPoint deliveryPoint, User changedBy)
    {
        if (parcel == null) throw new ArgumentNullException(nameof(parcel));
        if (deliveryPoint == null) throw new ArgumentNullException(nameof(deliveryPoint));
        RuleResult? ruleResult = null;
        foreach (var rule in _rules)
        {
            ruleResult = rule.ShouldApply(parcel, deliveryPoint);
            if (ruleResult.IsOk)
            {
                parcel.ChangeStatus(ruleResult.NewStatus.Value, changedBy ?? SystemUser.Instance);
                return true;
            }
        }
        throw new ParcelDomainException(parcel, deliveryPoint, ruleResult ?? RuleResult.Failed(ERuleResultError.Unknown, "Unknown"));
    }

    public bool ApplyRule<T>(Parcel parcel, DeliveryPoint deliveryPoint, User changedBy) 
        where T : IParcelStatusRule
    {
        var rule = _rules.OfType<T>().FirstOrDefault();
        
        if (rule == null) throw new Exception($"Rule {typeof(T).Name} does not exist");
        if (parcel == null) throw new ArgumentNullException(nameof(parcel));
        if (deliveryPoint == null) throw new ArgumentNullException(nameof(deliveryPoint));
        
        var ruleResult = rule.ShouldApply(parcel, deliveryPoint);
        
        if (ruleResult.IsFailed)
            throw new ParcelDomainException(parcel, deliveryPoint, ruleResult);
        
        parcel.ChangeStatus(ruleResult.NewStatus.Value, changedBy ?? SystemUser.Instance);
        return true;
    }
}