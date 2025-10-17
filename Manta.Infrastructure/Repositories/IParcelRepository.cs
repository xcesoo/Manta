using Manta.Domain.Entities;
using Manta.Domain.Enums;
using Manta.Domain.ValueObjects;

namespace Manta.Infrastructure.Repositories;

public interface IParcelRepository
{
    // Базові CRUD операції
    Task<Parcel?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<int> GetNextIdAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<Parcel>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(Parcel parcel, CancellationToken cancellationToken = default);
    Task UpdateAsync(Parcel parcel, CancellationToken cancellationToken = default);
    // Task UpdateAsync(int parcelId, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default);
    
    // Пошук за критеріями
    Task<IEnumerable<Parcel>> GetByDeliveryPointIdAsync(int deliveryPointId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Parcel>> GetByCurrentLocationAsync(int? locationId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Parcel>> GetByVehicleIdAsync(LicensePlate vehicleId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Parcel>> GetByStatusAsync(EParcelStatus status, CancellationToken cancellationToken = default);
    Task<IEnumerable<Parcel>> GetByRecipientEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<IEnumerable<Parcel>> GetByRecipientPhoneAsync(string phoneNumber, CancellationToken cancellationToken = default);
    
    // Спеціальні запити
    Task<IEnumerable<Parcel>> GetUnpaidParcelsAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<Parcel>> GetParcelsNotInRightLocationAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<Parcel>> GetParcelsInTransitAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<Parcel>> GetReadyForDeliveryAsync(int deliveryPointId, CancellationToken cancellationToken = default);
    
    // Статистика
    Task<int> CountByStatusAsync(EParcelStatus status, CancellationToken cancellationToken = default);
    Task<int> CountByDeliveryPointAsync(int deliveryPointId, CancellationToken cancellationToken = default);
    Task<decimal> GetTotalAmountDueAsync(CancellationToken cancellationToken = default);
    Task<double> GetTotalWeightByVehicleAsync(LicensePlate vehicleId, CancellationToken cancellationToken = default);
    
    // Пакетні операції
    Task<IEnumerable<Parcel>> GetByIdsAsync(IEnumerable<int> ids, CancellationToken cancellationToken = default);
    Task AddRangeAsync(IEnumerable<Parcel> parcels, CancellationToken cancellationToken = default);
    Task UpdateRangeAsync(IEnumerable<Parcel> parcels, CancellationToken cancellationToken = default);
    
    // Збереження змін
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}