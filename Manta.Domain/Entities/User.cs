using Manta.Domain.Enums;
using Manta.Domain.ValueObjects;

namespace Manta.Domain.Entities;

public abstract class User
{
    public int Id { get; protected set; }
    public Email Email { get; protected set; }
    public Name Name { get; protected set; }
    public string PasswordHash { get; protected set; }
    public abstract EUserRole Role { get; }

    protected User()
    {
    }

    protected User(int id, string name, string email, string passwordHash)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Username cannot be null or empty", nameof(name));
        Id = id;
        Name = name;
        Email = email;
        PasswordHash = passwordHash;
    }
    
}

public sealed class UnknownUser : User
{
    public UnknownUser(int id, string name, string email, string passwordHash)
        : base(id, name, email, passwordHash){}
    private UnknownUser()
    {
    }
    public override EUserRole Role => EUserRole.Unknown;
}

public sealed class Admin : User
{
    public Admin(int id, string fullname, string email, string passwordHash) 
        : base(id, fullname, email, passwordHash){}
    private Admin()
    {
    }
    public override EUserRole Role => EUserRole.Admin;
}

public sealed class Cashier : User
{
    public int? DeliveryPointId { get; private set; }
    private Cashier()
    {
    }

    public Cashier(int id, string name, string email, int? deliveryPointId, string passwordHash)
        : base(id, name, email, passwordHash)
    {
        DeliveryPointId = deliveryPointId;
    }
    public override EUserRole Role => EUserRole.Cashier;
}

public sealed class Driver : User
{
    public LicensePlate? LicensePlate { get; private set; }
    private Driver()
    {
    }

    public Driver(int id, string name, string email, string passwordHash, LicensePlate? licensePlate) : base(id, name, email, passwordHash)
    {
        LicensePlate = licensePlate;
    }
    public override EUserRole Role => EUserRole.Driver;
}

public sealed class SystemUser : User
{
    public static readonly SystemUser Instance = new SystemUser();
    private SystemUser() : base(0, "system", "system@manta.com", ""){}
    public override EUserRole Role => EUserRole.System;
}