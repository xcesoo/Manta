    flowchart LR
    classDef entity fill:#f9f9f9,stroke:#333,stroke-width:2px;
    E1[E1: customer_sender]:::entity
    E2[E2: cashier]:::entity

    classDef store fill:#e1f5fe,stroke:#0288d1,stroke-width:2px;
    D1[(D1: parcels)]:::store
    D2[(D2: delivery_points)]:::store
    D3[(D3: outbox_messages)]:::store
    D4[(D4: processed_logs)]:::store
    D5[(D5: vehicles)]:::store

    classDef process fill:#e8f5e9,stroke:#388e3c,stroke-width:2px;
    P1((P1: discover_delivery_points)):::process
    P2((P2: create_parcel)):::process
    P3((P3: accept_parcel_at_point)):::process


    E1 -- search_query --> P1
    P1 -- delivery_points_list --> E1
    P1 <== read_services ==> D2

    E1 -- create_request --> P2
    P2 -- parcel_guid--> E1
    P2 -- outbox_payload --> D3
    D3 -. consume_create .-> D1

    E2 -- accept_request --> P3
    P3 -- outbox_payload --> D3
    D3 -. consume_accept .-> P3
    P3 <== check_idempotency ==> D4
    P3 <== unload_parcel ==> D5
    P3 <== update_status_and_arrived_at ==> D1