using Manta.Application.Interfaces;
using Manta.Contracts;
using Manta.Domain.Interfaces;
using Manta.Infrastructure.Consumers;
using Manta.Infrastructure.Messaging;
using Manta.Infrastructure.Persistence;
using Manta.Infrastructure.Repositories;
using Manta.Infrastructure.Services;
using Manta.WebApi.Auth;
using MassTransit;
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
            options.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection"), o => o.MaxBatchSize(1000)));
        services.AddScoped<IParcelRepository, ParcelRepository>();
        services.AddScoped<IDeliveryPointRepository, DeliveryPointRepository>();
        services.AddScoped<IDeliveryVehicleRepository, DeliveryVehicleRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IPasswordHasher, BcryptPasswordHasher>();
        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddSingleton<IIntegrationMessageQueue, IntegrationMessageQueue>();
        services.AddHostedService<OutboxBatchProcessor>();
        services.AddHostedService<JanitorWorker>();
        
        services.AddMassTransit(x =>
        {
            x.AddConsumer<CreateParcelConsumer>().ExcludeFromConfigureEndpoints();
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(configuration["RabbitMq:Host"] ?? "localhost", "/", h =>
                {
                    h.Username(configuration["RabbitMq:Username"] ?? "guest");
                    h.Password(configuration["RabbitMq:Password"] ?? "guest");
                    h.ContinuationTimeout(TimeSpan.FromSeconds(60));
                    h.PublisherConfirmation = true; //todo must be true
                });
                cfg.ReceiveEndpoint("manta.parcel.create.batch", e =>
                {
                    e.PrefetchCount = 1000; 
                    e.ConcurrentMessageLimit = 1;
                    
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
