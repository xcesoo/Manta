using Manta.Application;
using Manta.Application.DataSeed;
using Manta.Application.Events;
using Manta.Application.Factories;
using Manta.Application.Services;
using Manta.Domain.CreationOptions;
using Manta.Domain.Entities;
using Manta.Domain.Services;
using Manta.Domain.ValueObjects;
using Manta.Infrastructure;
using Manta.Infrastructure.Persistence;
using Manta.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Manta.Presentation;

public static class Manta
{
    static async Task Main()
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

        // Реєстрація Application layer
        services.AddApplication();

        // Реєстрація Infrastructure layer
        services.AddInfrastructure(configuration);

        var serviceProvider = services.BuildServiceProvider();

        // Створення бази даних
        using (var scope = serviceProvider.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<MantaDbContext>();

            // Видалення старої бази
            // Створення бази даних
            // await dbContext.Database.EnsureDeletedAsync();
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
        var userRepo = scope.ServiceProvider.GetRequiredService<IUserRepository>();
        var deliveryService = scope.ServiceProvider.GetRequiredService<ParcelDeliveryService>();
        var statusService = scope.ServiceProvider.GetRequiredService<ParcelStatusService>();
        // await scope.ServiceProvider.GetRequiredService<Seed>().SeedAsync();
        var par = await parcelRepo.GetByIdAsync(1);
        // Ініціалізація EventsLoader
        EventsLoader.LoadAllEvents(statusService);
        // await dataSeed.SeedAsync();

        Console.WriteLine("Test data created successfully!");

        try
        {
            await deliveryService.LoadInDeliveryVehicle("AI0000KA", 1, SystemUser.Instance);
            await deliveryService.LoadInDeliveryVehicle("AI0000KA", 2, SystemUser.Instance);
            await deliveryService.UnloadFromDeliveryVehicle("AI0000KA", 3, SystemUser.Instance);
            await deliveryService.AcceptedAtDeliveryPoint(1, 1, SystemUser.Instance);
            await deliveryService.AcceptedAtDeliveryPoint(1, 2, SystemUser.Instance);
            await deliveryService.ReaddressParcel(2, SystemUser.Instance);
            await deliveryService.LoadInDeliveryVehicle("AI0000KA", 2, SystemUser.Instance);
            await deliveryService.AcceptedAtDeliveryPoint(2, 2, SystemUser.Instance);
            await deliveryService.CancelParcel(1, SystemUser.Instance);
            await deliveryService.CancelParcel(2, SystemUser.Instance);
            await deliveryService.ReturnRequestParcels(SystemUser.Instance, 1, 2);
            await deliveryService.LoadInDeliveryVehicle("AI0000KA", 1, SystemUser.Instance);
            await deliveryService.LoadInDeliveryVehicle("AI0000KA", 2, SystemUser.Instance);
            await deliveryService.ReturnParcel(SystemUser.Instance, 1, 2);
            // await deliveryService.LoadInDeliveryVehicle("AI0000KA", 20, driver1!);
            await deliveryService.AcceptedAtDeliveryPoint(2, 20, SystemUser.Instance);
            await deliveryService.PayForParcel(20);
            // await deliveryService.DeliverParcel(20, cashier1!);

            // await deliveryService.DeliverParcel(1, SystemUser.Instance);
            //
            // await deliveryService.DeliverParcel(2, SystemUser.Instance);

            var popa = await parcelRepo.GetAllAsync();
            Console.WriteLine("Operations completed successfully!");

            // Виведення статусів
            var allParcels = await parcelRepo.GetAllAsync();
            foreach (var p in allParcels)
            {
                Console.WriteLine(
                    $"Parcel {p.Id}: Status = {p.CurrentStatus.Status}, Location = {p.CurrentLocationDeliveryPointId}");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error: {e.Message}");
        }
    }
}