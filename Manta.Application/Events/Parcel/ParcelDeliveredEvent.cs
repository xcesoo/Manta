using MediatR;

namespace Manta.Application.Events.Parcel;

public record ParcelDeliveredEvent(int ParcelId) : INotification;