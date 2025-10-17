using Manta.Domain.Entities;
using Manta.Domain.Enums;

namespace Manta.Infrastructure.Repositories;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<int> GetNextIdAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync(User? user, CancellationToken cancellationToken = default);
    Task UpdateAsync(User user, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default);
    Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken = default);
    
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<IEnumerable<User>> GetByRoleAsync(EUserRole role, CancellationToken cancellationToken = default);
    
    // Специфічні методи для підкласів
    Task<Cashier?> GetCashierByDeliveryPointAsync(int deliveryPointId, CancellationToken cancellationToken = default);
    Task<Driver?> GetDriverByLicensePlateAsync(string licensePlate, CancellationToken cancellationToken = default);
    Task<IEnumerable<Admin>> GetAllAdminsAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<Cashier>> GetAllCashiersAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<Driver>> GetAllDriversAsync(CancellationToken cancellationToken = default);
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}