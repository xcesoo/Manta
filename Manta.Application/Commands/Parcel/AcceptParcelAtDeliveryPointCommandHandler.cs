using Manta.Application.Services;
using Manta.Domain.Interfaces;
using Manta.Domain.StatusRules.Interfaces;
using MediatR;

namespace Manta.Application.Commands.Parcel;

public class AcceptParcelAtDeliveryPointCommandHandler : IRequestHandler<AcceptParcelAtDeliveryPointCommand, int>
{
    private IUserRepository _userRepository;
    private ParcelDeliveryService _parcelDeliveryService;

    public AcceptParcelAtDeliveryPointCommandHandler(
        IUserRepository userRepository, 
        ParcelDeliveryService parcelDeliveryService)
    {
        _userRepository = userRepository;
        _parcelDeliveryService = parcelDeliveryService;
    }
    
    public async Task<int> Handle(AcceptParcelAtDeliveryPointCommand request, CancellationToken cancellationToken)
    {
        var sender = await _userRepository.GetByIdAsync(request.SenderId);
        if (sender == null)
            throw new ArgumentException($"User with ID {request.SenderId} not found");
        return await _parcelDeliveryService.AcceptedAtDeliveryPoint(request.DeliveryPointId, request.ParcelId, sender);
    }
}