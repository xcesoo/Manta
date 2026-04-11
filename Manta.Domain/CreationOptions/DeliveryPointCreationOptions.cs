namespace Manta.Domain.CreationOptions;

public record DeliveryPointCreationOptions(
    Guid Id,
    string Address,
    int Capacity
    );