using Manta.Domain.Entities;
using Manta.Domain.Enums;
using Manta.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Manta.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly MantaDbContext _context;

    public UserRepository(MantaDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<User?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
    }
    public async Task<int> GetNextIdAsync(CancellationToken cancellationToken = default)
    {
        var maxId = await _context.Users.MaxAsync(u => (int?)u.Id, cancellationToken) ?? 0;
        return maxId + 1;
    }   

    public async Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Users.ToListAsync(cancellationToken);
    }

    public async Task AddAsync(User user, CancellationToken cancellationToken = default)
    {
        if (user == null)
            throw new ArgumentNullException(nameof(user));

        await _context.Users.AddAsync(user, cancellationToken);
    }

    public Task UpdateAsync(User user, CancellationToken cancellationToken = default)
    {
        if (user == null)
            throw new ArgumentNullException(nameof(user));

        _context.Users.Update(user);
        return Task.CompletedTask;
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var user = await GetByIdAsync(id, cancellationToken);
        if (user != null)
        {
            _context.Users.Remove(user);
        }
    }

    public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Users
            .AnyAsync(u => u.Id == id, cancellationToken);
    }
    public async Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _context.Users
            .AnyAsync(u => EF.Property<string>(u.Email, "Value") == email, cancellationToken);
    }
    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email cannot be null or empty", nameof(email));

        return await _context.Users
            .FirstOrDefaultAsync(u => EF.Property<string>(u.Email, "Value") == email, cancellationToken);
    }

    public async Task<IEnumerable<User>> GetByRoleAsync(EUserRole role, CancellationToken cancellationToken = default)
    {
        return await _context.Users
            .Where(u => u.Role == role)
            .ToListAsync(cancellationToken);
    }

    public async Task<Cashier?> GetCashierByDeliveryPointAsync(int deliveryPointId, CancellationToken cancellationToken = default)
    {
        return await _context.Users.OfType<Cashier>()
            .FirstOrDefaultAsync(c => c.DeliveryPointId == deliveryPointId, cancellationToken);
    }

    public async Task<Driver?> GetDriverByLicensePlateAsync(string licensePlate, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(licensePlate))
            throw new ArgumentException("License plate cannot be null or empty", nameof(licensePlate));

        return await _context.Users.OfType<Driver>()
            .FirstOrDefaultAsync(d => EF.Property<string>(d.LicensePlate, "Value") == licensePlate, cancellationToken);
    }

    public async Task<IEnumerable<Admin>> GetAllAdminsAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Users.OfType<Admin>().ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Cashier>> GetAllCashiersAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Users.OfType<Cashier>().ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Driver>> GetAllDriversAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Users.OfType<Driver>().ToListAsync(cancellationToken);
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
}