using MediatR;

namespace Manta.Application.Queries.Parcel;

public record GetParcelByIdQuery(int Id) : IRequest<Manta.Domain.Entities.Parcel>;