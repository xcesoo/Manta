namespace manta.Domain.Entities;

public class User
{
    public string FirstName { get; set; }

    public User(string firstName)
    {
        FirstName = firstName;
    }
}