namespace Manta.Domain.CreationOptions;

public record DeliveryPointCreationOptions(
    string Address,
    int? Id = null
    );