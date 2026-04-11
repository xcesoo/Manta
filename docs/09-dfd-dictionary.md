# Словник DFD (Data Flow Diagram Dictionary)

## 1. Зовнішні сутності (External Entities)
* **E1 cashier**: Співробітник відділення (роль `Cashier`), який ініціює створення посилки, а також фізично приймає її на відділенні, викликаючи відповідні API-ендпоінти.

## 2. Сховища даних (Data Stores)
* **D1 parcels**: Агрегати посилок. Джерело істини про поточний статус та `arrived_at`.
* **D2 delivery_points**: Логістичні відділення. Містить ліміт `capacity`.
* **D3 outbox_messages**: Черга інтеграційних подій (MassTransit) для NFR: Performance-first.
* **D4 processed_logs**: Логи оброблених повідомлень для гарантії ідемпотентності.
* **D5 delivery_vehicles**: Кур'єрські авто. Звідси посилка вивантажується при скануванні на відділенні.
* **D6 users**: Працівники (касири/водії) для ініціалізації `RuleContext`.

## 3. Процеси (Processes)
### Level 0 (Загальні процеси)
* **P1 discover_points**: Пошук доступних відділень клієнтом або касиром.
* **P2 create_parcel**: Оформлення нової посилки в системі.
* **P3 accept_parcel_booking**: Головний процес прийому посилки на відділенні (`AcceptParcelAtDeliveryPoint`).

### Level 1 (Деталізація P3 - Accept Parcel Handler)
* **P3.1 receive_api_command**: Прийом `AcceptParcelAtDeliveryPointCommand`, швидкий запис в Outbox.
* **P3.2 check_idempotency**: Consumer перевіряє `processed_logs` (D4).
* **P3.3 fetch_domain_data**: `GetByIdAsync` для Parcel, DeliveryPoint та User. Формування `RuleContext.ForDelivery`.
* **P3.4 apply_status_policies**: Виклик `ParcelStatusService.ApplyRule<AcceptAtDeliveryPointPolicy>`.
* **P3.5 persist_state**: Збереження даних. Якщо посилка була в авто — виклик `UnloadParcelFromDeliveryVehicle`. Зміна `MoveToLocation` та запис у базу.

### Level 2 (Деталізація P3.4 - AcceptAtDeliveryPointPolicy)
* **P3.4.1 validate_dp_not_null**: Перевірка, що відділення існує в контексті.
* **P3.4.2 apply_wrong_dp_rule**: Робота `WrongDeliveryPointRule`. Якщо точка не співпадає, безпечно переводить статус у `WrongLocation`.
* **P3.4.3 apply_return_requested_rule**: Робота `ReturnRequestedRule`. Перевіряє, чи не скасована посилка раніше.
* **P3.4.4 apply_ready_for_pickup_rule**: Робота `ReadyForPickupRule`. Включає в себе перевірку ліміту (Twist A: `capacity <= 3`). Якщо успішно — статус `ReadyForPickup` і оновлення `ArrivedAt` (для старту Timeslot).

## 4. Потоки даних (Data Flows)
* **accept_request**: HTTP-запит з `parcel_id` та `delivery_point_id`.
* **success_context**: Валідний контекст після політик, готовий до збереження в БД.
* **rule_result_error**: Доменна помилка (наприклад, `ERuleResultError.WrongParcelStatus` або `slot_unavailable`).