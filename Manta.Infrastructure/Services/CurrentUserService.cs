using System.Security.Claims;
using Manta.Application.Interfaces;
using Manta.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace Manta.Infrastructure.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor  _accessor;
    public  CurrentUserService(IHttpContextAccessor accessor)
    {
        _accessor = accessor;
    }

    public Guid UserId
    {
        get
        {
            var id = _accessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Guid.TryParse(id, out var result) ? result : Guid.Empty;
        }
    }

    public EUserRole Role {
        get
        {
            var role = _accessor.HttpContext?.User?.FindFirst(ClaimTypes.Role)?.Value;
            if(Enum.TryParse<EUserRole>(role,true, out var result)) return result;
            return EUserRole.Unknown;
        }
    }

    public string Email => _accessor.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value ?? string.Empty;

    public string UserName => _accessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value ?? string.Empty;
}