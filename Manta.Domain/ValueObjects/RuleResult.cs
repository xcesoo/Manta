using Manta.Domain.Enums;

namespace Manta.Domain.ValueObjects;

public record RuleResult(
    bool Success, 
    EParcelStatus? NewStatus, 
    ERuleResultError? Code = null, 
    string? Message = null) 
{
    public static RuleResult Ok(EParcelStatus newStatus) => 
        new RuleResult(true, newStatus, null, null);
    public static RuleResult Failed(ERuleResultError code, string message) => 
        new RuleResult(false, null, code, message);
    
    public bool IsFailed => !Success;
    public bool IsOk => Success;
}