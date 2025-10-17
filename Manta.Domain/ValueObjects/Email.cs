namespace Manta.Domain.ValueObjects;

public sealed record  Email
{
    public string Value { get; private set; }

    private Email(){}
    public Email(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || !IsValid(value))
            throw new ArgumentException("Invalid email format.", nameof(value));
        Value = value;
    }

    private bool IsValid(string email)
    {
        return System.Text.RegularExpressions.Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
    }

    public override string ToString() => Value;
    public static implicit operator string(Email email) => email.Value;
    public static implicit operator Email(string email) => new Email(email);
}