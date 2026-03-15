using MediatR;

namespace Manta.Application.Commands.Parcel;

public record AcceptParcelAtDeliveryPointCommand(int ParcelId, int DeliveryPointId, int SenderId) : IRequest<int>;