# Словник DFD (Data Flow Diagram Dictionary)

Цей словник описує всі елементи діаграм потоків даних (DFD) для системи управління місткістю логістичних відділень проєкту **Manta**. Усі ідентифікатори наведені у форматі `snake_case` згідно з вимогами до практичної роботи.

## 1. Зовнішні сутності (External Entities)
Зовнішні сутності — це актори (користувачі або зовнішні системи), які ініціюють потоки даних або отримують результати роботи системи.

* **E1 cashier**: Співробітник відділення (роль `Cashier` з `EUserRole.cs`). Він ініціює створення посилки (`POST /api/parcels`), а також фізично приймає її на відділенні (`POST /api/parcels/accept`), смикаючи відповідні API-ендпоінти.* **E2 cashier**: Співробітник відділення (роль `Cashier`), який фізично приймає посилку та сканує її, ініціюючи зміну статусу та перевірку місткості.

## 2. Сховища даних (Data Stores)
Сховища даних відповідають таблицям бази даних (`DbSet`) у контексті `MantaDbContext` та репозиторіях інфраструктурного шару.

* **D1 parcels**: Основне сховище агрегатів посилок (`IParcelRepository`). Зберігає статуси, вагу, габарити та таймслот зберігання (`arrived_at`).
* **D2 delivery_points**: Сховище логістичних відділень (`IDeliveryPointRepository`). Використовується для перевірки місткості (`capacity`) та доступності слотів.
* **D3 outbox_messages**: Черга інтеграційних повідомлень (`IIntegrationMessageQueue` / MassTransit). Забезпечує архітектурний NFR-акцент (Performance-first) для асинхронної обробки.
* **D4 processed_logs**: Таблиця логів оброблених повідомлень (`ProcessedLog`). Використовується консюмерами для перевірки ідемпотентності (запобігання подвійному скануванню посилки).
* **D5 delivery_vehicles**: Сховище транспортних засобів (`IDeliveryVehicleRepository`). Використовується для вивантаження посилки з авто при її прийнятті на відділенні.
* **D6 users**: Сховище користувачів (`IUserRepository`). Зберігає дані працівників та клієнтів для авторизації та валідації дій.

## 3. Процеси (Processes)
Процеси відображають обробники команд та запитів (Handlers) у шарі Application.

### Загальні процеси (Level 0)
* **P1 discover_delivery_points**: Отримання списку відділень (реалізовано через `GetDeliveryPointByIdQuery` та лістинг).
* **P2 create_parcel**: Асинхронне створення посилки (`CreateParcelCommand`). Приймає запит, повертає GUID і скидає подію в Outbox.
* **P3 accept_parcel_at_point**: Прийняття посилки у відділенні (`AcceptParcelAtDeliveryPointCommand`). Основний бізнес-процес бронювання місця (capacity).

### Декомпозиція процесу P3 (Level 1)
* **P3.1 accept_api_command**: HTTP API ендпоінт, що приймає запит від касира, формує `AcceptParcelAtDeliveryPointMessage` і записує його в **D3**.
* **P3.2 check_processed_logs**: Крок консюмера (`AcceptParcelAtDeliveryPointConsumer`), що перевіряє **D4** на наявність `message_id`, щоб відкинути дублікати.
* **P3.3 fetch_aggregates**: Отримання необхідних сутностей (`Parcel`, `DeliveryPoint`, `User`) з БД (**D1**, **D2**, **D6**) перед виконанням бізнес-логіки.
* **P3.4 apply_status_policy**: Валідація доменних правил через `ParcelStatusService.ApplyRule<AcceptAtDeliveryPointPolicy>`. Перевіряє правильність локації та місткість. Встановлює `arrived_at` та вивантажує з авто.
* **P3.5 persist_changes**: Асинхронне збереження оновлених сутностей у базу даних (**D1**, **D4**, **D5**) та завершення транзакції.

## 4. Потоки даних (Data Flows)
* **search_query**: Параметри пошуку відділення.
* **delivery_points_list**: DTO зі списком доступних відділень та їхньою місткістю.
* **create_request**: Вхідні дані для посилки (`weight`, `amount_due`, `recipient_email` тощо).
* **parcel_guid**: Унікальний ідентифікатор створеної або прийнятої посилки (`Guid`), який миттєво повертається клієнту.
* **outbox_payload**: Серіалізоване повідомлення (JSON), що містить `message_id`, тип події та дані команди.
* **accept_request**: HTTP POST запит від касира, що містить `parcel_id` та `delivery_point_id`.
* **consume_accept**: Процес читання повідомлення з черги брокером (RabbitMQ/MassTransit).
* **check_idempotency**: SQL-запит до `ProcessedLogs` для пошуку `message_id`.
* **unload_parcel**: Дані про оновлення статусу машини (звільнення місця в авто).
* **update_status_and_arrived_at**: Оновлений агрегат посилки зі статусом `ReadyForPickup` та встановленим таймслотом `arrived_at`.