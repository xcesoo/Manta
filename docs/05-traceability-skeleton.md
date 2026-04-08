# Traceability Skeleton

| User Story | Design (DFD / ERD) | API Path / Команда | Тести / Error Codes                                     |
| :--- | :--- | :--- |:--------------------------------------------------------|
| **US-1** (Прийняття/бронювання комірки) | | `POST /api/parcels/accept` <br> `AcceptParcelAtDeliveryPointCommand` | 202 Accepted (returns GUID) <br> 409 `slot_unavailable` |
| **US-2** (Перевірка слоту `storage`) | | `GET /api/parcels/{id}` <br> `GetParcelByIdQuery` | 200 OK <br> 404 `not_found`                             |
| **US-3** (Скасування посилки) | | `PATCH /api/parcels/{id}/cancel` | 200 OK <br> 409 `wrong_parcel_status`                   |
| **US-4** (Створення відділення) | | `POST /api/deliverypoint` <br> `CreateDeliveryPointCommand` | 201 Created                                             |