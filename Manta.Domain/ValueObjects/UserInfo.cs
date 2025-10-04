    using Manta.Domain.Entities;
    using Manta.Domain.Enums;

    namespace Manta.Domain.ValueObjects;

    public sealed record UserInfo
    {
        public int Id { get; init; }
        public string Email { get; init; }
        public string Name { get; init; }   
        public EUserRole  Role { get; init; }

        private UserInfo(User user)
        {
            Id = user.Id;
            Email = user.Email;
            Name = user.Name;
            Role = user.Role;
        }
        public static implicit operator UserInfo(User user) => new UserInfo(user);
    }