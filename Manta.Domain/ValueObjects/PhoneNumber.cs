using System.Text.RegularExpressions;

namespace Manta.Domain.ValueObjects;

public sealed partial record  PhoneNumber
{
    [GeneratedRegex(@"^\+\d{12}$")]
    private static partial Regex PhoneRegex();
    public string Value { get; }

    private PhoneNumber(){}
    public PhoneNumber(string value)
    {
        if(!IsValid(value))
            throw new ArgumentException("Invalid phone number format.", nameof(value));
        Value = value;
    }

    private bool IsValid(string value)
    {
        return PhoneRegex().IsMatch(value);
    }

    public override string ToString() => Value;
    public static implicit operator string(PhoneNumber phoneNumber) => phoneNumber.Value;
    public static implicit operator PhoneNumber(string phoneNumber) => new PhoneNumber(phoneNumber);
}