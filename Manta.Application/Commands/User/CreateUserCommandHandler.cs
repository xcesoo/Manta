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
        int rawValue = (int)request.Role;
        Console.WriteLine($"DEBUG: Role Name = {request.Role}, Numeric Value = {rawValue}");
        if(await _userRepository.ExistsByEmailAsync(request.Email))
            throw new ArgumentException("User with this email already exists");
        var options = new UserCreationOptions(
            Id: await _userRepository.GetNextIdAsync(cancellationToken),
            Name: request.Name,
            Email: request.Email,
            PasswordHash: _passwordHasher.Hash(request.Password),
            DeliveryPointId: request.DeliveryPointId,
            VehicleId: request.VehicleId ?? null);
        Domain.Entities.User? user = request.Role switch
        {
            EUserRole.Admin => await UserFactory.Create<Admin>(options),
            EUserRole.Cashier => await UserFactory.Create<Cashier>(options),
            EUserRole.Driver => await UserFactory.Create<Driver>(options),
            EUserRole.Unknown => await UserFactory.Create<UnknownUser>(options),
            _ => throw new ArgumentException("Invalid role")
        };
        if(user == null) throw new ArgumentException("Failed to create user"); 
        await _userRepository.AddAsync(user, cancellationToken);
        await _userRepository.SaveChangesAsync(cancellationToken);
        return user.Id;
    }
}