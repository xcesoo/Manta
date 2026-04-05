using Manta.Application.Commands.Parcel;
using Manta.Contracts.Messages;
using Manta.Domain.Entities;
using Manta.Domain.Enums;
using Manta.Domain.Interfaces;
using Manta.Domain.Services;
using Manta.Domain.StatusRules.Context;
using Manta.Domain.StatusRules.Interfaces;
using Manta.Domain.StatusRules.Policies;
using Manta.Domain.ValueObjects;

namespace Manta.Application.Handlers;

public class AcceptParcelAtDeliveryPointHandler(
    IParcelRepository parcelRepository,
    IUserRepository userRepository,
    IDeliveryPointRepository deliveryPointRepository,
    IDeliveryVehicleRepository deliveryVehicleRepository,
    ParcelStatusService statusService,
    LogisticsDomainService logisticsService)
{
    private readonly IParcelRepository _parcelRepository = parcelRepository;
    private readonly IDeliveryPointRepository _deliveryPointRepository = deliveryPointRepository;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IDeliveryVehicleRepository _deliveryVehicleRepository = deliveryVehicleRepository;

    private readonly ParcelStatusService _statusService = statusService;
    private readonly LogisticsDomainService _logisticsService = logisticsService;

    public async Task<Guid> HandleAsync(AcceptParcelAtDeliveryPointMessage msg, CancellationToken ct)
    {
        var parcel = await _parcelRepository.GetByIdAsync(msg.ParcelId, ct) ??
                     throw new ArgumentException($"Parcel {msg.ParcelId} not found");

        var deliveryPoint = await _deliveryPointRepository.GetByIdAsync(msg.DeliveryPointId, ct) ??
                            throw new ArgumentException($"Point {msg.DeliveryPointId} not found");

        var user = await _userRepository.GetByIdAsync(msg.UserId, ct) ??
                   throw new ArgumentException($"User {msg.UserId} not found");

        var context = RuleContext.ForDelivery(parcel, user, deliveryPoint);

        if (!_statusService.ApplyRule<AcceptAtDeliveryPointPolicy>(context))
            throw new ArgumentException("Failed to apply status rule");

        if (parcel.CurrentStatus.Status == EParcelStatus.ReadyForPickup)
            parcel.ChangeArrivedAt(DateTime.UtcNow);

        if (parcel.CurrentVehicleId != null)
        {
            var vehicle = await _deliveryVehicleRepository.GetByIdAsync(parcel.CurrentVehicleId.Value, ct);
            if (vehicle != null)
            {
                _logisticsService.UnloadParcelFromDeliveryVehicle(vehicle, parcel);
                await _deliveryVehicleRepository.UpdateAsync(vehicle, ct);
            }
        }
        parcel.MoveToLocation(deliveryPoint.Id);
        await _parcelRepository.UpdateAsync(parcel, ct);

        return parcel.Id;
    }
}