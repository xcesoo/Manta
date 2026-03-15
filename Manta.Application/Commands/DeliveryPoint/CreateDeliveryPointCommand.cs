using MediatR;

namespace Manta.Application.Commands.DeliveryPoint;

public record CreateDeliveryPointCommand(
    string Address,
    int? Id) : IRequest<int>;