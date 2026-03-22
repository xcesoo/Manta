using Manta.Domain.CreationOptions;
using Manta.Domain.Entities;
using Manta.Domain.Interfaces;

namespace Manta.Application.Factories;

public static class UserFactory
{
    public static async Task<TUser?> Create<TUser>(UserCreationOptions options)
    where TUser : User
    {
        object instance = typeof(TUser) switch
        {
            var t when t == typeof(Admin) =>
                new Admin((int)options.Id, options.Name, options.Email,  options.PasswordHash),
            var t when t == typeof(Cashier) =>
                new Cashier((int)options.Id!, options.Name, options.Email, (int)options.DeliveryPointId!,  options.PasswordHash),
            var t when t == typeof(Driver) =>
                new Driver((int)options.Id!, options.Name, options.Email, options.PasswordHash, options.VehicleId!),
            var t when t == typeof(SystemUser) => SystemUser.Instance,
            var t when t == typeof(UnknownUser) => 
                new UnknownUser((int)options.Id, options.Name, options.Email, options.PasswordHash),
            _ => throw new ArgumentException("Invalid user type")
        };
        return instance as TUser;
    }
}