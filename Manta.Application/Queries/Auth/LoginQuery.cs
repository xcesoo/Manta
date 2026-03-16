using MediatR;

namespace Manta.Application.Queries.Auth;

using Domain.ValueObjects;

public record LoginQuery(string Email, string Password) : IRequest<string>;