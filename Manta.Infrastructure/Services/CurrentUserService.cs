using System.Security.Claims;
using Manta.Application.Interfaces;
using Manta.Domain.Enums;
using Manta.Domain.ValueObjects;
using Microsoft.AspNetCore.Http;

namespace Manta.Infrastructure.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor  _accessor;
    private UserInfo? _systemUser;
    public  CurrentUserService(IHttpContextAccessor accessor)
    {
        _accessor = accessor;
    }

    public void SetSystemUser(Guid userId, string email, string name, EUserRole role)
    {
        _systemUser = new UserInfo(userId, email, name, role);
    }
    public Guid UserId => _systemUser?.Id ?? GetGuidClaim(ClaimTypes.NameIdentifier);
    
    public EUserRole Role => _systemUser?.Role ?? ParseRoleClaim();

    public string Email => _systemUser?.Email ?? _accessor.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value ?? string.Empty;

    public string UserName => _systemUser?.Name ?? _accessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value ?? string.Empty;
    
    private Guid GetGuidClaim(string type)
    {
        var val = _accessor.HttpContext?.User.FindFirst(type)?.Value;
        return Guid.TryParse(val, out var id) ? id : Guid.Empty;
    }
    private EUserRole ParseRoleClaim()
    {
        var roleStr = _accessor.HttpContext?.User.FindFirst(ClaimTypes.Role)?.Value;
        return Enum.TryParse<EUserRole>(roleStr, true, out var role) ? role : EUserRole.Unknown;
    }
}