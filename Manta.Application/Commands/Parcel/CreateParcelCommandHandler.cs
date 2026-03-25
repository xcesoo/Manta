using Manta.Application.Factories;
using Manta.Application.Interfaces;
using Manta.Domain.CreationOptions;
using Manta.Domain.Interfaces;
using Manta.Domain.ValueObjects;
using MassTransit;
using MediatR;

namespace Manta.Application.Commands.Parcel;

public class CreateParcelCommandHandler : IRequestHandler<CreateParcelCommand, Guid>
{
    private readonly IParcelRepository _parcelRepository;
    private readonly IUserRepository _userRepository;
    private readonly ICurrentUserService _currentUser;
    private readonly IDeliveryPointRepository _deliveryPointRepository;
    public CreateParcelCommandHandler(IParcelRepository parcelRepository, 
        IUserRepository userRepository, 
        IDeliveryPointRepository deliveryPointRepository, 
        ICurrentUserService currentUser)
    {
        _parcelRepository = parcelRepository;
        _userRepository = userRepository;
        _deliveryPointRepository = deliveryPointRepository;
        _currentUser = currentUser;
    }

    public async Task<Guid> Handle(CreateParcelCommand request, CancellationToken cancellationToken)
    {
        //todo check delivery point
        var parcelId = NewId.NextGuid();
        var options = new ParcelCreationOptions
            (
                Id: parcelId,
                DeliveryPointId: request.DeliveryPointId,
                AmountDue: request.AmountDue,
                Weight: request.Weight,
                RecipientEmail: request.RecipientEmail,
                RecipientName: request.RecipientName,
                RecipientPhoneNumber: request.RecipientPhone,
                CreatedBy: new UserInfo(_currentUser.UserId, _currentUser.Email, _currentUser.UserName, _currentUser.Role));
        var parcel = await ParcelFactory.Create(options);
        if (parcel == null) throw new ArgumentException("Failed to create parcel");
        await _parcelRepository.AddAsync(parcel, cancellationToken);
        await _parcelRepository.SaveChangesAsync(cancellationToken);
        return parcel.Id;
    }
}