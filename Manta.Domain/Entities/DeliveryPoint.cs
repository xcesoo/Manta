using Manta.Domain.CreationOptions;

namespace Manta.Domain.Entities;
public class DeliveryPoint
{
    public int Id { get; private set; }
    public string Address { get; private set; }
    
    private DeliveryPoint() { }
    private DeliveryPoint(int id, string address)
    {
        Id = id;
        Address = address;
    }

    internal static DeliveryPoint Create(DeliveryPointCreationOptions options)
    {
        if (options.Id <= 0) throw new ArgumentException("Id can't be null", nameof(Id));
        if (string.IsNullOrWhiteSpace(options.Address)) throw new ArgumentException("Address can't be null", nameof(options.Address));;
        return new DeliveryPoint((int)options.Id!, options.Address);
    }
    
}