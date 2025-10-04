using Manta.Domain.ValueObjects;

namespace Manta.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Manta.Domain.Entities;

public class MantaDbContext : DbContext
{
    public MantaDbContext(DbContextOptions<MantaDbContext> options) : base(options) {}
    
    public DbSet<Parcel> Parcels {get; set;}
    public DbSet<DeliveryPoint> DeliveryPoints {get; set;}
    public DbSet<DeliveryVehicle> DeliveryVehicles {get; set;}
    public DbSet<User> Users {get; set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Parcel>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(p => p.DeliveryPointId).IsRequired();
            entity.Property(p => p.AmountDue).IsRequired();
            entity.Property(p => p.Weight).IsRequired();

            entity.OwnsOne(p => p.RecipientName, name =>
            {
                name.Property(n => n.Value)
                    .HasColumnName("RecipientName")
                    .HasMaxLength(200)
                    .IsRequired();
            });
            entity.OwnsOne(p => p.RecipientPhoneNumber, phone =>
            {
                phone.Property(p => p.Value)
                    .HasColumnName("RecipientPhoneNumber")
                    .HasMaxLength(200)
                    .IsRequired(false);
            });
            entity.OwnsOne(p => p.RecipientEmail, email =>
            {
                email.Property(e => e.Value)
                    .HasColumnName("RecipientEmail")
                    .HasMaxLength(200)
                    .IsRequired(false);
            });
            entity.OwnsOne(p => p.CurrentStatus, status =>
            {
                status.Property(s => s.Status)
                    .HasColumnName("Status")
                    .IsRequired();
                status.Property(s => s.ChangedAt)
                    .HasColumnName("ChangedAt")
                    .IsRequired();
                status.OwnsOne(s => s.ChangedBy, user =>
                {
                    user.Property(u => u.Id).HasColumnName("ChangedById");
                    user.Property(u => u.Name).HasColumnName("ChangedByName");
                    user.Property(u => u.Email).HasColumnName("ChangedByEmail");
                    user.Property(u => u.Role).HasColumnName("ChangedByRole");
                });
                entity.OwnsMany(p => p.StatusHistory, history =>
                {
                    history.WithOwner().HasForeignKey("ParcelId");
                    history.Property<int>("Id").ValueGeneratedOnAdd();
                    history.HasKey("Id");
                    status.Property(s => s.Status)
                        .HasColumnName("Status")
                        .IsRequired();
                    status.Property(s => s.ChangedAt)
                        .HasColumnName("ChangedAt")
                        .IsRequired();
                    status.OwnsOne(s => s.ChangedBy, user =>
                    {
                        user.Property(u => u.Id).HasColumnName("ChangedById");
                        user.Property(u => u.Name).HasColumnName("ChangedByName");
                        user.Property(u => u.Email).HasColumnName("ChangedByEmail");
                        user.Property(u => u.Role).HasColumnName("ChangedByRole");
                    });
                    status.ToTable("ParcelStatusHistory");
                });
            });
            entity.OwnsOne(p => p.CurrentVehicleId, vehicle =>
            {
                vehicle.Property(v => v.Value)
                    .HasColumnName("VehicleId")
                    .HasMaxLength(8)
                    .IsRequired(false);
            });
        });
        modelBuilder.Entity<DeliveryPoint>(entity =>
        {
            entity.HasKey(dp => dp.Id);
            entity.Property(dp => dp.Address)
                .HasMaxLength(500)
                .IsRequired();
        });
        modelBuilder.Entity<DeliveryVehicle>(entity =>
        {
            entity.Property<int>("TechnicalId")
                .ValueGeneratedOnAdd();
            entity.HasKey("TechnicalId");
            
            entity.Property(v => v.Id)
                .HasConversion(lp => lp.Value, v => LicensePlate.Create(v))
                .HasColumnName("LicensePlate")
                .HasMaxLength(10)
                .IsRequired();
            entity.HasIndex("LicensePlate").IsUnique();
            
            entity.OwnsOne(v => v.CarModel, car =>
            {
                car.Property(c => c.Brand)
                    .HasColumnName("CarBrand")
                    .HasMaxLength(100)
                    .IsRequired();

                car.Property(c => c.Model)
                    .HasColumnName("CarModel") 
                    .HasMaxLength(100)
                    .IsRequired();
            });

            entity.Property(v => v.Capacity)
                .HasPrecision(10, 2)
                .IsRequired();

            entity.Property(v => v.CurrentLoad)
                .HasPrecision(10, 2)
                .IsRequired();
            entity.HasMany<Parcel>()
                .WithOne()
                .HasPrincipalKey(v => v.Id)
                .HasForeignKey(p => p.CurrentVehicleId)
                .OnDelete(DeleteBehavior.SetNull);
        });
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.Id);

            entity.OwnsOne(u => u.Email, email =>
            {
                email.Property(e => e.Value)
                    .HasColumnName("Email")
                    .HasMaxLength(300)
                    .IsRequired();
            });

            entity.OwnsOne(u => u.Name, name =>
            {
                name.Property(n => n.Value)
                    .HasColumnName("Name")
                    .HasMaxLength(200)
                    .IsRequired();
            });

            entity.Property(u => u.Role)
                .HasColumnName("Role")
                .IsRequired();

            entity.HasDiscriminator<string>("UserType")
                .HasValue<Admin>("Admin")
                .HasValue<Cashier>("Cashier")
                .HasValue<Driver>("Driver")
                .HasValue<SystemUser>("SystemUser");
        });
        
        modelBuilder.Entity<Cashier>(entity =>
        {
            entity.Property(c => c.DeliveryPointId)
                .HasColumnName("DeliveryPointId")
                .IsRequired();
        });

        modelBuilder.Entity<Driver>(entity =>
        {
            entity.OwnsOne(d => d.LicensePlate, lp =>
            {
                lp.Property(p => p.Value)
                    .HasColumnName("LicensePlate")
                    .HasMaxLength(10)
                    .IsRequired();
            });
        });
    }
}