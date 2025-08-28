namespace manta.Domain.Entities;

public abstract class User
{
    public virtual string Username { get; private set; }
    public virtual string Email { get; private set; }
}

public class Admin : User
{
    public override string Username => "root";

    public override string Email => "root";
    
}