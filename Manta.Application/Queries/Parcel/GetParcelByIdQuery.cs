using MediatR;

namespace Manta.Application.Queries.Parcel;

public record GetParcelByIdQuery(Guid Id) : IRequest<Manta.Domain.Entities.Parcel>;