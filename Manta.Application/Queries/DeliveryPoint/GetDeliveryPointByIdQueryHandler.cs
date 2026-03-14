using Manta.Domain.Interfaces;
using MediatR;

namespace Manta.Application.Queries.DeliveryPoint;

public class GetDeliveryPointByIdQueryHandler(IDeliveryPointRepository deliveryPointRepository)
    : IRequestHandler<GetDeliveryPointByIdQuery, Domain.Entities.DeliveryPoint>
{
    public async Task<Domain.Entities.DeliveryPoint> Handle(GetDeliveryPointByIdQuery request,
        CancellationToken cancellationToken)
    {
        return await deliveryPointRepository.GetByIdAsync(request.Id);
    }
}