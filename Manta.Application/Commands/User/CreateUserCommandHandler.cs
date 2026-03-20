using Manta.Application.Factories;
using Manta.Application.Interfaces;
using Manta.Domain.CreationOptions;
using Manta.Domain.Entities;
using Manta.Domain.Enums;
using Manta.Domain.Interfaces;
using MediatR;

namespace Manta.Application.Commands.User;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
{
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUserRepository _userRepository;

    public CreateUserCommandHandler(IPasswordHasher passwordHasher, IUserRepository userRepository)
    {
        _passwordHasher = passwordHasher;
        _userRepository = userRepository;
    }
    public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var options = new UserCreationOptions(
            Name: request.Name,
            Email: request.Email,
            PasswordHash: _passwordHasher.Hash(request.Password),
            DeliveryPointId: request.DeliveryPointId,
            VehicleId: request.VehicleId ?? null);
        Domain.Entities.User? user = request.Role switch
        {
            EUserRole.Admin => await UserFactory.Create<Admin>(options, _userRepository),
            EUserRole.Cashier => await UserFactory.Create<Cashier>(options, _userRepository),
            EUserRole.Driver => await UserFactory.Create<Driver>(options, _userRepository),
            _ => throw new ArgumentException("Invalid role")
        };
        if(user == null) throw new ArgumentException("Failed to create user"); 
        return user.Id;
    }
}