using Manta.Domain.CreationOptions;
using Manta.Domain.Entities;
using Manta.Infrastructure.Repositories;

namespace Manta.Application.Factories;

public static class UserFactory
{
    public static async Task<TUser?> Create<TUser>(UserCreationOptions options, IUserRepository context)
    where TUser : User
    {
        var newOptions = options with{Id = await context.GetNextIdAsync()};
        object instance = typeof(TUser) switch
        {
            var t when t == typeof(Admin) =>
                new Admin((int)newOptions.Id, newOptions.Name, newOptions.Email),
            var t when t == typeof(Cashier) =>
                new Cashier((int)newOptions.Id!, newOptions.Name, newOptions.Email, (int)newOptions.DeliveryPointId!),
            var t when t == typeof(Driver) =>
                new Driver((int)newOptions.Id!, newOptions.Name, newOptions.Email, newOptions.VehicleId!),
            var t when t == typeof(SystemUser) => SystemUser.Instance,                
            _ => throw new ArgumentException("Invalid user type")
        };
        if(await context.ExistsByEmailAsync(newOptions.Email))
            throw new ArgumentException("User with this email already exists");
        await context.AddAsync(instance as User);
        await context.SaveChangesAsync();
        return instance as TUser;
    }
}