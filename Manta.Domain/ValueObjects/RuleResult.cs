using Manta.Domain.Enums;

namespace Manta.Domain.ValueObjects;

public record RuleResult(
    bool Success, 
    EParcelStatus? NewStatus, 
    string? Code = null, 
    string? Message = null) // todo change Code to enum
{
    public static RuleResult Ok(EParcelStatus newStatus) => new RuleResult(true, newStatus, null, null);
    public static RuleResult Failed(string code, string message) => new RuleResult(false, null, code, message);
    
    public bool IsFailed => !Success;
    public bool IsOk => Success;
}