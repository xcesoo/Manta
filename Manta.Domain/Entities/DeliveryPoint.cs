using Manta.Domain.CreationOptions;

namespace Manta.Domain.Entities;
public class DeliveryPoint
{
    public Guid Id { get; private set; }
    public string Address { get; private set; }
    
    private DeliveryPoint() { }
    private DeliveryPoint(Guid id, string address)
    {
        Id = id;
        Address = address;
    }

    internal static DeliveryPoint Create(DeliveryPointCreationOptions options)
    {
        if (string.IsNullOrWhiteSpace(options.Address)) throw new ArgumentException("Address can't be null", nameof(options.Address));;
        return new DeliveryPoint(options.Id, options.Address);
    }
    
}