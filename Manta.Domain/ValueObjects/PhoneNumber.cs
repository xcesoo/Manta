namespace Manta.Domain.ValueObjects;

public sealed record  PhoneNumber
{
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
        return System.Text.RegularExpressions.Regex.IsMatch(value, @"^\+\d{12}$");
    }

    public override string ToString() => Value;
    public static implicit operator string(PhoneNumber phoneNumber) => phoneNumber.Value;
    public static implicit operator PhoneNumber(string phoneNumber) => new PhoneNumber(phoneNumber);
}