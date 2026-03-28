using Manta.Domain.Enums;

namespace Manta.Application.Interfaces;

public interface ICurrentUserService
{
    public Guid UserId { get; }
    public string UserName { get; }
    public string Email { get; }
    EUserRole Role { get; }
    public void SetSystemUser(Guid userId, string email, string name, EUserRole role);
}