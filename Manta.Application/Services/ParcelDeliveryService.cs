using System.Data;
using Manta.Domain.Entities;
using Manta.Domain.Events;
using Manta.Domain.Services;
using Manta.Domain.StatusRules;
using Manta.Domain.StatusRules.Implementations;
using Manta.Domain.StatusRules.Policies;
using Manta.Domain.ValueObjects;
using Manta.Infrastructure.EventDispatcher;
using Manta.Infrastructure.Repositories;

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

    public async Task PayForParcel(int parcelId)
    {
        var parcel = await _parcelRepository.GetByIdAsync(parcelId) ?? throw new ArgumentException($"Parcel with id {parcelId} not found.");
        if(parcel.Paid) return;
        parcel.Pay();
    }
    public async Task DeliverParcel(int parcelId, User changeBy)
    {
        var parcel = await _parcelRepository.GetByIdAsync(parcelId) ?? throw new ArgumentException($"Parcel with id {parcelId} not found.");
        var context = RuleContext.ForParcel(parcel, changeBy);
        
        if (_statusService.ApplyRule<DeliveredRule>(context))
        {
            DomainEvents.Raise(new ParcelDeliveredEvent(parcel, changeBy));
            await _parcelRepository.UpdateAsync(parcel);
            await _parcelRepository.SaveChangesAsync();
        }
        else throw new ArgumentException($"Failed to deliver the parcel {nameof(parcel)}");
    }

    public async Task AcceptedAtDeliveryPoint(int deliveryPointId, int parcelId, User changedBy)
    {
        var parcel = await _parcelRepository.GetByIdAsync(parcelId) ?? throw new ArgumentException($"Parcel with id {parcelId} not found.");
        var deliveryPoint = await _deliveryPointRepository.GetByIdAsync(deliveryPointId) ?? throw new ArgumentException($"DeliveryPoint with id {deliveryPointId} not found.");
        var context = RuleContext.ForDelivery(parcel, changedBy, deliveryPoint);
        if (_statusService.ApplyRule<AcceptAtDeliveryPointPolicy>(context))
        {
            if (parcel.CurrentVehicleId != null) await UnloadFromDeliveryVehicle(parcel.CurrentVehicleId, parcel.Id, changedBy);
            parcel.MoveToLocation(deliveryPoint.Id);
            DomainEvents.Raise(new ParcelAddedToDeliveryPointEvent(parcel, deliveryPoint, changedBy));
            await _parcelRepository.UpdateAsync(parcel);
            await _parcelRepository.SaveChangesAsync();
        }
        else throw new ArgumentException("Failed to accept the parcel", nameof(parcel));
    }

    public async Task ReaddressParcel(int deliveryPointId, int parcelId, User changedBy)
    {
        var parcel = await _parcelRepository.GetByIdAsync(parcelId) ?? throw new ArgumentException($"Parcel with id {parcelId} not found.");
        var deliveryPoint = await _deliveryPointRepository.GetByIdAsync(deliveryPointId) ?? throw new ArgumentException($"DeliveryPoint with id {deliveryPointId} not found.");
        var context = RuleContext.ForDelivery(parcel, changedBy, deliveryPoint);
        if (CanReaddressParcel(context))
        {
            parcel.Readdress(deliveryPoint.Id);
            await _parcelRepository.UpdateAsync(parcel);
            await _parcelRepository.SaveChangesAsync();
        }
        else throw new ArgumentException("Failed to readdress the parcel", nameof(parcel));
    }

    public async Task ReaddressParcel(int parcelId, User changedBy)
    {
        var parcel = await _parcelRepository.GetByIdAsync(parcelId) ?? throw new ArgumentException($"Parcel with id {parcelId} not found.");
        var context = RuleContext.ForParcel(parcel, changedBy);
        if (CanReaddressParcel(context))
        {
            parcel.Readdress(parcel.DeliveryPointId);
            await _parcelRepository.UpdateAsync(parcel);
            await _parcelRepository.SaveChangesAsync();
        }
        else throw new ArgumentException("Failed to readdress the parcel", nameof(parcel));
    }

    private bool CanReaddressParcel(RuleContext context) => _statusService.ApplyRule<ReaddressRequestedRule>(context);

    public async Task LoadInDeliveryVehicle(LicensePlate deliveryVehicleId, int parcelId, User changeBy)
    {
        var parcel = await _parcelRepository.GetByIdAsync(parcelId) ?? throw new ArgumentException($"Parcel with id {parcelId} not found.");
        var deliveryVehicle = await _deliveryVehicleRepository.GetByIdAsync(deliveryVehicleId) ?? throw new ArgumentException($"DeliveryVehicle with id {deliveryVehicleId} not found.");
        var context = RuleContext.ForVehicle(parcel,changeBy, deliveryVehicle);
        if (_statusService.ApplyRule<TransitRule>(context))
        {
            deliveryVehicle.LoadParcel(parcel.Id, parcel.Weight);
            parcel.ChangeDeliveryVehicle(deliveryVehicle.Id);
            parcel.MoveToLocation(null);
            await _deliveryVehicleRepository.UpdateAsync(deliveryVehicle);
            await _deliveryVehicleRepository.SaveChangesAsync();
            await _parcelRepository.UpdateAsync(parcel);
            await _parcelRepository.SaveChangesAsync();
        }
    }

    public async Task UnloadFromDeliveryVehicle(LicensePlate deliveryVehicleId, int parcelId, User changeBy)
    {
        var parcel = await _parcelRepository.GetByIdAsync(parcelId) ?? throw new ArgumentException($"Parcel with id {parcelId} not found.");
        var deliveryVehicle = await _deliveryVehicleRepository.GetByIdAsync(deliveryVehicleId) ?? throw new ArgumentException($"DeliveryVehicle with id {deliveryVehicleId} not found.");
        deliveryVehicle.UnloadParcel(parcel.Id, parcel.Weight);
        parcel.ChangeDeliveryVehicle(null);
        await _deliveryVehicleRepository.UpdateAsync(deliveryVehicle);
        await _deliveryVehicleRepository.SaveChangesAsync();
        await _parcelRepository.UpdateAsync(parcel);
        await _parcelRepository.SaveChangesAsync();
    }

    public async Task CancelParcel(int parcelId, User cancelledBy)
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

    public async Task ReturnRequestParcels(User changedBy, params int[] parcelIds)
    {
        foreach (var parcelId in parcelIds)
        {
            var parcel = await _parcelRepository.GetByIdAsync(parcelId) ??
                         throw new ArgumentException($"Parcel with id {parcelId} not found.");
            var context = RuleContext.ForParcel(parcel, changedBy);
            if(_statusService.ApplyRule<ReturnRequestedRule>(context))
            {
                await _parcelRepository.UpdateAsync(parcel);
                await _parcelRepository.SaveChangesAsync();
            }
        }
    }

    public async Task ReturnParcel(User changedBy, params int[] parcelIds)
    {
        foreach (var parcelId in parcelIds)
        {
            var parcel = await _parcelRepository.GetByIdAsync(parcelId) ??
                         throw new ArgumentException($"Parcel with id {parcelId} not found.");
            var context = RuleContext.ForParcel(parcel, changedBy);
            if(_statusService.ApplyRule<ReturnedRule>(context))
            {
                if (parcel.CurrentVehicleId != null) await UnloadFromDeliveryVehicle(parcel.CurrentVehicleId, parcel.Id, changedBy);
                await _parcelRepository.UpdateAsync(parcel);
                await _parcelRepository.SaveChangesAsync();
            }
        }
    }

}