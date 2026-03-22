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
            Address:request.Address
            );
        var deliveryPoint = await DeliveryPointFactory.Create(options);
        if (deliveryPoint == null) throw new ArgumentException($"Failed to create delivery point");
        await _deliveryPointRepository.AddAsync(deliveryPoint, cancellationToken);
        await _deliveryPointRepository.SaveChangesAsync(cancellationToken);
        return deliveryPoint.Id;
    }
}