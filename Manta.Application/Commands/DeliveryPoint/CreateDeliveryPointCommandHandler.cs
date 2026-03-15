using Manta.Application.Factories;
using Manta.Domain.CreationOptions;
using Manta.Domain.Interfaces;
using MediatR;

namespace Manta.Application.Commands.DeliveryPoint;

public class CreateDeliveryPointCommandHandler : IRequestHandler<CreateDeliveryPointCommand, int>
{
    private IDeliveryPointRepository _deliveryPointRepository;

    public CreateDeliveryPointCommandHandler(IDeliveryPointRepository deliveryPointRepository)
    {
        _deliveryPointRepository = deliveryPointRepository;
    }
    
    public async Task<int> Handle(CreateDeliveryPointCommand request, CancellationToken cancellationToken)
    {
        var options = new DeliveryPointCreationOptions(
            Address:request.Address,
            Id:request.Id);
        return await DeliveryPointFactory.Create(options, _deliveryPointRepository);
    }
}