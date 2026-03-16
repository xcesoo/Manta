using Manta.Application.Interfaces;
using Manta.Domain.Interfaces;
using Manta.Infrastructure.Persistence;
using Manta.Infrastructure.Repositories;
using Manta.WebApi.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Manta.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        services.AddDbContext<MantaDbContext>(options =>
            options.UseSqlite(
                configuration.GetConnectionString("DefaultConnection")));
        services.AddScoped<IParcelRepository, ParcelRepository>();
        services.AddScoped<IDeliveryPointRepository, DeliveryPointRepository>();
        services.AddScoped<IDeliveryVehicleRepository, DeliveryVehicleRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IPasswordHasher, BcryptPasswordHasher>();
        services.AddScoped<IJwtProvider, JwtProvider>();
        return services;
    }
}
