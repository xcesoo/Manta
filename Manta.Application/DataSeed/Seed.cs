using System.Diagnostics;
using Manta.Application.Factories;
using Manta.Application.Interfaces;
using Manta.Application.Services;
using Manta.Domain.CreationOptions;
using Manta.Domain.Entities;
using Manta.Domain.Enums;
using Manta.Domain.Interfaces;
using MediatR;
using Bogus;
using Manta.Application.Commands.DeliveryPoint;
using Manta.Application.Commands.DeliveryVehicle;
using Manta.Application.Commands.Parcel;
using Manta.Application.Commands.User;

namespace Manta.Application.DataSeed;

public class Seed : ISeed
{
    private IDeliveryPointRepository _deliveryPointRepository;
    private IUserRepository _userRepository;
    private IMediator _mediator;

    public Seed(
        IDeliveryPointRepository deliveryPointRepository,
        IUserRepository userRepository,
        IMediator mediator)
    {
        _deliveryPointRepository = deliveryPointRepository;
        _userRepository = userRepository;
        _mediator = mediator;
    }

    public async Task SeedAsync()
    {
        //add system user
        var systemUser = await _userRepository.GetByIdAsync(0)
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
        var deliveryPointRange = await _deliveryPointRepository.GetNextIdAsync() - 1;
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
                
                int? deliveryPointId = role is EUserRole.Cashier ?
                    f.Random.Number(1, deliveryPointRange) :
                    null;
                
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
        var userRange = await _userRepository.GetNextIdAsync() -1;
        //
        timeru.Stop();
        
        var timerp =Stopwatch.StartNew();
        //seed parcels
        var parcelsGenerate = new Faker<CreateParcelCommand>("uk")
            .CustomInstantiator(f => new CreateParcelCommand(
                RecipientName: f.Name.FullName(),
                RecipientPhone: f.Phone.PhoneNumber("+380#########"),
                RecipientEmail: f.Internet.Email(),
                Weight: f.Random.Double(0, 100),
                AmountDue: f.Random.Decimal(0, 10000),
                SenderId: f.Random.Number(1, userRange),
                DeliveryPointId: f.Random.Number(1, deliveryPointRange)
            ));
        var parcelsCommands = parcelsGenerate.Generate(300);
        foreach (var command in parcelsCommands) await _mediator.Send(command);
        //
        timerp.Stop();
        
        Console.WriteLine($"Seeding delivery points {timerdp.Elapsed.TotalSeconds:F2}sec");
        Console.WriteLine($"Seeding delivery vehicles {timerdv.Elapsed.TotalSeconds:F2}sec");
        Console.WriteLine($"Seeding delivery users {timeru.Elapsed.TotalSeconds:F2}sec");
        Console.WriteLine($"Seeding delivery parcels {timerp.Elapsed.TotalSeconds:F2}sec");
    }
}