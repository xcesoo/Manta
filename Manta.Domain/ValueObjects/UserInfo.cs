    using Manta.Domain.Entities;
    using Manta.Domain.Enums;

    namespace Manta.Domain.ValueObjects;

    public sealed record UserInfo
    {
        public Guid Id { get; init; }
        public string Email { get; init; }
        public string Name { get; init; }   
        public EUserRole Role { get; init; }

        public UserInfo(Guid id, string email, string name, EUserRole role)
        {
            Id = id;
            Email = email;
            Name = name;
            Role = role;
        }
        public static implicit operator UserInfo(User user) => new UserInfo(user.Id, user.Email, user.Name, user.Role);
    }