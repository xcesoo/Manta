using Manta.Domain.Entities;

namespace Manta.Presentation.State;

public static class AppContext
{
    public static User? CurrentUser { get; set; } = null;
    public static int? CurrentDeliveryPointId { get; set; } = null;
    public static event Action DeliveryPointChangedEvent;
    public static event Action UserChangedEvent;
    public static event Action ShipmentChangedEvent;
    public static void DeliveryPointChanged()
    {
        DeliveryPointChangedEvent?.Invoke();
    }
    public static void UserChanged()
    {
        UserChangedEvent?.Invoke();
    }
    public static void ShipmentChanged()
    {
        ShipmentChangedEvent?.Invoke();
    }
}