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
using Manta.Application.Commands.Parcel;
using Manta.Application.Commands.User;
using Manta.Application.Interfaces;
using Manta.Contracts;
using MassTransit;

namespace Manta.Application.DataSeed;

public class Seed : ISeed
{
    private IDeliveryPointRepository _deliveryPointRepository;
    private IUserRepository _userRepository;
    private IParcelRepository _parcelRepository;
    private IMediator _mediator;
    private ICurrentUserService _currentUser;
    private ISendEndpointProvider _sendEndpointProvider;
    public Seed(
        IDeliveryPointRepository deliveryPointRepository,
        IUserRepository userRepository,
        IMediator mediator,
        IParcelRepository parcelRepository,
        ICurrentUserService currentUser,
        ISendEndpointProvider sendEndpointProvider)
    {
        _deliveryPointRepository = deliveryPointRepository;
        _userRepository = userRepository;
        _mediator = mediator;
        _parcelRepository = parcelRepository;
        _currentUser = currentUser;
        _sendEndpointProvider = sendEndpointProvider;
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
        if ((await _deliveryPointRepository.GetAllAsync()).Any()) return;
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

        var timerdv = Stopwatch.StartNew();
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

                Guid? deliveryPointId = role is EUserRole.Cashier
                    ? f.PickRandom(allDeliveryPoints.Select(dp => dp.Id))
                    : null;

                string? deliveryVehicle = role is EUserRole.Driver && unusedVehicles.Count > 0
                    ? unusedVehicles.Dequeue()
                    : null;

                return new CreateUserCommand(
                    Name: f.Name.FullName(),
                    Email: f.Internet.Email(),
                    Password: "Password123!",
                    Role: role,
                    DeliveryPointId: deliveryPointId,
                    VehicleId: deliveryVehicle);
            });
        var usersCommands = usersGenerate.Generate(30);
        foreach (var command in usersCommands) await _mediator.Send(command);
        var allUsers = await _userRepository.GetAllAsync();
        //
        timeru.Stop();

        var timerp = Stopwatch.StartNew();
        //seed parcels
        // _currentUser.SetSystemUser(new Guid("00000000-0000-0000-0000-000000000000"), "system@manta.com", "system",
        //     EUserRole.System);
        // var parcelsGenerate = new Faker<CreateParcelCommand>("uk")
        //     .CustomInstantiator(f => new CreateParcelCommand(
        //         RecipientName: f.Name.FullName(),
        //         RecipientPhone: f.Phone.PhoneNumber("+380#########"),
        //         RecipientEmail: f.Internet.Email(),
        //         Weight: f.Random.Double(0, 100),
        //         AmountDue: f.Random.Decimal(0, 10000),
        //         DeliveryPointId: f.PickRandom(allDeliveryPoints.Select(dp => dp.Id)))
        //     );
        // var parcelsCommands = parcelsGenerate.Generate(500000);
        // // foreach (var command in parcelsCommands) await _mediator.Send(command);
        // var messages = parcelsCommands.Select(c => new CreateParcelMessage(
        //     ParcelId: NewId.NextGuid(),
        //     MessageId: NewId.NextGuid(),
        //     DeliveryPointId: c.DeliveryPointId,
        //     AmountDue: c.AmountDue,
        //     Weight: c.Weight,
        //     RecipientEmail: c.RecipientEmail,
        //     RecipientName: c.RecipientName,
        //     RecipientPhoneNumber: c.RecipientPhone,
        //     CreatedById: _currentUser.UserId,
        //     CreatedByName: _currentUser.UserName,
        //     CreatedByEmail: _currentUser.Email,
        //     CreatedByRole: _currentUser.Role.ToString()
        // )).ToList();
        // var batches = messages.Chunk(500).ToList();
        // // foreach (var batch in batches) await _publishEndpoint.PublishBatch(batch);
        // var endpoint1 = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:manta.parcel.create.batch.1"));
        // var endpoint2 = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:manta.parcel.create.batch.2"));
        // var parallelOptions = new ParallelOptions { MaxDegreeOfParallelism = 20 };
        // int counter = 0;
        // await Parallel.ForEachAsync(batches, parallelOptions,
        //     async (batch, cancellationToken) =>
        //     {
        //         var current = Interlocked.Increment(ref counter);
        //         var targetEndpoint = (current % 2 == 0) ? endpoint1 : endpoint2;
        //         await targetEndpoint.SendBatch(batch, cancellationToken);
        //     });
        _currentUser.SetSystemUser(new Guid("00000000-0000-0000-0000-000000000000"), "system@manta.com", "system",
            EUserRole.System);

        var parcelsGenerate = new Faker<CreateParcelCommand>("uk")
            .CustomInstantiator(f => new CreateParcelCommand(
                RecipientName: f.Name.FullName(),
                RecipientPhone: f.Phone.PhoneNumber("+380#########"),
                RecipientEmail: f.Internet.Email(),
                Weight: f.Random.Double(0, 100),
                AmountDue: f.Random.Decimal(0, 10000),
                DeliveryPointId: f.PickRandom(allDeliveryPoints.Select(dp => dp.Id)))
            );

        var parcelsCommands = parcelsGenerate.Generate(1_000_000);
        
        var parallelOptions = new ParallelOptions { MaxDegreeOfParallelism = 50 };

        await Parallel.ForEachAsync(parcelsCommands, parallelOptions, async (command, cancellationToken) =>
        {
            await _mediator.Send(command, cancellationToken);
        });

        timerp.Stop();
    }
}
// Console.WriteLine($"Seeding delivery points {timerdp.Elapsed.TotalSeconds:F2}sec");
        // Console.WriteLine($"Seeding delivery vehicles {timerdv.Elapsed.TotalSeconds:F2}sec");
        // Console.WriteLine($"Seeding delivery users {timeru.Elapsed.TotalSeconds:F2}sec");
        // Console.WriteLine($"Seeding delivery parcels {timerp.Elapsed.TotalSeconds:F2}sec");