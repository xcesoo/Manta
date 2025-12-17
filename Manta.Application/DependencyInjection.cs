using System.Reflection;
using Manta.Application.DataSeed;
using Manta.Application.Services;
using Manta.Domain.Services;
using Manta.Domain.StatusRules.Context;
using Microsoft.Extensions.DependencyInjection;

namespace Manta.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<Seed>();

        services.AddSingleton(RuleLoader.LoadAllRules);
        
        services.AddScoped<ParcelStatusService>();
        
        services.AddScoped<ParcelDeliveryService>();

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        
        return services;
    }
}