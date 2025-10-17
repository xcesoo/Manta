using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Manta.Domain.ValueObjects;

public sealed record  LicensePlate
{
    public string Value { get; }

    private LicensePlate(){}
    private LicensePlate(string value)
    {
        Value = value;
    }

    public static LicensePlate Create(string value)
    {
        if(string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("LicensePlate cannot be null or whitespace.", nameof(value));
        if (!Regex.IsMatch(value, "^\\p{L}{2}\\d{4}\\p{L}{2}$"))
            throw new ArgumentException($"Invalid license plate format -> {value}. {nameof(value)}");
        return new LicensePlate(value);
    }

    public static implicit operator string(LicensePlate licensePlate) => licensePlate.Value;
    public static implicit operator LicensePlate(string licensePlate) => Create(licensePlate);
    
}