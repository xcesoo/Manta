using MediatR;

namespace Manta.Application.Commands.Parcel;

public record AcceptParcelAtDeliveryPointCommand(Guid ParcelId, Guid DeliveryPointId, Guid SenderId) : IRequest<Guid>;