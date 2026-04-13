# Мапа форм та валідації

## Форма 1: Створення відправлення (Бронювання)
**API Endpoint:** `POST /api/Parcels` (Payload: `CreateParcelCommand`)
**Дія після успіху:** Отримання `202 Accepted` з `uuid` посилки.

| Поле (snake_case) | Тип UI | Обов'язкове | Клієнтська валідація                 | Мапінг серверної помилки (422) |
| :--- |:-------|:------------|:-------------------------------------| :--- |
| `delivery_point_id` | Select | Так         | `required`. Не порожнє значення.     | `details[].attribute == 'delivery_point_id'` |
| `weight` | Number | Так         | `min="0.1"`, `step="0.1"`. Більше 0. | `details[].attribute == 'weight'` |
| `amount_due` | Number | Так         | `min="0"`, `step="0.01"`.            | `details[].attribute == 'amount_due'` |
| `recipient_name` | Text   | Так         | Максимум 200 символів.               | `details[].attribute == 'recipient_name'` |
| `recipient_phone` | Tel    | Так         | Патерн: `^\+\d{12}$`.                | `details[].attribute == 'recipient_phone'` |
| `recipient_email` | Email  | Так         | Патерн: `^[^@\s]+@[^@\s]+\.[^@\s]+$`                             | `details[].attribute == 'recipient_email'` |

**UX-патерни та NFR (Performance-first):**
* **Блокування Submit:** Після натискання кнопки "Створити", вона переходить у стан `disabled` (loading), щоб уникнути подвійного відправлення запиту.
* **Error Summary:** Якщо сервер повертає `422 Unprocessable Content`, над формою з'являється блок із переліком помилок, які мають якірні посилання на відповідні поля.
* **Фокус-менеджмент:** При клієнтській валідації фокус автоматично переміщується на перше невалідне поле.

## Форма 2: Прийом на відділенні (Зайняття слоту)
**API Endpoint:** `POST /api/Parcels/accept` (Payload: `AcceptParcelAtDeliveryPointCommand`)

| Поле (snake_case) | Тип UI | Обов'язкове | Клієнтська валідація | Мапінг серверної помилки |
| :--- | :--- | :--- | :--- | :--- |
| `parcel_id` | Text (Scan) | Так | Валідний UUID формат. | `details[].attribute == 'parcel_id'` |
| `delivery_point_id` | Hidden/Select | Так | Валідний UUID. (Зазвичай береться з контексту авторизованого касира). | `details[].attribute == 'delivery_point_id'` |

**Обробка Twist A (Місткість):**
Якщо сервер повертає `409 Conflict` (перевищено ліміт 3 місця), форма не очищується. Відображається глобальний банер помилки (Global Alert) з відповідним повідомленням з `ApiErrorResponse.message`.