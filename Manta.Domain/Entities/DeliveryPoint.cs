using Manta.Domain.CreationOptions;

namespace Manta.Domain.Entities;
public class DeliveryPoint
{
    public int Id { get; private set; } = 0;
    public string Address { get; private set; }
    
    private DeliveryPoint() { }
    private DeliveryPoint(string address)
    {
        Address = address;
    }

    internal static DeliveryPoint Create(DeliveryPointCreationOptions options)
    {
        if (string.IsNullOrWhiteSpace(options.Address)) throw new ArgumentException("Address can't be null", nameof(options.Address));;
        return new DeliveryPoint(options.Address);
    }
    
}