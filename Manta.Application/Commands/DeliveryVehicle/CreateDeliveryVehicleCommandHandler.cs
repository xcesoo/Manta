using Manta.Application.Factories;
using Manta.Domain.CreationOptions;
using Manta.Domain.Interfaces;
using Manta.Domain.ValueObjects;
using MediatR;

namespace Manta.Application.Commands.DeliveryVehicle;

public class CreateDeliveryVehicleCommandHandler : IRequestHandler<CreateDeliveryVehicleCommand, int>
{
    private IDeliveryVehicleRepository _deliveryVehicleRepository;

    public CreateDeliveryVehicleCommandHandler(IDeliveryVehicleRepository deliveryVehicleRepository)
    {
        _deliveryVehicleRepository = deliveryVehicleRepository;
    }

    public async Task<int> Handle(CreateDeliveryVehicleCommand request, CancellationToken cancellationToken)
    {
        var options = new DeliveryVehicleCreationOptions(
            await _deliveryVehicleRepository.GetNextIdAsync(cancellationToken),
            request.LicensePlate!,
            (request.Brand, request.Model),
            request.Capacity);
        var vehicle = await DeliveryVehicleFactory.Create(options);
        if (vehicle is null) throw new ArgumentException("Failed to create delivery vehicle");
        await _deliveryVehicleRepository.AddAsync(vehicle, cancellationToken);
        await _deliveryVehicleRepository.SaveChangesAsync(cancellationToken);
        return vehicle.Id;
    }
}