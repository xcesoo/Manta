using MediatR;

namespace Manta.Application.Commands.Parcel;

public record CreateParcelCommand(
    int SenderId,             
    int DeliveryPointId,      
    double Weight,           
    decimal AmountDue,        
    string RecipientName,     
    string RecipientPhone,
    string RecipientEmail
    ) : IRequest<int>;