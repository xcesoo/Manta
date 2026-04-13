using Manta.Application.Commands.Parcel;
using Manta.Application.Services;
using Manta.Contracts.Messages;
using Manta.Domain.Entities;
using Manta.Domain.Enums;
using Manta.Domain.Interfaces;
using Manta.Domain.Services;
using Manta.Domain.StatusRules.Context;
using Manta.Domain.StatusRules.Interfaces;
using Manta.Domain.StatusRules.Policies;
using Manta.Domain.ValueObjects;
using Microsoft.Extensions.Logging;

namespace Manta.Application.Handlers;

public class AcceptParcelAtDeliveryPointHandler(
    IParcelRepository parcelRepository,
    IUserRepository userRepository,
    IDeliveryPointRepository deliveryPointRepository,
    IDeliveryVehicleRepository deliveryVehicleRepository,
    ParcelStatusService statusService,
    LogisticsDomainService logisticsService,
    ILogger<AcceptParcelAtDeliveryPointHandler> logger)
{
    private readonly IParcelRepository _parcelRepository = parcelRepository;
    private readonly IDeliveryPointRepository _deliveryPointRepository = deliveryPointRepository;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IDeliveryVehicleRepository _deliveryVehicleRepository = deliveryVehicleRepository;

    private readonly ParcelStatusService _statusService = statusService;
    private readonly LogisticsDomainService _logisticsService = logisticsService;
    
    private readonly ILogger<AcceptParcelAtDeliveryPointHandler> _logger = logger;

    public async Task<Guid> HandleAsync(AcceptParcelAtDeliveryPointMessage msg, CancellationToken ct)
    {
        var parcel = await _parcelRepository.GetByIdAsync(msg.ParcelId, ct) ??
                     throw new ArgumentException($"Parcel {msg.ParcelId} not found");

        var deliveryPoint = await _deliveryPointRepository.GetByIdAsync(msg.DeliveryPointId, ct) ??
                            throw new ArgumentException($"Point {msg.DeliveryPointId} not found");

        var user = await _userRepository.GetByIdAsync(msg.UserId, ct) ??
                   throw new ArgumentException($"User {msg.UserId} not found");
        
        int activeCount = await _parcelRepository.GetActiveParcelsCountAtPointAsync(msg.DeliveryPointId, ct);

        var context = RuleContext.ForDelivery(parcel, user, deliveryPoint, activeCount);

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

        _logger.LogInformation(
            "Касир {MaskedName} ({MaskedEmail}) приймає посилку {ParcelId} на відділенні {PointId}",
            PiiMasker.MaskName(user.Name),
            PiiMasker.MaskEmail(user.Email),
            parcel.Id,
            deliveryPoint.Id);
        
        return parcel.Id;
    }
}