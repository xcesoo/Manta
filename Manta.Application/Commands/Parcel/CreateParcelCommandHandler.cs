using Manta.Application.Factories;
using Manta.Domain.CreationOptions;
using Manta.Domain.Interfaces;
using MediatR;

namespace Manta.Application.Commands.Parcel;

public class CreateParcelCommandHandler : IRequestHandler<CreateParcelCommand, int>
{
    private readonly IParcelRepository _parcelRepository;
    private readonly IUserRepository _userRepository;
    private readonly IDeliveryPointRepository _deliveryPointRepository;
    
    public CreateParcelCommandHandler(IParcelRepository parcelRepository, IUserRepository userRepository, IDeliveryPointRepository deliveryPointRepository)
    {
        _parcelRepository = parcelRepository;
        _userRepository = userRepository;
        _deliveryPointRepository = deliveryPointRepository;
    }

    public async Task<int> Handle(CreateParcelCommand request, CancellationToken cancellationToken)
    {
        var sender = await _userRepository.GetByIdAsync(request.SenderId);
        if (sender == null)
            throw new ArgumentException($"User with ID {request.SenderId} not found");
        //todo check delivery point
        var options = new ParcelCreationOptions
            (
                DeliveryPointId: request.DeliveryPointId,
                AmountDue: request.AmountDue,
                Weight: request.Weight,
                RecipientEmail: request.RecipientEmail,
                RecipientName: request.RecipientName,
                RecipientPhoneNumber: request.RecipientPhone,
                CreatedBy: sender);
        return await ParcelFactory.Create(options, _parcelRepository);
    }
}