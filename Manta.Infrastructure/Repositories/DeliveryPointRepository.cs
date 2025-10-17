using Manta.Domain.Entities;
using Manta.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Manta.Infrastructure.Repositories;

public class DeliveryPointRepository : IDeliveryPointRepository
{
    private readonly MantaDbContext _context;

    public DeliveryPointRepository(MantaDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<DeliveryPoint?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.DeliveryPoints
            .FirstOrDefaultAsync(dp => dp.Id == id, cancellationToken);
    }
    public async Task<int> GetNextIdAsync(CancellationToken cancellationToken = default)
    {
        var maxId = await _context.DeliveryPoints.MaxAsync(dp => (int?)dp.Id, cancellationToken) ?? 0;
        return maxId + 1;
    }   

    public async Task<IEnumerable<DeliveryPoint>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.DeliveryPoints.ToListAsync(cancellationToken);
    }

    public async Task AddAsync(DeliveryPoint deliveryPoint, CancellationToken cancellationToken = default)
    {
        if (deliveryPoint == null)
            throw new ArgumentNullException(nameof(deliveryPoint));

        await _context.DeliveryPoints.AddAsync(deliveryPoint, cancellationToken);
    }

    public Task UpdateAsync(DeliveryPoint deliveryPoint, CancellationToken cancellationToken = default)
    {
        if (deliveryPoint == null)
            throw new ArgumentNullException(nameof(deliveryPoint));

        _context.DeliveryPoints.Update(deliveryPoint);
        return Task.CompletedTask;
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var deliveryPoint = await GetByIdAsync(id, cancellationToken);
        if (deliveryPoint != null)
        {
            _context.DeliveryPoints.Remove(deliveryPoint);
        }
    }

    public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.DeliveryPoints
            .AnyAsync(dp => dp.Id == id, cancellationToken);
    }

    public async Task<DeliveryPoint?> GetByAddressAsync(string address, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(address))
            throw new ArgumentException("Address cannot be null or empty", nameof(address));

        return await _context.DeliveryPoints
            .FirstOrDefaultAsync(dp => dp.Address == address, cancellationToken);
    }

    public async Task<IEnumerable<DeliveryPoint>> SearchByAddressAsync(string addressPart, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(addressPart))
            throw new ArgumentException("Address part cannot be null or empty", nameof(addressPart));

        return await _context.DeliveryPoints
            .Where(dp => dp.Address.Contains(addressPart))
            .ToListAsync(cancellationToken);
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
}