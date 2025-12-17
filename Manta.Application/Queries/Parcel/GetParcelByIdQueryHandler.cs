using Manta.Domain.Interfaces;
using MediatR;

namespace Manta.Application.Queries.Parcel;

public class GetParcelByIdQueryHandler : IRequestHandler<GetParcelByIdQuery, Manta.Domain.Entities.Parcel>
{
    private readonly IParcelRepository _parcelRepository;
    public GetParcelByIdQueryHandler(IParcelRepository parcelRepository)
    {
        _parcelRepository = parcelRepository;
    }
    public async Task<Manta.Domain.Entities.Parcel> Handle(GetParcelByIdQuery request, CancellationToken cancellationToken)
    {
        return await _parcelRepository.GetByIdAsync(request.Id);
    }
}