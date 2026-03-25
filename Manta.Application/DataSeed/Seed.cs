using System.Diagnostics;
using Manta.Application.Factories;
using Manta.Domain.CreationOptions;
using Manta.Domain.Entities;
using Manta.Domain.Enums;
using Manta.Domain.Interfaces;
using MediatR;
using Bogus;
using Manta.Application.Commands.DeliveryPoint;
using Manta.Application.Commands.DeliveryVehicle;
using Manta.Application.Commands.User;
using MassTransit;

namespace Manta.Application.DataSeed;

public class Seed : ISeed
{
    private IDeliveryPointRepository _deliveryPointRepository;
    private IUserRepository _userRepository;
    private IParcelRepository _parcelRepository;
    private IMediator _mediator;

    public Seed(
        IDeliveryPointRepository deliveryPointRepository,
        IUserRepository userRepository,
        IMediator mediator,
        IParcelRepository parcelRepository)
    {
        _deliveryPointRepository = deliveryPointRepository;
        _userRepository = userRepository;
        _mediator = mediator;
        _parcelRepository = parcelRepository;
    }

    public async Task SeedAsync()
    {
        //add system user
        var systemUser = await _userRepository.GetByIdAsync(Guid.Empty)
                         ?? null;
        if (systemUser == null)
        {
            await _userRepository.AddAsync(SystemUser.Instance);
            await _userRepository.SaveChangesAsync();
        }
        
        //check seed db
        if((await _deliveryPointRepository.GetAllAsync()).Any()) return;
        //

        var timerdp = Stopwatch.StartNew();
        //seed delivery points
        var deliveryPointsGenerate = new Faker<CreateDeliveryPointCommand>("uk")
            .CustomInstantiator(f => new CreateDeliveryPointCommand(
                Address: f.Address.FullAddress()
                ));
        var deliveryPointsCommands = deliveryPointsGenerate.Generate(30);
        foreach (var command in deliveryPointsCommands) await _mediator.Send(command);
        var allDeliveryPoints = await _deliveryPointRepository.GetAllAsync();
        //
        timerdp.Stop();

        var timerdv =Stopwatch.StartNew();
        //seed vehicles
        var regionPlates = new[] { "AA", "KA", "BC", "BH", "AE", "BI" };
        var unusedVehicles = new Queue<string>();
        var deliveryVehiclesGenerate = new Faker<CreateDeliveryVehicleCommand>("uk")
            .CustomInstantiator(f => new CreateDeliveryVehicleCommand(
            LicensePlate: $"{f.PickRandom(regionPlates)}{f.Random.Replace("####??")}",
            Brand: f.Vehicle.Manufacturer(),
            Model: f.Vehicle.Model(),
            Capacity: f.Random.Double(500, 1000)
            ));
        var deliveryVehiclesCommands = deliveryVehiclesGenerate.Generate(10);
        foreach (var command in deliveryVehiclesCommands)
        {
            await _mediator.Send(command);
            unusedVehicles.Enqueue(command.LicensePlate);
        }
        //
        timerdv.Stop();

        var timeru = Stopwatch.StartNew();
        //seed users
        var usersRoles = new EUserRole[] { EUserRole.Unknown, EUserRole.Admin, EUserRole.Cashier, EUserRole.Driver };
        var usersGenerate = new Faker<CreateUserCommand>("uk")
            .CustomInstantiator(f =>
            {
                var role = f.PickRandom(usersRoles);
                
                Guid? deliveryPointId = role is EUserRole.Cashier ?
                    f.PickRandom(allDeliveryPoints.Select(dp => dp.Id)) : null;
                
                string? deliveryVehicle  = role is EUserRole.Driver && unusedVehicles.Count > 0 ? 
                    unusedVehicles.Dequeue() : 
                    null;
                
                return new CreateUserCommand(
                    Name: f.Name.FullName(),
                    Email: f.Internet.Email(),
                    Password: "Password123!",
                    Role: role,
                    DeliveryPointId:deliveryPointId,
                    VehicleId: deliveryVehicle);
            });
        var usersCommands = usersGenerate.Generate(30);
        foreach (var command in usersCommands) await _mediator.Send(command);
        var allUsers = await _userRepository.GetAllAsync();
        //
        timeru.Stop();
        
        var timerp =Stopwatch.StartNew();
        //seed parcels
        var parcelsGenerate = new Faker<ParcelCreationOptions>("uk")
            .CustomInstantiator(f => new ParcelCreationOptions(
                Id: NewId.NextGuid(),
                RecipientName: f.Name.FullName(),
                RecipientPhoneNumber: f.Phone.PhoneNumber("+380#########"),
                RecipientEmail: f.Internet.Email(),
                Weight: f.Random.Double(0, 100),
                AmountDue: f.Random.Decimal(0, 10000),
                CreatedBy: f.PickRandom(allUsers),
                DeliveryPointId: f.PickRandom(allDeliveryPoints.Select(dp => dp.Id))
            ));
        var parcelsOptions = parcelsGenerate.Generate(300);
        var parcels = new List<Parcel>();
        foreach (var option in parcelsOptions) parcels.Add(await ParcelFactory.Create(option));
        await _parcelRepository.AddRangeAsync(parcels);
        await _parcelRepository.SaveChangesAsync();
        //
        
        timerp.Stop();
        
        Console.WriteLine($"Seeding delivery points {timerdp.Elapsed.TotalSeconds:F2}sec");
        Console.WriteLine($"Seeding delivery vehicles {timerdv.Elapsed.TotalSeconds:F2}sec");
        Console.WriteLine($"Seeding delivery users {timeru.Elapsed.TotalSeconds:F2}sec");
        Console.WriteLine($"Seeding delivery parcels {timerp.Elapsed.TotalSeconds:F2}sec");
    }
}