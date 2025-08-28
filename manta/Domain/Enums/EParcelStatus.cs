namespace manta.Domain.Enums;

public enum EParcelStatus
{
    Processing,                  // Обробка
    Assembling,                  // Комплектується
    ScheduledForShipment,        // Заплановано до відправки
    ShipmentCancelled,           // Відправлення скасовано
    InTransit,                   // В дорозі
    ReaddressRequested,          // Оформлено переадресацію
    ReadyForPickup,              // Готове до видачі
    WrongLocation,               // Невірна локація
    StorageExpired,              // Вийшов термін зберігання
    ReturnRequested,             // Отримано запит на повернення
    ReturnProcessed,             // Оформлене повернення
    ReturnedToContainer,         // Повернення помістили в контейнер
    ReturnGivenToCourier,        // Повернення передано курʼєру
    PartiallyReceived,           // Частково отримано
    Delivered,                   // Видано
    Returned                     // Повернуто
}