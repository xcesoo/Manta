    using Manta.Domain.Entities;
    using Manta.Domain.ValueObjects;
    using Manta.Infrastructure.Persistence;
    using Microsoft.EntityFrameworkCore;
    using Manta.Domain.Interfaces;

    namespace Manta.Infrastructure.Repositories;

    public class DeliveryVehicleRepository : IDeliveryVehicleRepository
    {
        private readonly MantaDbContext _context;

        public DeliveryVehicleRepository(MantaDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<DeliveryVehicle?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.DeliveryVehicles
                .FirstOrDefaultAsync(v => v.Id == id, cancellationToken);
        }

        public async Task<DeliveryVehicle?> GetByLicensePlateIdAsync(LicensePlate licensePlate,
            CancellationToken cancellationToken = default)
        {
            if (licensePlate == null)
                throw new ArgumentNullException(nameof(licensePlate));
            return await  _context.DeliveryVehicles
                .FirstOrDefaultAsync(v => v.LicensePlateId == licensePlate, cancellationToken);
        }
        

        public async Task<IEnumerable<DeliveryVehicle>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.DeliveryVehicles.ToListAsync(cancellationToken);
        }

        public async Task AddAsync(DeliveryVehicle vehicle, CancellationToken cancellationToken = default)
        {
            if (vehicle == null)
                throw new ArgumentNullException(nameof(vehicle));

            await _context.DeliveryVehicles.AddAsync(vehicle, cancellationToken);
        }

        public Task UpdateAsync(DeliveryVehicle vehicle, CancellationToken cancellationToken = default)
        {
            if (vehicle == null)
                throw new ArgumentNullException(nameof(vehicle));

            _context.DeliveryVehicles.Update(vehicle);
            return Task.CompletedTask;
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var vehicle = await GetByIdAsync(id, cancellationToken);
            if (vehicle != null)
            {
                _context.DeliveryVehicles.Remove(vehicle);
            }
        }

        public async Task<bool> ExistsAsync(LicensePlate licensePlateId, CancellationToken cancellationToken = default)
        {
            if (licensePlateId == null)
                throw new ArgumentNullException(nameof(licensePlateId));

            return await _context.DeliveryVehicles
                .AnyAsync(v => v.LicensePlateId == licensePlateId, cancellationToken);
        }
        //todo

        public async Task<IEnumerable<DeliveryVehicle>> GetAvailableVehiclesAsync(double requiredCapacity, CancellationToken cancellationToken = default)
        {
            return await _context.DeliveryVehicles
                .Where(v => v.Capacity - v.CurrentLoad >= requiredCapacity)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<DeliveryVehicle>> GetByCarModelAsync(string brand, string model, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(brand))
                throw new ArgumentException("Brand cannot be null or empty", nameof(brand));
            if (string.IsNullOrWhiteSpace(model))
                throw new ArgumentException("Model cannot be null or empty", nameof(model));

            return await _context.DeliveryVehicles
                .Where(v => EF.Property<string>(v.CarModel, "Brand") == brand 
                            && EF.Property<string>(v.CarModel, "Model") == model)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<DeliveryVehicle>> GetOverloadedVehiclesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.DeliveryVehicles
                .Where(v => v.CurrentLoad > v.Capacity)
                .ToListAsync(cancellationToken);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }