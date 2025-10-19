
using Manta.Application;
using Manta.Application.DataSeed;
using Manta.Application.Events;
using Manta.Application.Services;
using Manta.Domain.Entities;
using Manta.Domain.Services;
using Manta.Infrastructure;
using Manta.Infrastructure.Persistence;
using Manta.Infrastructure.Repositories;
using Manta.Presentation.Forms;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Manta.Presentation;

static class Program
{
    [STAThread]
    static async Task Main()
    {
        IParcelRepository _parcelRepository;
        IDeliveryPointRepository _deliveryPointRepository;
        IDeliveryVehicleRepository _deliveryVehicleRepository;
        IUserRepository _userRepository;
        ParcelDeliveryService _deliveryService;
        ParcelStatusService _statusService;
        
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
            //await dbContext.Database.EnsureDeletedAsync();
            await dbContext.Database.EnsureCreatedAsync();
            if (!dbContext.Users.Any(u => u.Id == 0))
            {
                dbContext.Users.Add(SystemUser.Instance);
                await dbContext.SaveChangesAsync();
            }

            MessageBox.Show("Database created successfully!", "Manta", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            _parcelRepository = scope.ServiceProvider.GetRequiredService<IParcelRepository>();
            _deliveryPointRepository = scope.ServiceProvider.GetRequiredService<IDeliveryPointRepository>();
            _deliveryVehicleRepository = scope.ServiceProvider.GetRequiredService<IDeliveryVehicleRepository>();
            _userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();
            _deliveryService = scope.ServiceProvider.GetRequiredService<ParcelDeliveryService>();
            _statusService = scope.ServiceProvider.GetRequiredService<ParcelStatusService>();
            //await scope.ServiceProvider.GetRequiredService<Seed>().SeedAsync();
            ApplicationConfiguration.Initialize();
           EventsLoader.LoadAllEvents(_statusService);
           // await _deliveryService.ForceAcceptedAtDeliveryPoint(1, 1, await _userRepository.GetByEmailAsync("kka@manta.com"));
           // await _deliveryService.ForceAcceptedAtDeliveryPoint(1,4, SystemUser.Instance); // TODO ВИДАЛИТИ!!! (для перевірки каси)
           // await _deliveryService.ForceAcceptedAtDeliveryPoint(1,6, SystemUser.Instance); // TODO ВИДАЛИТИ!!! (для перевірки каси)
           // await _deliveryService.ParcelChangeAmountDue(1, 100m);
            
            System.Windows.Forms.Application.Run(new FMain(
                _parcelRepository,
                _deliveryPointRepository,
                _deliveryVehicleRepository,
                _userRepository,
                _deliveryService,
                _statusService));
        }

    }

}
