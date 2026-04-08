flowchart TD
classDef entity fill:#f9f9f9,stroke:#333,stroke-width:2px;
classDef store fill:#e1f5fe,stroke:#0288d1,stroke-width:2px;
classDef process fill:#fff3e0,stroke:#f57c00,stroke-width:2px;

    E2[E2: cashier]:::entity
    D1[(D1: parcels)]:::store
    D2[(D2: delivery_points)]:::store
    D3[(D3: outbox_messages)]:::store
    D4[(D4: processed_logs)]:::store
    D5[(D5: delivery_vehicles)]:::store
    D6[(D6: users)]:::store

    P3_1((P3.1: accept_api_command)):::process
    P3_2((P3.2: check_processed_logs)):::process
    P3_3((P3.3: fetch_aggregates)):::process
    P3_4((P3.4: apply_status_policy)):::process
    P3_5((P3.5: persist_changes)):::process

    E2 -- "HTTP POST (accept_request)" --> P3_1
    P3_1 -- "enqueue_message" --> D3
    
    D3 -. "Consume(AcceptParcelMessage)" .-> P3_2
    
    P3_2 -- "read(message_id)" --> D4
    P3_2 -- "if already_processed = true" --> End([Drop Message])
    P3_2 -- "if false -> invoke Handler" --> P3_3
    
    P3_3 -- "GetByIdAsync(ParcelId)" --> D1
    P3_3 -- "GetByIdAsync(DeliveryPointId)" --> D2
    P3_3 -- "GetByIdAsync(UserId)" --> D6
    P3_3 -- "entities_loaded" --> P3_4
    
    P3_4 -- "RuleContext.ForDelivery" --> P3_4
    P3_4 -- "UnloadParcelFromDeliveryVehicle" --> D5
    P3_4 -- "ChangeArrivedAt(UtcNow)" --> P3_5
    
    P3_5 -- "UpdateAsync(parcel)" --> D1
    P3_5 -- "UpdateAsync(vehicle)" --> D5
    P3_5 -- "Add(ProcessedLog)" --> D4