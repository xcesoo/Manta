using Manta.Domain.Enums;
using MediatR;

namespace Manta.Application.Commands.User;

public record CreateUserCommand(
    string Name,
    string Email,
    string Password,
    EUserRole Role,
    Guid? DeliveryPointId = null,
    string? VehicleId = null) : IRequest <Guid>;