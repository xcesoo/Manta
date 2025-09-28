using Manta.Domain.Enums;
using Manta.Domain.ValueObjects;

namespace Manta.Domain.Entities;

public abstract class User
{
    public int Id { get; protected set; }
    public Email Email { get; protected set; }
    public Name Name { get; protected set; }
    public abstract EUserRole Role { get; }

    protected User(int id, string name, string email)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Username cannot be null or empty", nameof(name));
        Id = id;
        Name = name;
        Email = email;
    }
}

public sealed class Admin : User
{
    public Admin(int id, string fullname, string email) 
        : base(id, fullname, email){}
    public override EUserRole Role => EUserRole.Admin;
}

public sealed class Cashier : User
{
    public int DeliveryPointId { get; private set; }

    public Cashier(int id, string name, string email, int deliveryPointId)
        : base(id, name, email)
    {
        DeliveryPointId = deliveryPointId;
    }
    public override EUserRole Role => EUserRole.Cashier;
}

public sealed class Driver : User
{
    public LicensePlate LicensePlate { get; private set; }
    public Driver(int id, string name, string email, LicensePlate licensePlate) : base(id, name, email)
    {
        LicensePlate = licensePlate;
    }
    public override EUserRole Role => EUserRole.Driver;
}

public sealed class SystemUser : User
{
    public static readonly SystemUser Instance = new SystemUser();
    private SystemUser() : base(0, "system", "system@manta.com"){}
    public override EUserRole Role => EUserRole.System;
}