using Manta.Domain.Entities;

namespace Manta.Infrastructure.Repositories;

public interface IDeliveryPointRepository
{
    Task<DeliveryPoint?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<int> GetNextIdAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<DeliveryPoint>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(DeliveryPoint deliveryPoint, CancellationToken cancellationToken = default);
    Task UpdateAsync(DeliveryPoint deliveryPoint, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default);
    
    Task<DeliveryPoint?> GetByAddressAsync(string address, CancellationToken cancellationToken = default);
    Task<IEnumerable<DeliveryPoint>> SearchByAddressAsync(string addressPart, CancellationToken cancellationToken = default);
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}