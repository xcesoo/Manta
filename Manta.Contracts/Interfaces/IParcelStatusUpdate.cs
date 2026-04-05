namespace Manta.Contracts.Interfaces;

public interface IParcelStatusUpdate
{
    Guid ParcelId { get; init; }
    Guid MessageId { get; init; }
}