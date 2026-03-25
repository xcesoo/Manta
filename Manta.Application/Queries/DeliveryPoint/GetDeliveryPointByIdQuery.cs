using MediatR;

namespace Manta.Application.Queries.DeliveryPoint;

public record GetDeliveryPointByIdQuery(Guid Id) : IRequest<Domain.Entities.DeliveryPoint>;