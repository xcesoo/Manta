using Manta.Domain.Entities;
using Manta.Domain.ValueObjects;

namespace Manta.Infrastructure.Repositories;

public interface IDeliveryVehicleRepository
{
    Task<DeliveryVehicle?> GetByIdAsync(LicensePlate id, CancellationToken cancellationToken = default);
    Task<IEnumerable<DeliveryVehicle>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(DeliveryVehicle vehicle, CancellationToken cancellationToken = default);
    Task UpdateAsync(DeliveryVehicle vehicle, CancellationToken cancellationToken = default);
    Task DeleteAsync(LicensePlate id, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(LicensePlate id, CancellationToken cancellationToken = default);
    
    Task<IEnumerable<DeliveryVehicle>> GetAvailableVehiclesAsync(double requiredCapacity, CancellationToken cancellationToken = default);
    Task<IEnumerable<DeliveryVehicle>> GetByCarModelAsync(string brand, string model, CancellationToken cancellationToken = default);
    Task<IEnumerable<DeliveryVehicle>> GetOverloadedVehiclesAsync(CancellationToken cancellationToken = default);
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}