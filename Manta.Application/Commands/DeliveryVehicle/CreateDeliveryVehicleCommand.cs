using MediatR;

namespace Manta.Application.Commands.DeliveryVehicle;

public record CreateDeliveryVehicleCommand(
    string LicensePlate, 
    string Brand, string Model, 
    double Capacity) : IRequest<Guid>;