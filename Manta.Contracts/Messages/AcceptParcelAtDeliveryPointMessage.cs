using Manta.Contracts.Interfaces;

namespace Manta.Contracts.Messages;

public record AcceptParcelAtDeliveryPointMessage(
    Guid MessageId,
    Guid ParcelId, 
    Guid DeliveryPointId,
    Guid UserId) : IParcelStatusUpdate;