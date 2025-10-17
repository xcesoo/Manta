using Manta.Application.DataSeed;
using Manta.Application.Services;
using Manta.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Manta.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<Seed>();

        // Реєстрація Domain сервісів
        services.AddScoped<ParcelStatusService>();
        
        // Реєстрація Application сервісів
        services.AddScoped<ParcelDeliveryService>();

        return services;
    }
}