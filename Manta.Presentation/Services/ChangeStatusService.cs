namespace Manta.Presentation.Services;

public static class ChangeStatusService
{
    public static event Action OnStatusChanged;
    public static void ShipmentChangedStatus()
    {
        OnStatusChanged?.Invoke();
    }
}