using Manta.Domain.Entities;

namespace Manta.Presentation.Services;

public static class CashDeskManager
{
    public static List<Parcel> Parcels { get; set; } = new();
    public static event Action? ParcelsChanged;
    
    public static void Add(Parcel parcel)
    {
        if (Parcels.Contains(parcel)) return;
        Parcels.Add(parcel);
        ParcelsChanged?.Invoke();
    }
    public static void Remove(Parcel parcel)
    {
        if (!Parcels.Contains(parcel)) return;
        Parcels.Remove(parcel);
        ParcelsChanged?.Invoke();
    }
    public static void Clear()
    {
        Parcels.Clear();
        ParcelsChanged?.Invoke();
    }

    public static void Complete()
    {
        ChangeStatusService.ShipmentChangedStatus();
    }
}