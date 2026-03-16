namespace Manta.Application.Interfaces;

using Domain.Entities;

public interface IJwtProvider
{
    string GenerateToken(User user);
}