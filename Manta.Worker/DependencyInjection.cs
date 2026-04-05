using Manta.Application.Handlers;
using Manta.Application.Interfaces;
using Manta.Infrastructure;
using Manta.Infrastructure.Consumers;
using Manta.Infrastructure.Messaging;
using Manta.Infrastructure.Services;
using Manta.WebApi.Auth;
using MassTransit;

namespace Manta.Worker;

public static class DependencyInjection
{
    public static IServiceCollection AddWorkerInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSharedPersistence(configuration);
        
        services.AddHttpContextAccessor();
        services.AddScoped<IPasswordHasher, BcryptPasswordHasher>();
        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddSingleton<IIntegrationMessageQueue, IntegrationMessageQueue>();
        
        services.AddMassTransit(x =>
        {
            x.AddConsumer<CreateParcelConsumer>();
            x.AddConsumer<AcceptParcelAtDeliveryPointConsumer>();

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(configuration["RabbitMq:Host"] ?? "localhost", "/", h =>
                {
                    h.Username(configuration["RabbitMq:Username"] ?? "guest");
                    h.Password(configuration["RabbitMq:Password"] ?? "guest");
                });
                
                cfg.ReceiveEndpoint("manta.parcel.accept.delivery.point", e =>
                {
                    e.PrefetchCount = 1000;
                    e.ConcurrentMessageLimit = 30; 
                    e.ConfigureConsumer<AcceptParcelAtDeliveryPointConsumer>(context);
                });

                cfg.ReceiveEndpoint("manta.parcel.create.batch", e =>
                {
                    e.PrefetchCount = 1000; 
                    
                    e.ConfigureConsumer<CreateParcelConsumer>(context, c =>
                    {
                        c.Options<BatchOptions>(o =>
                        {
                            o.SetMessageLimit(500);
                            o.SetTimeLimit(TimeSpan.FromSeconds(2));
                        });
                    }); 
                });

                cfg.ConfigureEndpoints(context);
            });
        });

        return services;
    }
}