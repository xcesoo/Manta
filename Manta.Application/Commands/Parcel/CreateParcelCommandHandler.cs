using Manta.Application.Factories;
using Manta.Application.Interfaces;
using Manta.Contracts;
using Manta.Domain.CreationOptions;
using Manta.Domain.Interfaces;
using Manta.Domain.ValueObjects;
using MassTransit;
using MediatR;

namespace Manta.Application.Commands.Parcel;

public class CreateParcelCommandHandler : IRequestHandler<CreateParcelCommand, Guid>
{
    private readonly ICurrentUserService _currentUser;
    private readonly IIntegrationMessageQueue _queue;
    public CreateParcelCommandHandler(ICurrentUserService currentUser, IIntegrationMessageQueue queue)
    {
        _currentUser = currentUser;
        _queue = queue;
    }

    public async Task<Guid> Handle(CreateParcelCommand request, CancellationToken cancellationToken)
    {
        //todo check delivery point
        var parcelId = NewId.NextGuid();
        var messageId = NewId.NextGuid();
        var message = new CreateParcelMessage
            (
                ParcelId: parcelId,
                MessageId: messageId,
                DeliveryPointId: request.DeliveryPointId,
                AmountDue: request.AmountDue,
                Weight: request.Weight,
                RecipientEmail: request.RecipientEmail,
                RecipientName: request.RecipientName,
                RecipientPhoneNumber: request.RecipientPhone,
                CreatedById: _currentUser.UserId,
                CreatedByName: _currentUser.UserName,
                CreatedByEmail: _currentUser.Email,
                CreatedByRole: _currentUser.Role.ToString()); 
        await _queue.EnqueueAsync(message, messageId, cancellationToken);
        return parcelId;
    }
}