using Manta.Domain.ValueObjects;

namespace Manta.Application.Services;

public class PiiMasker
{
    public static string MaskEmail(Email email)
    {
        var parts = email.Value.Split('@');
        var local = parts[0];
        var domain = parts[1];

        if (local.Length <= 2) return $"***@{domain}";
        return $"{local.Substring(0,2)}***@{domain}";
    }

    public static string MaskPhone(PhoneNumber phone)
    {
        var prefix = phone.Value.Substring(0, 4);
        var tail = phone.Value.Substring(phone.Value.Length - 2);
        return $"{prefix}******{tail}";
    }

    public static string MaskName(Name name) => $"{name.Value.Substring(0, 1)}***";
}