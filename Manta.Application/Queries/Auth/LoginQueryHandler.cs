using Manta.Application.Interfaces;
using Manta.Domain.Entities;
using Manta.Domain.Interfaces;
using MediatR;
using Manta.Domain.ValueObjects;

namespace Manta.Application.Queries.Auth;

public class LoginQueryHandler : IRequestHandler<LoginQuery, string>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtProvider _jwtProvider;
    private readonly IPasswordHasher _passwordHasher;

    public LoginQueryHandler(IUserRepository userRepository, IJwtProvider jwtProvider, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _jwtProvider = jwtProvider;
        _passwordHasher = passwordHasher;
    }
    
    public async Task<string> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);
        if (user == null)
            throw new ArgumentException("Invalid email or password.");
        bool isPasswordValid = _passwordHasher.Verify(request.Password, user.PasswordHash);
        if(!isPasswordValid)
            throw new ArgumentException("Invalid email or password.");
        return _jwtProvider.GenerateToken(user);
    }
}