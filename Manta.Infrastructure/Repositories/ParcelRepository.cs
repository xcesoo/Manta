using Manta.Domain.Entities;
using Manta.Domain.Enums;
using Manta.Domain.ValueObjects;
using Manta.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Manta.Infrastructure.Repositories;

public class ParcelRepository : IParcelRepository
{
    private readonly MantaDbContext _context;

    public ParcelRepository(MantaDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    // Базові CRUD операції
    public async Task<Parcel?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Parcels
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }
    public async Task<int> GetNextIdAsync(CancellationToken cancellationToken = default)
    {
        var maxId = await _context.Parcels.MaxAsync(p => (int?)p.Id, cancellationToken) ?? 0;
        return maxId + 1;
    }   

    public async Task<IEnumerable<Parcel>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Parcels
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Parcel parcel, CancellationToken cancellationToken = default)
    {
        if (parcel == null)
            throw new ArgumentNullException(nameof(parcel));

        await _context.Parcels.AddAsync(parcel, cancellationToken);
    }

    public Task UpdateAsync(Parcel parcel, CancellationToken cancellationToken = default)
    {
        if (parcel == null)
            throw new ArgumentNullException(nameof(parcel));

        _context.Parcels.Update(parcel);
        return Task.CompletedTask;
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var parcel = await GetByIdAsync(id, cancellationToken);
        if (parcel != null)
        {
            _context.Parcels.Remove(parcel);
        }
    }

    public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Parcels
            .AnyAsync(p => p.Id == id, cancellationToken);
    }

    // Пошук за критеріями
    public async Task<IEnumerable<Parcel>> GetByDeliveryPointIdAsync(int deliveryPointId, CancellationToken cancellationToken = default)
    {
        return await _context.Parcels
            .Where(p => p.DeliveryPointId == deliveryPointId || p.CurrentLocationDeliveryPointId == deliveryPointId)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Parcel>> GetByCurrentLocationAsync(int? locationId, CancellationToken cancellationToken = default)
    {
        return await _context.Parcels
            .Where(p => p.CurrentLocationDeliveryPointId == locationId)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Parcel>> GetByVehicleIdAsync(LicensePlate vehicleId, CancellationToken cancellationToken = default)
    {
        if (vehicleId == null)
            throw new ArgumentNullException(nameof(vehicleId));

        return await _context.Parcels
            .Where(p => p.CurrentVehicleId == vehicleId)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Parcel>> GetByStatusAsync(EParcelStatus status, CancellationToken cancellationToken = default)
    {
        var parcels = await _context.Parcels
            .ToListAsync(cancellationToken);

        return parcels
            .Where(p => p.CurrentStatus.Status == status)
            .ToList();
    }

    public async Task<IEnumerable<Parcel>> GetByRecipientEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email cannot be null or empty", nameof(email));

        return await _context.Parcels
            .Where(p => EF.Property<string>(p.RecipientEmail, "Value") == email)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Parcel>> GetByRecipientPhoneAsync(string phoneNumber, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
            throw new ArgumentException("Phone number cannot be null or empty", nameof(phoneNumber));

        return await _context.Parcels
            .Where(p => EF.Property<string>(p.RecipientPhoneNumber, "Value").Contains(phoneNumber))
            .ToListAsync(cancellationToken);
    }

    // Спеціальні запити
    public async Task<IEnumerable<Parcel>> GetUnpaidParcelsAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Parcels
            .Where(p => p.AmountDue > 0)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Parcel>> GetParcelsNotInRightLocationAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Parcels
            .Where(p => p.DeliveryPointId != p.CurrentLocationDeliveryPointId)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Parcel>> GetParcelsInTransitAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Parcels
            .Where(p => p.CurrentVehicleId != null)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Parcel>> GetReadyForDeliveryAsync(int deliveryPointId, CancellationToken cancellationToken = default)
    {
        var parcels = await _context.Parcels
            .Where(p => p.DeliveryPointId == deliveryPointId 
                        && p.CurrentLocationDeliveryPointId == deliveryPointId
                        && p.AmountDue == 0)
            .ToListAsync(cancellationToken);

        // Фільтруємо за статусом (потрібно завантажити в пам'ять)
        return parcels
            .Where(p => p.CurrentStatus.Status == EParcelStatus.ReadyForPickup)
            .ToList();
    }

    // Статистика
    public async Task<int> CountByStatusAsync(EParcelStatus status, CancellationToken cancellationToken = default)
    {
        var parcels = await _context.Parcels.ToListAsync(cancellationToken);
        return parcels.Count(p => p.CurrentStatus.Status == status);
    }

    public async Task<int> CountByDeliveryPointAsync(int deliveryPointId, CancellationToken cancellationToken = default)
    {
        return await _context.Parcels
            .CountAsync(p => p.DeliveryPointId == deliveryPointId, cancellationToken);
    }

    public async Task<decimal> GetTotalAmountDueAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Parcels
            .SumAsync(p => p.AmountDue, cancellationToken);
    }

    public async Task<double> GetTotalWeightByVehicleAsync(LicensePlate vehicleId, CancellationToken cancellationToken = default)
    {
        if (vehicleId == null)
            throw new ArgumentNullException(nameof(vehicleId));

        return await _context.Parcels
            .Where(p => p.CurrentVehicleId == vehicleId)
            .SumAsync(p => p.Weight, cancellationToken);
    }

    // Пакетні операції
    public async Task<IEnumerable<Parcel>> GetByIdsAsync(IEnumerable<int> ids, CancellationToken cancellationToken = default)
    {
        if (ids == null)
            throw new ArgumentNullException(nameof(ids));

        var idList = ids.ToList();
        return await _context.Parcels
            .Where(p => idList.Contains(p.Id))
            .ToListAsync(cancellationToken);
    }

    public async Task AddRangeAsync(IEnumerable<Parcel> parcels, CancellationToken cancellationToken = default)
    {
        if (parcels == null)
            throw new ArgumentNullException(nameof(parcels));

        await _context.Parcels.AddRangeAsync(parcels, cancellationToken);
    }

    public Task UpdateRangeAsync(IEnumerable<Parcel> parcels, CancellationToken cancellationToken = default)
    {
        if (parcels == null)
            throw new ArgumentNullException(nameof(parcels));

        _context.Parcels.UpdateRange(parcels);
        return Task.CompletedTask;
    }

    // Збереження змін
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
}