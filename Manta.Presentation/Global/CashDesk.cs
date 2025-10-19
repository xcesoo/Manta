using Manta.Domain.Entities;

namespace Manta.Presentation;

public static class CashDesk
{
    public static List<Parcel> Parcels { get; set; } = new();
    public static event Action? ParcelsChanged;
    public static event Action? DeliveryCompleted;
    public static void AddParcel(Parcel parcel)
    {
        if (Parcels.Contains(parcel)) return;
        Parcels.Add(parcel);
        ParcelsChanged?.Invoke();
    }
    public static void RemoveParcel(Parcel parcel)
    {
        if (!Parcels.Contains(parcel)) return;
        Parcels.Remove(parcel);
        ParcelsChanged?.Invoke();
    }
    public static void Clear()
    {
        Parcels.Clear();
        ParcelsChanged?.Invoke();
        DeliveryCompleted?.Invoke();
    }
}