using MediatR;

namespace Manta.Application.Commands.Parcel;

public record CreateParcelCommand(
    Guid SenderId,             
    Guid DeliveryPointId,      
    double Weight,           
    decimal AmountDue,        
    string RecipientName,     
    string RecipientPhone,
    string RecipientEmail
    ) : IRequest<Guid>;