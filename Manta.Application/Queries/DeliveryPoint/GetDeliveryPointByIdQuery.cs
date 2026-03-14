using MediatR;

namespace Manta.Application.Queries.DeliveryPoint;

public record GetDeliveryPointByIdQuery(int Id) : IRequest<Domain.Entities.DeliveryPoint>;