using Manta.Domain.Entities;

namespace Manta.Domain.Interfaces;

public interface IDeliveryPointRepository
{
    Task<DeliveryPoint?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<DeliveryPoint>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(DeliveryPoint deliveryPoint, CancellationToken cancellationToken = default);
    Task UpdateAsync(DeliveryPoint deliveryPoint, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
    
    Task<DeliveryPoint?> GetByAddressAsync(string address, CancellationToken cancellationToken = default);
    Task<IEnumerable<DeliveryPoint>> SearchByAddressAsync(string addressPart, CancellationToken cancellationToken = default);
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}