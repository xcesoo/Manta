using Manta.Application.DataSeed;
using Manta.Application.Services;
using Manta.Domain.Services;
using Manta.Domain.StatusRules.Context;
using Microsoft.Extensions.DependencyInjection;
using Manta.Application.Common.Events;
using Manta.Domain.Events;
using Manta.Application.Handlers;

namespace Manta.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<Seed>();

        services.AddSingleton(RuleLoader.LoadAllRules);
        
        services.AddScoped<IHandle<ParcelAddedToDeliveryPointEvent>, ParcelAddedHandler>();
        
        services.AddScoped<IHandle<ParcelDeliveredEvent>, ParcelDeliveredHandler>();
        
        services.AddScoped<IEventDispatcher, EventDispatcher>();
        
        services.AddScoped<ParcelStatusService>();
        
        services.AddScoped<ParcelDeliveryService>();
        

        return services;
    }
}