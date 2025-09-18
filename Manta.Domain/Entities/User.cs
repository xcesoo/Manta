using Manta.Domain.Enums;

namespace Manta.Domain.Entities;

public abstract class User
{
    public int Id { get; protected set; }
    public string Email { get; protected set; }
    public string FullName { get; protected set; }
    public abstract UserRole Role { get; }

    protected User(int id, string fullName, string email)
    {
        if (string.IsNullOrWhiteSpace(fullName))
            throw new ArgumentException("Username cannot be null or empty", nameof(fullName));
        Id = id;
        FullName = fullName;
        Email = email;
    }
}

public sealed class Admin : User
{
    public Admin(int id, string fullname, string email) 
        : base(id, fullname, email){}
    public override UserRole Role => UserRole.Admin;
}

public sealed class Cashier : User
{
    public int DeliveryPoint { get; private set; }

    public Cashier(int id, string fullname, string email, int deliveryPoint)
        : base(id, fullname, email)
    {
        DeliveryPoint = deliveryPoint;
    }
    public override UserRole Role => UserRole.Cashier;
}

public sealed class SystemUser : User
{
    public static readonly SystemUser Instance = new SystemUser();
    private SystemUser() : base(0, "system", "system@manta.com"){}
    public override UserRole Role => UserRole.System;
}