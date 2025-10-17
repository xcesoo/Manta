using Manta.Domain.ValueObjects;

using Microsoft.EntityFrameworkCore;
using Manta.Domain.Entities;

namespace Manta.Infrastructure.Persistence;
public class MantaDbContext : DbContext
{
    public MantaDbContext(DbContextOptions<MantaDbContext> options) : base(options) {}
    
    public DbSet<Parcel> Parcels {get; set;}
    public DbSet<DeliveryPoint> DeliveryPoints {get; set;}
    public DbSet<DeliveryVehicle> DeliveryVehicles {get; set;}
    public DbSet<User> Users {get; set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {// Конфігурація Parcel
        modelBuilder.Entity<Parcel>(entity =>
        {
            entity.HasKey(e => e.Id);
            
            entity.Property(p => p.Id)
                .ValueGeneratedNever();
            
            entity.Property(p => p.DeliveryPointId)
                .IsRequired();
            
            entity.Property(p => p.CurrentLocationDeliveryPointId)
                .IsRequired(false);
            
            entity.Property(p => p.AmountDue)
                .HasPrecision(18, 2)
                .IsRequired();
            
            entity.Property(p => p.Weight)
                .IsRequired();
            
            // Value Objects
            entity.OwnsOne(p => p.RecipientName, name =>
            {
                name.Property(n => n.Value)
                    .HasColumnName("RecipientName")
                    .HasMaxLength(200)
                    .IsRequired();
            });
            
            entity.OwnsOne(p => p.RecipientPhoneNumber, phone =>
            {
                phone.Property(ph => ph.Value)
                    .HasColumnName("RecipientPhoneNumber")
                    .HasMaxLength(20)
                    .IsRequired();
            });
            
            entity.OwnsOne(p => p.RecipientEmail, email =>
            {
                email.Property(e => e.Value)
                    .HasColumnName("RecipientEmail")
                    .HasMaxLength(300);
            });
            
            // CurrentVehicleId як простий навігаційний property
            entity.Property(p => p.CurrentVehicleId)
                .HasConversion(
                    v => v != null ? v.Value : null,
                    v => v != null ? LicensePlate.Create(v) : null)
                .HasColumnName("CurrentVehicleId")
                .HasMaxLength(10)
                .IsRequired(false);
            
            // Ігноруємо computed properties
            entity.Ignore(p => p.InRightLocation);
            entity.Ignore(p => p.Paid);
            entity.Ignore(p => p.CurrentStatus);
            
            // StatusHistory як окрема таблиця
            entity.OwnsMany(p => p.StatusHistory, history =>
            {
                history.ToTable("ParcelStatusHistory");
                history.WithOwner().HasForeignKey("ParcelId");
                history.Property<int>("Id").ValueGeneratedOnAdd();
                history.HasKey("Id");
                
                history.Property(s => s.Status)
                    .HasColumnName("Status")
                    .HasConversion<string>()
                    .IsRequired();
                
                history.Property(s => s.ChangedAt)
                    .HasColumnName("ChangedAt")
                    .IsRequired();
                
                history.OwnsOne(s => s.ChangedBy, user =>
                {
                    user.Property(u => u.Id)
                        .HasColumnName("ChangedById")
                        .IsRequired();
                    
                    user.Property(u => u.Name)
                        .HasColumnName("ChangedByName")
                        .HasMaxLength(200)
                        .IsRequired();

                    user.Property(u => u.Email)
                        .HasColumnName("ChangedByEmail")
                        .HasMaxLength(300);
                    
                    // Ігноруємо Role - це вичислювана властивість
                    user.Ignore(u => u.Role);
                });
            });

            
            // Зв'язки
            entity.HasOne<DeliveryPoint>()
                .WithMany()
                .HasForeignKey(p => p.DeliveryPointId)
                .OnDelete(DeleteBehavior.Restrict);
            
            entity.HasOne<DeliveryPoint>()
                .WithMany()
                .HasForeignKey(p => p.CurrentLocationDeliveryPointId)
                .OnDelete(DeleteBehavior.SetNull);
            
            entity.HasOne<DeliveryVehicle>()
                .WithMany()
                .HasPrincipalKey(v => v.Id)
                .HasForeignKey(p => p.CurrentVehicleId)
                .OnDelete(DeleteBehavior.SetNull);
            
            // Індекси
            entity.HasIndex(p => p.DeliveryPointId);
            entity.HasIndex(p => p.CurrentLocationDeliveryPointId);
            entity.HasIndex(p => p.CurrentVehicleId);
        });
        
        // Конфігурація DeliveryPoint
        modelBuilder.Entity<DeliveryPoint>(entity =>
        {
            entity.HasKey(dp => dp.Id);
            
            entity.Property(dp => dp.Id)
                .ValueGeneratedNever();
            
            entity.Property(dp => dp.Address)
                .HasMaxLength(500)
                .IsRequired();
        });
        
        // Конфігурація DeliveryVehicle
        modelBuilder.Entity<DeliveryVehicle>(entity =>
        {
            // Використовуємо LicensePlate як первинний ключ
            entity.HasKey(v => v.Id);
            
            entity.Property(v => v.Id)
                .HasConversion(
                    lp => lp.Value,
                    v => LicensePlate.Create(v))
                .HasColumnName("LicensePlate")
                .HasMaxLength(10)
                .IsRequired();
            
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
            
            // ParcelsIds як backing field для колекції
            entity.Property<List<int>>("_parcelsIds")
                .HasColumnName("ParcelsIds")
                .HasConversion(
                    v => string.Join(',', v),
                    v => string.IsNullOrEmpty(v) ? new List<int>() : v.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList())
                .IsRequired();
            
            entity.Ignore(v => v.ParcelsIds);
        });
        
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.Id);
            
            entity.Property(u => u.Id)
                .ValueGeneratedNever();
            
            entity.OwnsOne(u => u.Email, email =>
            {
                email.Property(e => e.Value)
                    .HasColumnName("Email")
                    .HasMaxLength(300);
                
                // Створюємо індекс на вкладеному рівні
                email.HasIndex(e => e.Value);
            });
            
            entity.OwnsOne(u => u.Name, name =>
            {
                name.Property(n => n.Value)
                    .HasColumnName("Name")
                    .HasMaxLength(200)
                    .IsRequired();
            });
            
            // Ігноруємо Role - це вичислювана властивість
            entity.Ignore(u => u.Role);
            
            // Discriminator для TPH
            entity.HasDiscriminator<string>("UserType")
                .HasValue<Admin>("Admin")
                .HasValue<Cashier>("Cashier")
                .HasValue<Driver>("Driver")
                .HasValue<SystemUser>("SystemUser");
        });

        
        // Конфігурація Cashier
        modelBuilder.Entity<Cashier>(entity =>
        {
            entity.Property(c => c.DeliveryPointId)
                .HasColumnName("DeliveryPointId")
                .IsRequired();
            
            entity.HasOne<DeliveryPoint>()
                .WithMany()
                .HasForeignKey(c => c.DeliveryPointId)
                .OnDelete(DeleteBehavior.Restrict);
        });        
        
        // Конфігурація Driver
        modelBuilder.Entity<Driver>(entity =>
        {
            entity.OwnsOne(d => d.LicensePlate, lp =>
            {
                lp.Property(p => p.Value)
                    .HasColumnName("LicensePlate")
                    .HasMaxLength(10)
                    .IsRequired();
                
                // Створюємо індекс на вкладеному рівні
                lp.HasIndex(p => p.Value);
            });
        });

        

        base.OnModelCreating(modelBuilder);
    }
}