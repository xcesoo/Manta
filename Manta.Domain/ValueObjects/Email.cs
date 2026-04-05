using System.Text.RegularExpressions;

namespace Manta.Domain.ValueObjects;

public sealed partial record  Email
{
    [GeneratedRegex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")]
    private static partial Regex EmailRegex();
    public string Value { get; private set; }

    private Email(){}
    public Email(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || !IsValid(value))
            throw new ArgumentException("Invalid email format.", nameof(value));
        Value = value;
    }

    private bool IsValid(string value)
    {
        return EmailRegex().IsMatch(value);
    }

    public override string ToString() => Value;
    public static implicit operator string(Email email) => email.Value;
    public static implicit operator Email(string email) => new Email(email);
}