using Manta.Domain.Entities;
using Manta.Infrastructure.Repositories;

namespace Manta.Presentation.Services;

public class ParcelSearchService
{
    private readonly IParcelRepository _parcelRepository;
    public ParcelSearchService(IParcelRepository parcelRepository)
    {
        _parcelRepository = parcelRepository;
    }

    public async Task<IEnumerable<Parcel>> GetParcelsAsync(int deliveryPointId, string? search = null)
    {
        if (string.IsNullOrEmpty(search)) return await _parcelRepository.GetByDeliveryPointIdAsync(deliveryPointId);
        else
        {
            return await _parcelRepository.GetByRecipientPhoneAsync(search);
        }
    }
}