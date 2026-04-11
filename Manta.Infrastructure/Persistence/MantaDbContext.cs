using Manta.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Manta.Domain.Entities;
using Manta.Infrastructure.Entities;

namespace Manta.Infrastructure.Persistence;

public class MantaDbContext : DbContext
{
    public MantaDbContext(DbContextOptions<MantaDbContext> options) : base(options) {}
    
    public DbSet<Parcel> Parcels {get; set;}
    public DbSet<DeliveryPoint> DeliveryPoints {get; set;}
    public DbSet<DeliveryVehicle> DeliveryVehicles {get; set;}
    public DbSet<User> Users {get; set;}
    
    public DbSet<OutboxMessage> OutboxMessages {get; set;}
    public DbSet<ProcessedLog> ProcessedLogs {get; set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OutboxMessage>(entity =>
        {
            entity.HasKey(e => e.MessageId);
            entity.Property(e => e.MessageId).ValueGeneratedNever();
            entity.Property(e => e.MessageType).IsRequired();
            entity.Property(e => e.Payload)
                .HasColumnType("jsonb")
                .IsRequired();
            entity.HasIndex(e => e.CreatedAt);
            entity.Property(e => e.CreatedAt).IsRequired();

        });
        modelBuilder.Entity<ProcessedLog>(entity =>
        {
            entity.HasKey(e => e.MessageId);
            entity.Property(e => e.MessageId).ValueGeneratedNever();
            entity.HasIndex(e => e.ProcessedAt);
            entity.Property(e=>e.ProcessedAt).IsRequired();
        });
        //  Parcel
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
            entity.Property(p => p.ArrivedAt)
                .HasColumnName("ArrivedAt");
            
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
            
            // CurrentVehicleId 
            entity.Property(p => p.CurrentVehicleId)
                .HasColumnName("CurrentVehicleId")
                .IsRequired(false);
            
            //  computed properties
            entity.Ignore(p => p.InRightLocation);
            entity.Ignore(p => p.Paid);
            entity.Ignore(p => p.CurrentStatus);
            
            // StatusHistory
            entity.OwnsMany(p => p.StatusHistory, history =>
            {
                history.ToTable("ParcelStatusHistory");
                history.WithOwner().HasForeignKey("ParcelId");
                history.Property<int>("Id").UseIdentityByDefaultColumn();
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
                    
                    user.Property(u => u.Role)
                        .HasColumnName("ChangedByRole")
                        .HasConversion<string>()
                        .IsRequired();
                });
            });

            
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
            entity.Property(dp => dp.Capacity)
                .IsRequired()
                .HasDefaultValue(3);
        });
        
        // Конфігурація DeliveryVehicle
        modelBuilder.Entity<DeliveryVehicle>(entity =>
        {
            // Використовуємо LicensePlate як первинний ключ
            entity.HasKey(v => v.Id);

            entity.Property(v => v.Id)
                .ValueGeneratedNever();
            
            entity.Property(v => v.LicensePlateId)
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
        });
        
                modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.Id);
            
            entity.Property(u => u.Id)
                .ValueGeneratedNever();
            entity.Property(u => u.PasswordHash)
                .HasColumnName("PasswordHash")
                .IsRequired();
            
            // Email як Value Object через OwnsOne
            entity.OwnsOne(u => u.Email, email =>
            {
                email.Property(e => e.Value)
                    .HasColumnName("Email")
                    .HasMaxLength(300)
                    .IsRequired();
                
                // ✅ ПРАВИЛЬНИЙ СИНТАКСИС: Індекс створюється тут, всередині OwnsOne
                email.HasIndex(e => e.Value).IsUnique();
            });
            
            // Name як Value Object через OwnsOne
            entity.OwnsOne(u => u.Name, name =>
            {
                name.Property(n => n.Value)
                    .HasColumnName("Name")
                    .HasMaxLength(200)
                    .IsRequired();
            });
            
            entity.Ignore(u => u.Role);
            
            // Discriminator для Table-Per-Hierarchy (TPH)
            entity.HasDiscriminator<string>("UserType")
                .HasValue<Admin>("Admin")
                .HasValue<Cashier>("Cashier")
                .HasValue<Driver>("Driver")
                .HasValue<SystemUser>("SystemUser")
                .HasValue<UnknownUser>("UnknownUser");
        });
        
        // Конфігурація Admin (пуста)
        modelBuilder.Entity<Admin>();
        
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
            // LicensePlate як Value Object через OwnsOne
            entity.OwnsOne(d => d.LicensePlate, lp =>
            {
                lp.Property(p => p.Value)
                    .HasColumnName("LicensePlate")
                    .HasMaxLength(10)
                    .IsRequired();

                // ✅ Індекс створюється тут, всередині OwnsOne
                lp.HasIndex(p => p.Value);
            });
        });
        
        // Конфігурація SystemUser (пуста)
        modelBuilder.Entity<SystemUser>();

        modelBuilder.Entity<UnknownUser>();

        base.OnModelCreating(modelBuilder);
    }
}
