using manta.Domain.Entities;
using manta.Domain.Enums;
using manta.Domain.Interfaces;

namespace manta.Domain.StatusRules;

public class DeliveredRule : IParcelStatusRule
{
    public bool ShouldApply(Parcel parcel, DeliveryPoint deliveryPoint, out EParcelStatus newStatus)
    {
        // Приклад логіки:
        // Перевіряємо, чи поточний статус - "Готове до видачі"
        // І чи поточна точка видачі відповідає точці видачі посилки
        if (parcel.CurrentStatus.Status == EParcelStatus.ReadyForPickup && parcel.DeliveryPointId == deliveryPoint.Id)
        {
            newStatus = EParcelStatus.Delivered; // Встановлюємо новий статус
            return true; // Правило застосовується
        }
        newStatus = parcel.CurrentStatus.Status;
        return false; // Правило не застосовується
    }
}