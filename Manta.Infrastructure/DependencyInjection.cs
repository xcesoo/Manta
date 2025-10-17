using Manta.Infrastructure.Persistence;
using Manta.Infrastructure.Repositories;
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
        // Реєстрація DbContext з SQLite
        services.AddDbContext<MantaDbContext>(options =>
            options.UseSqlite(
                configuration.GetConnectionString("DefaultConnection")));

        // Реєстрація репозиторіїв
        services.AddScoped<IParcelRepository, ParcelRepository>();
        services.AddScoped<IDeliveryPointRepository, DeliveryPointRepository>();
        services.AddScoped<IDeliveryVehicleRepository, DeliveryVehicleRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}
