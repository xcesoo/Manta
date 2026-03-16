using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Manta.Application.Interfaces;
using Manta.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Manta.WebApi.Auth;

public class JwtProvider : IJwtProvider
{
    private readonly IConfiguration _configuration;
    
    public JwtProvider(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Role, user.Role.ToString()),
        };
        if (user is Cashier userCashier)
        {
            claims.Add(new Claim("DeliveryPointId", userCashier.DeliveryPointId.ToString()));
        }
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        int expiresIn = int.Parse(_configuration["Jwt:ExpireHours"]!);
        
        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(expiresIn),
            signingCredentials: credentials);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}