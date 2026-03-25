using Manta.Domain.ValueObjects;
using MediatR;

namespace Manta.Application.Commands.Parcel;

public record AcceptParcelAtDeliveryPointCommand(Guid ParcelId, Guid DeliveryPointId) : IRequest<Guid>;