using Manta.Domain.Entities;
using Manta.Domain.Enums;
using Manta.Domain.Exceptions;
using Manta.Domain.StatusRules;
using Manta.Domain.StatusRules.Interfaces;
using Manta.Domain.ValueObjects;

namespace Manta.Domain.Services;

public class ParcelStatusService
{
    private readonly List<IParcelStatusRule> _rules = RuleLoader.LoadAllRules;
    
    public bool ApplyRule<TRule>(RuleContext context) 
        where TRule : IParcelStatusRule
    {
        var rule = _rules.OfType<TRule>().FirstOrDefault()
            ?? throw new InvalidOperationException($"Rule {typeof(TRule).Name} not registered");
        
        if(context.Parcel is null || context.User is null)
            throw new ArgumentException("RuleContext must contain Parcel and ChangedBy.");
        
        var result = rule.ShouldApply(context);
        
        if (result.IsFailed)
            throw new ParcelDomainException(context.Parcel, context.DeliveryPoint, result);
        
        context.Parcel.ChangeStatus(result.NewStatus.Value, context.User);
        return true;
    }
}