using Manta.Application.Interfaces;
using Manta.Application.Services;
using Manta.Domain.Interfaces;
using Manta.Domain.StatusRules.Interfaces;
using Manta.Domain.ValueObjects;
using MediatR;

namespace Manta.Application.Commands.Parcel;

public class AcceptParcelAtDeliveryPointCommandHandler : IRequestHandler<AcceptParcelAtDeliveryPointCommand, Guid>
{
    private IUserRepository _userRepository;
    private ParcelDeliveryService _parcelDeliveryService;
    private readonly ICurrentUserService _currentUser;

    public AcceptParcelAtDeliveryPointCommandHandler(
        IUserRepository userRepository, 
        ParcelDeliveryService parcelDeliveryService,
        ICurrentUserService  currentUser)
    {
        _userRepository = userRepository;
        _parcelDeliveryService = parcelDeliveryService;
        _currentUser = currentUser;
    }
    
    public async Task<Guid> Handle(AcceptParcelAtDeliveryPointCommand request, CancellationToken cancellationToken)
    {
        return await _parcelDeliveryService.AcceptedAtDeliveryPoint(
            request.DeliveryPointId, 
            request.ParcelId, 
            new UserInfo(_currentUser.UserId,  _currentUser.Email, _currentUser.UserName, _currentUser.Role));
    }
}