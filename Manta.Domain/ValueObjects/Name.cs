namespace Manta.Domain.ValueObjects;

public sealed record  Name
{
    public string Value { get; private set; }

    public Name(string value)
    {
        if(string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Name cannot be null or empty.", nameof(value));
        if(!value.All(c => char.IsLetter(c) || char.IsWhiteSpace(c)))
            throw new ArgumentException("Name can only contain letters and spaces.", nameof(value));
        Value = value;
    }
    public override string ToString() => Value;
    public static implicit operator string(Name name) => name.Value;
    public static implicit operator Name(string name) => new Name(name);
}