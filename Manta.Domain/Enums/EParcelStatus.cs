namespace Manta.Domain.Enums;

public enum EParcelStatus
{
    Processing,                  // Обробка✅
    ShipmentCancelled,           // Відправлення скасовано ✅
    InTransit,                   // В дорозі ✅
    ReaddressRequested,          // Оформлено переадресацію ✅
    ReadyForPickup,              // Готове до видачі ✅
    WrongLocation,               // Невірна локація ✅
    StorageExpired,              // Вийшов термін зберігання
    ReturnRequested,             // Отримано запит на повернення ✅
    InReturnTransit,             // Повернення прямує назад✅
    Delivered,                   // Видано ✅
    Returned                     // Повернуто✅
}