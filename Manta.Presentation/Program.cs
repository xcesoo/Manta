using Manta.Application;
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
        var userRepo = scope.ServiceProvider.GetRequiredService<IUserRepository>();
        var deliveryService = scope.ServiceProvider.GetRequiredService<ParcelDeliveryService>();
        var statusService = scope.ServiceProvider.GetRequiredService<ParcelStatusService>();

        // Ініціалізація EventsLoader
        EventsLoader.LoadAllEvents(statusService);


        // Створення тестових даних
        await DeliveryVehicleFactory.Create(new DeliveryVehicleCreationOptions(
            LicensePlate: "AI0000KA",
            CarModel: ("Mercedes", "Sprinter"),
            Capacity: 10000), vehicleRepo);

        await DeliveryPointFactory.Create(new DeliveryPointCreationOptions(Address: "Kyiv"), deliveryPointRepo);
        await DeliveryPointFactory.Create(new DeliveryPointCreationOptions(Address: "Lviv"), deliveryPointRepo);
        
        var admin = await UserFactory.Create<Admin>(new UserCreationOptions(
            Name: "Кирило Карпета Андрійович", Email: "notxceso@gmail.com"), userRepo);
        var driver = await UserFactory.Create<Driver>(new UserCreationOptions(
            Name: "Кривовух Микола Потапович", Email: "krivovuh@gmail.com", VehicleId: "AI0000KA"), userRepo);
        var cashier = await UserFactory.Create<Cashier>(new UserCreationOptions(
            Name: "Петрик Микола Андрійович", Email: "petryk@gmail.com", DeliveryPointId: 1), userRepo);

        await ParcelFactory.Create(new ParcelCreationOptions(
            DeliveryPointId: 1, AmountDue: 52m, Weight: 4,
            RecipientName: "Гаврилюк Олександр Сергійович",
            RecipientPhoneNumber: "+380931234567",
            RecipientEmail: "oleksandr.gavryliuk@gmail.com",
            CreatedBy: SystemUser.Instance), parcelRepo);

        await ParcelFactory.Create(new ParcelCreationOptions(
            DeliveryPointId: 2, AmountDue: 0m, Weight: 5,
            RecipientName: "Бондар Вікторія Ігорівна",
            RecipientPhoneNumber: "+380961234568",
            RecipientEmail: "victoria.bondar@gmail.com",
            CreatedBy: SystemUser.Instance), parcelRepo);

        await ParcelFactory.Create(new ParcelCreationOptions(
            DeliveryPointId: 2, AmountDue: 75m, Weight: 3,
            RecipientName: "Лисенко Валерій Павлович",
            RecipientPhoneNumber: "+380671987654",
            RecipientEmail: "valeriy.lysenko@gmail.com",
            CreatedBy: SystemUser.Instance), parcelRepo);

        await ParcelFactory.Create(new ParcelCreationOptions(
            DeliveryPointId: 1, AmountDue: 12m, Weight: 10,
            RecipientName: "Остапчук Олена Василівна",
            RecipientPhoneNumber: "+380981122233",
            RecipientEmail: "olena.ostapchuk@gmail.com",
            CreatedBy: SystemUser.Instance), parcelRepo);

        await ParcelFactory.Create(new ParcelCreationOptions(
            DeliveryPointId: 2, AmountDue: 100m, Weight: 6,
            RecipientName: "Мельник Іван Михайлович",
            RecipientPhoneNumber: "+380731234559",
            RecipientEmail: "ivan.melnyk@gmail.com",
            CreatedBy: SystemUser.Instance), parcelRepo);

        await ParcelFactory.Create(new ParcelCreationOptions(
            DeliveryPointId: 1, AmountDue: 80m, Weight: 9,
            RecipientName: "Петренко Анна Олегівна",
            RecipientPhoneNumber: "+380992233445",
            RecipientEmail: "anna.petrenko@gmail.com",
            CreatedBy: SystemUser.Instance), parcelRepo);

        await ParcelFactory.Create(new ParcelCreationOptions(
            DeliveryPointId: 2, AmountDue: 0m, Weight: 3,
            RecipientName: "Шевчук Андрій Васильович",
            RecipientPhoneNumber: "+380961119988",
            RecipientEmail: "andriy.shevchuk@gmail.com",
            CreatedBy: SystemUser.Instance), parcelRepo);

        await ParcelFactory.Create(new ParcelCreationOptions(
            DeliveryPointId: 2, AmountDue: 56m, Weight: 7,
            RecipientName: "Зубко Юлія Іванівна",
            RecipientPhoneNumber: "+380631234578",
            RecipientEmail: "yulia.zubko@gmail.com",
            CreatedBy: SystemUser.Instance), parcelRepo);

        await ParcelFactory.Create(new ParcelCreationOptions(
            DeliveryPointId: 1, AmountDue: 10m, Weight: 8,
            RecipientName: "Сидоренко Богдан Петрович",
            RecipientPhoneNumber: "+380991772233",
            RecipientEmail: "bogdan.sydorenko@gmail.com",
            CreatedBy: SystemUser.Instance), parcelRepo);

        await ParcelFactory.Create(new ParcelCreationOptions(
            DeliveryPointId: 1, AmountDue: 130m, Weight: 12,
            RecipientName: "Романюк Марія Вікторівна",
            RecipientPhoneNumber: "+380671928374",
            RecipientEmail: "maria.romanyuk@gmail.com",
            CreatedBy: SystemUser.Instance), parcelRepo);

        await ParcelFactory.Create(new ParcelCreationOptions(
            DeliveryPointId: 2, AmountDue: 0m, Weight: 4,
            RecipientName: "Коваль Денис Павлович",
            RecipientPhoneNumber: "+380961210123",
            RecipientEmail: "denys.koval@gmail.com",
            CreatedBy: SystemUser.Instance), parcelRepo);

        await ParcelFactory.Create(new ParcelCreationOptions(
            DeliveryPointId: 2, AmountDue: 0m, Weight: 4,
            RecipientName: "Коваль Денис Павлович",
            RecipientPhoneNumber: "+380961210123",
            RecipientEmail: "denys.koval@gmail.com",
            CreatedBy: SystemUser.Instance), parcelRepo);

        await ParcelFactory.Create(new ParcelCreationOptions(
            DeliveryPointId: 2, AmountDue: 67m, Weight: 8,
            RecipientName: "Гринюк Валентина Сергіївна",
            RecipientPhoneNumber: "+380931234589",
            RecipientEmail: "valentyna.grynyuk@gmail.com",
            CreatedBy: SystemUser.Instance), parcelRepo);

        await ParcelFactory.Create(new ParcelCreationOptions(
            DeliveryPointId: 1, AmountDue: 49m, Weight: 11,
            RecipientName: "Гончарук Олексій Романович",
            RecipientPhoneNumber: "+380732112233",
            RecipientEmail: "oleksiy.goncharuk@gmail.com",
            CreatedBy: SystemUser.Instance), parcelRepo);

        await ParcelFactory.Create(new ParcelCreationOptions(
            DeliveryPointId: 2, AmountDue: 98m, Weight: 7,
            RecipientName: "Бойко Ольга Василівна",
            RecipientPhoneNumber: "+380672334455",
            RecipientEmail: "olga.boyko@gmail.com",
            CreatedBy: SystemUser.Instance), parcelRepo);

        await ParcelFactory.Create(new ParcelCreationOptions(
            DeliveryPointId: 1, AmountDue: 120m, Weight: 6,
            RecipientName: "Ткаченко Лілія Миколаївна",
            RecipientPhoneNumber: "+380631234599",
            RecipientEmail: "liliya.tkachenko@gmail.com",
            CreatedBy: SystemUser.Instance), parcelRepo);

        await ParcelFactory.Create(new ParcelCreationOptions(
            DeliveryPointId: 1, AmountDue: 32m, Weight: 5,
            RecipientName: "Корнієнко Євген Вікторович",
            RecipientPhoneNumber: "+380961234577",
            RecipientEmail: "yevhen.korniienko@gmail.com",
            CreatedBy: SystemUser.Instance), parcelRepo);

        await ParcelFactory.Create(new ParcelCreationOptions(
            DeliveryPointId: 2, AmountDue: 58m, Weight: 10,
            RecipientName: "Кравченко Артем Ігорович",
            RecipientPhoneNumber: "+380991234588",
            RecipientEmail: "artem.kravchenko@gmail.com",
            CreatedBy: SystemUser.Instance), parcelRepo);

        await ParcelFactory.Create(new ParcelCreationOptions(
            DeliveryPointId: 1, AmountDue: 0m, Weight: 9,
            RecipientName: "Демчук Ірина Костянтинівна",
            RecipientPhoneNumber: "+380671234589",
            RecipientEmail: "iryna.demchuk@gmail.com",
            CreatedBy: SystemUser.Instance), parcelRepo);

        await ParcelFactory.Create(new ParcelCreationOptions(
            DeliveryPointId: 2, AmountDue: 47m, Weight: 7,
            RecipientName: "Степанюк Аркадій Віталійович",
            RecipientPhoneNumber: "+380631234123",
            RecipientEmail: "arkadiy.stepanyuk@gmail.com",
            CreatedBy: SystemUser.Instance), parcelRepo);



        Console.WriteLine("Test data created successfully!");

        try
        {
            await deliveryService.LoadInDeliveryVehicle("AI0000KA", 1, SystemUser.Instance);
            await deliveryService.LoadInDeliveryVehicle("AI0000KA", 2, SystemUser.Instance);
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
            await deliveryService.LoadInDeliveryVehicle("AI0000KA", 20, driver!);
            await deliveryService.AcceptedAtDeliveryPoint(2, 20, SystemUser.Instance);
            await deliveryService.PayForParcel(20);
            await deliveryService.DeliverParcel(20, cashier!);

            // await deliveryService.DeliverParcel(1, SystemUser.Instance);
            //
            // await deliveryService.DeliverParcel(2, SystemUser.Instance);
            
            var popa = await parcelRepo.GetByIdAsync(20);
            Console.WriteLine("Operations completed successfully!");
            var popa2 = await parcelRepo.GetByIdAsync(2);

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