using Manta.Domain.Interfaces;
using MediatR;

namespace Manta.Application.Queries.Parcel;

public class GetParcelByIdQueryHandler(IParcelRepository parcelRepository)
    : IRequestHandler<GetParcelByIdQuery, Manta.Domain.Entities.Parcel>
{
    public async Task<Domain.Entities.Parcel?> Handle(GetParcelByIdQuery request, CancellationToken cancellationToken)
    {
        return await parcelRepository.GetByIdAsync(request.Id);
    }
}