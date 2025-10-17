using Manta.Domain.Enums;
using Manta.Domain.StatusRules.Interfaces;
using Manta.Domain.ValueObjects;

namespace Manta.Domain.StatusRules.Implementations;

public sealed class TransitRule: IParcelStatusRule
{
    public RuleResult ShouldApply(RuleContext context) => context switch
    {
        {Parcel:{CurrentVehicleId: var currentVehicleId, CurrentStatus:{Status: 
                    EParcelStatus.Processing or 
                    EParcelStatus.ReaddressRequested or 
                    EParcelStatus.WrongLocation}}, 
            DeliveryVehicle:{Id: var vehicleId}} 
            when currentVehicleId != vehicleId =>
            RuleResult.Ok(EParcelStatus.InTransit),
        
        {Parcel:{CurrentStatus:{Status: 
                EParcelStatus.ReturnRequested or 
                EParcelStatus.StorageExpired or 
                EParcelStatus.ShipmentCancelled}, CurrentVehicleId: var currentVehicleId},
                DeliveryVehicle:{Id: var vehicleId}}
            when currentVehicleId != vehicleId =>
            RuleResult.Ok(EParcelStatus.InReturnTransit),
                
        _ => RuleResult.Failed(
            ERuleResultError.Unknown, 
            $"Failed to load a parcel.")
    };
}