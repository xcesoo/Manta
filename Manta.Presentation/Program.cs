using Manta.Application;
using Manta.Application.Events;
using Manta.Application.Factories;
using Manta.Application.Services;
using Manta.Domain.CreationOptions;
using Manta.Domain.Entities;
using Manta.Domain.Services;
using Manta.Infrastructure;
using Manta.Infrastructure.Persistence;
using Manta.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Manta.Presentation;

public static class Manta
{
    static async Task Main(string[] args)
    {
        // Створення конфігурації
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                // Резервна строка підключення, якщо appsettings.json не знайдено
                ["ConnectionStrings:DefaultConnection"] = "Data Source=MantaDb.db"

            })
            .Build();


        // Налаштування DI контейнера
        var services = new ServiceCollection();
        
        // Реєстрація Application layer (спочатку!)
        services.AddApplication();
        
        // Реєстрація Infrastructure layer
        services.AddInfrastructure(configuration);

        var serviceProvider = services.BuildServiceProvider();

        // Створення бази даних та міграції
        using (var scope = serviceProvider.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<MantaDbContext>();
            
            // Видалення старої бази (якщо потрібно)
            // await dbContext.Database.EnsureDeletedAsync();
            
            // Створення бази даних
            await dbContext.Database.EnsureDeletedAsync();
            await dbContext.Database.EnsureCreatedAsync();
            if (!dbContext.Users.Any(u => u.Id == 0))
            {
                dbContext.Users.Add(SystemUser.Instance);
                await dbContext.SaveChangesAsync();
            }
            
            Console.WriteLine("Database created successfully!");
        }

        // Тестування функціоналу
        await TestApplication(serviceProvider);
    }

    private static async Task TestApplication(ServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        
        var parcelRepo = scope.ServiceProvider.GetRequiredService<IParcelRepository>();
        var deliveryPointRepo = scope.ServiceProvider.GetRequiredService<IDeliveryPointRepository>();
        var vehicleRepo = scope.ServiceProvider.GetRequiredService<IDeliveryVehicleRepository>();
        var deliveryService = scope.ServiceProvider.GetRequiredService<ParcelDeliveryService>();
        var statusService = scope.ServiceProvider.GetRequiredService<ParcelStatusService>();

        // Ініціалізація EventsLoader
        EventsLoader.LoadAllEvents(statusService);

        // Створення тестових даних
        var dp = DeliveryPointFactory.Create(1, "Kyiv");
        var dp2 = DeliveryPointFactory.Create(2, "Lviv");
        
        await deliveryPointRepo.AddAsync(dp);
        await deliveryPointRepo.AddAsync(dp2);
        await deliveryPointRepo.SaveChangesAsync();

        var dv = DeliveryVehicleFactory.Create(new DeliveryVehicleCreationOptions(
            LicensePlate: "AI0000KA",
            CarModel: ("Mercedes", "Sprinter"),
            Capacity: 10));

        await vehicleRepo.AddAsync(dv);
        await vehicleRepo.SaveChangesAsync();

        var parcel = ParcelFactory.Create(new ParcelCreationOptions(
            Id: 1,
            DeliveryPointId: 1,
            AmountDue: 1000m,
            RecipientName: "John",
            RecipientPhoneNumber: "+380987654321",
            RecipientEmail: "popa@gmail.com",
            Weight: 10,
            CreatedBy: SystemUser.Instance));

        var parcel2 = ParcelFactory.Create(new ParcelCreationOptions(
            Id: 2,
            DeliveryPointId: 2,
            AmountDue: 0m,
            RecipientName: "Jane",
            RecipientPhoneNumber: "+380987654322",
            RecipientEmail: "jane@gmail.com",
            Weight: 5,
            CreatedBy: SystemUser.Instance));

        await parcelRepo.AddAsync(parcel);
        await parcelRepo.AddAsync(parcel2);
        await parcelRepo.SaveChangesAsync();

        Console.WriteLine("Test data created successfully!");

        try
        {
            deliveryService.AcceptedAtDeliveryPoint(dp, parcel, SystemUser.Instance);
            await parcelRepo.UpdateAsync(parcel);
            await parcelRepo.SaveChangesAsync();
            
            deliveryService.AcceptedAtDeliveryPoint(dp, parcel2, SystemUser.Instance);
            await parcelRepo.UpdateAsync(parcel2);
            await parcelRepo.SaveChangesAsync();

            Console.WriteLine("Operations completed successfully!");
            
            // Виведення статусів
            var allParcels = await parcelRepo.GetAllAsync();
            foreach (var p in allParcels)
            {
                Console.WriteLine($"Parcel {p.Id}: Status = {p.CurrentStatus.Status}, Location = {p.CurrentLocationDeliveryPointId}");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
        }
    }
}