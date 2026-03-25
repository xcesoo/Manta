using Manta.Domain.Entities;
using Manta.Domain.Enums;
using Manta.Domain.Interfaces;
using Manta.Domain.Services;
using Manta.Domain.StatusRules;
using Manta.Domain.StatusRules.Context;
using Manta.Domain.StatusRules.Implementations;
using Manta.Domain.StatusRules.Policies;
using Manta.Domain.ValueObjects;

namespace Manta.Application.Services;

public class ParcelDeliveryService
{
    private readonly ParcelStatusService _statusService;
    private IParcelRepository _parcelRepository;
    private IDeliveryPointRepository _deliveryPointRepository;
    private IDeliveryVehicleRepository _deliveryVehicleRepository;
    private IUserRepository _userRepository;

    public ParcelDeliveryService(
        ParcelStatusService statusService,
        IParcelRepository parcelRepository,
        IDeliveryPointRepository deliveryPointRepository,
        IDeliveryVehicleRepository deliveryVehicleRepository,
        IUserRepository userRepository)
    {
        _statusService = statusService;
        _parcelRepository = parcelRepository;
        _deliveryPointRepository = deliveryPointRepository;
        _deliveryVehicleRepository = deliveryVehicleRepository;
        _userRepository = userRepository;
    }

    public async Task PayForParcel(Guid parcelId)
    {
        var parcel = await _parcelRepository.GetByIdAsync(parcelId) ??
                     throw new ArgumentException($"Parcel with id {parcelId} not found.");
        if (parcel.Paid) return;
        parcel.Pay();
    }

    public async Task DeliverParcel(Guid parcelId, UserInfo changeBy)
    {
        var parcel = await _parcelRepository.GetByIdAsync(parcelId) ??
                     throw new ArgumentException($"Parcel with id {parcelId} not found.");
        var context = RuleContext.ForParcel(parcel, changeBy);

        if (_statusService.ApplyRule<DeliveredRule>(context))
        {
            await _parcelRepository.UpdateAsync(parcel);
            await _parcelRepository.SaveChangesAsync();
        }
        else throw new ArgumentException($"Failed to deliver the parcel {nameof(parcel)}");
    }

    public async Task<Guid> AcceptedAtDeliveryPoint(Guid deliveryPointId, Guid parcelId, UserInfo changedBy)
    {
        var parcel = await _parcelRepository.GetByIdAsync(parcelId) ??
                     throw new ArgumentException($"Parcel with id {parcelId} not found.");
        var deliveryPoint = await _deliveryPointRepository.GetByIdAsync(deliveryPointId) ??
                            throw new ArgumentException($"DeliveryPoint with id {deliveryPointId} not found.");
        var context = RuleContext.ForDelivery(parcel, changedBy, deliveryPoint);
        if (_statusService.ApplyRule<AcceptAtDeliveryPointPolicy>(context))
        {
            if (parcel.CurrentStatus.Status == EParcelStatus.ReadyForPickup)
            {
                parcel.ChangeArrivedAt(parcel.CurrentStatus.ChangedAt);
            }
            if (parcel.CurrentVehicleId != null)
                await UnloadFromDeliveryVehicle(parcel.CurrentVehicleId, parcel.Id, changedBy);
            parcel.MoveToLocation(deliveryPoint.Id);
            await _parcelRepository.UpdateAsync(parcel);
            await _parcelRepository.SaveChangesAsync();
            return parcel.Id;
        }
        else throw new ArgumentException("Failed to accept the parcel", nameof(parcel));
    }

    public async Task ReaddressParcel(Guid deliveryPointId, Guid parcelId, UserInfo changedBy)
    {
        var parcel = await _parcelRepository.GetByIdAsync(parcelId) ??
                     throw new ArgumentException($"Parcel with id {parcelId} not found.");
        var deliveryPoint = await _deliveryPointRepository.GetByIdAsync(deliveryPointId) ??
                            throw new ArgumentException($"DeliveryPoint with id {deliveryPointId} not found.");
        var context = RuleContext.ForDelivery(parcel, changedBy, deliveryPoint);
        if (CanReaddressParcel(context))
        {
            parcel.Readdress(deliveryPoint.Id);
            await _parcelRepository.UpdateAsync(parcel);
            await _parcelRepository.SaveChangesAsync();
        }
        else throw new ArgumentException("Failed to readdress the parcel", nameof(parcel));
    }

    public async Task ReaddressParcel(Guid parcelId, UserInfo changedBy)
    {
        var parcel = await _parcelRepository.GetByIdAsync(parcelId) ??
                     throw new ArgumentException($"Parcel with id {parcelId} not found.");
        var context = RuleContext.ForParcel(parcel, changedBy);
        if (CanReaddressParcel(context))
        {
            parcel.Readdress(parcel.DeliveryPointId);
            await _parcelRepository.UpdateAsync(parcel);
            await _parcelRepository.SaveChangesAsync();
        }
        else throw new ArgumentException("Failed to readdress the parcel", nameof(parcel));
    }
    public async Task ParcelChangeAmountDue(Guid parcelId, decimal amountDue)
    {
        if (amountDue < 0) throw new ArgumentException("Amount due must be positive", nameof(amountDue));
        var parcel = await _parcelRepository.GetByIdAsync(parcelId) ??
                     throw new ArgumentException($"Parcel with id {parcelId} not found.");
        parcel.ChangeAmountDue(amountDue);
    }

    private bool CanReaddressParcel(RuleContext context) => _statusService.ApplyRule<ReaddressRequestedRule>(context);

    public async Task LoadInDeliveryVehicle(LicensePlate deliveryVehicleId, Guid parcelId, UserInfo changeBy)
    {
        var parcel = await _parcelRepository.GetByIdAsync(parcelId) ??
                     throw new ArgumentException($"Parcel with id {parcelId} not found.");
        var deliveryVehicle = await _deliveryVehicleRepository.GetByLicensePlateIdAsync(deliveryVehicleId) ??
                              throw new ArgumentException($"DeliveryVehicle with id {deliveryVehicleId} not found.");
        var context = RuleContext.ForVehicle(parcel, changeBy, deliveryVehicle);
        if (_statusService.ApplyRule<TransitRule>(context))
        {
            deliveryVehicle.LoadParcel(parcel.Id, parcel.Weight);
            parcel.ChangeDeliveryVehicle(deliveryVehicle.LicensePlateId);
            await _deliveryVehicleRepository.UpdateAsync(deliveryVehicle);
            await _deliveryVehicleRepository.SaveChangesAsync();
            await _parcelRepository.UpdateAsync(parcel);
            await _parcelRepository.SaveChangesAsync();
        }
    }

    public async Task UnloadFromDeliveryVehicle(LicensePlate deliveryVehicleId, Guid parcelId, UserInfo changeBy)
    {
        var parcel = await _parcelRepository.GetByIdAsync(parcelId) ??
                     throw new ArgumentException($"Parcel with id {parcelId} not found.");
        var deliveryVehicle = await _deliveryVehicleRepository.GetByLicensePlateIdAsync(deliveryVehicleId) ??
                              throw new ArgumentException($"DeliveryVehicle with id {deliveryVehicleId} not found.");
        deliveryVehicle.UnloadParcel(parcel.Id, parcel.Weight);
        parcel.ChangeDeliveryVehicle(null);
        await _deliveryVehicleRepository.UpdateAsync(deliveryVehicle);
        await _deliveryVehicleRepository.SaveChangesAsync();
        await _parcelRepository.UpdateAsync(parcel);
        await _parcelRepository.SaveChangesAsync();
    }

    public async Task CancelParcel(Guid parcelId, UserInfo cancelledBy)
    {
        var parcel = await _parcelRepository.GetByIdAsync(parcelId) ??
                     throw new ArgumentException($"Parcel with id {parcelId} not found.");
        var context = RuleContext.ForParcel(parcel, cancelledBy);
        if (_statusService.ApplyRule<ShipmentCancelledRule>(context))
        {
            await _parcelRepository.UpdateAsync(parcel);
            await _parcelRepository.SaveChangesAsync();
        }
    }

    public async Task ReturnRequestParcels(UserInfo changedBy, params Guid[] parcelIds)
    {
        foreach (var parcelId in parcelIds)
        {
            var parcel = await _parcelRepository.GetByIdAsync(parcelId) ??
                         throw new ArgumentException($"Parcel with id {parcelId} not found.");
            var context = RuleContext.ForParcel(parcel, changedBy);
            if (_statusService.ApplyRule<ReturnRequestedRule>(context))
            {
                await _parcelRepository.UpdateAsync(parcel);
                await _parcelRepository.SaveChangesAsync();
            }
        }
    }

    public async Task ReturnParcel(UserInfo changedBy, params Guid[] parcelIds)
    {
        foreach (var parcelId in parcelIds)
        {
            var parcel = await _parcelRepository.GetByIdAsync(parcelId) ??
                         throw new ArgumentException($"Parcel with id {parcelId} not found.");
            var context = RuleContext.ForParcel(parcel, changedBy);
            if (_statusService.ApplyRule<ReturnedRule>(context))
            {
                if (parcel.CurrentVehicleId != null)
                    await UnloadFromDeliveryVehicle(parcel.CurrentVehicleId, parcel.Id, changedBy);
                parcel.MoveToLocation(null);
                await _parcelRepository.UpdateAsync(parcel);
                await _parcelRepository.SaveChangesAsync();
            }
        }
    }
}