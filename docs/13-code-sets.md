# Код-сети та їх еволюція (Code Sets)

## 1. Статуси посилки (`status` в таблиці `parcel_status_history`)
Це машинні значення (snake_case), які визначають життєвий цикл "бронювання" слота.

* `processing` — Посилка оформлена.
* `shipment_cancelled` — **Звільняє слот**. Скасована оператором точки видачі (Cashier) або адміністратором (Admin).
* `in_transit` — Доставляється.
* `readdress_requested` — **Звільняє слот**. Переадресування посилки на іншу точку видачі.
* `ready_for_pickup` — **Займає слот (capacity)**. Прибула на точку.
* `wrong_location` — **Звільняє слот**. Посилка приїхала в невідповідну точку видачі → `readdress_requested`.
* `storage_expired` — **Звільняє слот**. Вийшов термін зберігання.
* `return_requested` — **Звільняє слот**. Отримано запит на повернення.
* `in_return_transit` — Доставляється назад.
* `delivered` — **Звільняє слот**. Видана клієнту.
* `returned` — **Звільняє слот**. Повернута відправнику.

**Граф переходів:**

```mermaid
stateDiagram-v2
    [*] --> processing
    processing --> in_transit
    processing --> shipment_cancelled
    in_transit --> ready_for_pickup : Успішно
    in_transit --> wrong_location : Помилка логістики
    wrong_location --> readdress_requested
    readdress_requested --> in_transit
    ready_for_pickup --> delivered : Видано клієнту
    ready_for_pickup --> storage_expired : Минуло 3 дні
    storage_expired --> return_requested
    return_requested --> in_return_transit
    in_return_transit --> returned
    delivered --> [*]
    returned --> [*]
    shipment_cancelled --> [*]
   ```

## 2. Правило еволюції Code Sets
**Append-only (Тільки розширення).** Існуючі машинні коди жорстко зашиті в базу даних та логіку додатку.
* Дозволяється: Додавати нові статуси (напр., `destroyed`).
* **Забороняється:** Перейменовувати (напр., `delivered` на `given`) або видаляти існуючі статуси, оскільки це порушить зворотну сумісність (Backward Compatibility) та цілісність історичних логів.