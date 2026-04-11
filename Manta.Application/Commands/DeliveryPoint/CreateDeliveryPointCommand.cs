using MediatR;

namespace Manta.Application.Commands.DeliveryPoint;

public record CreateDeliveryPointCommand(
    string Address,
    int Capacity
    ) : IRequest<Guid>;