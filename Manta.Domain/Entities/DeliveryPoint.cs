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

    internal static DeliveryPoint Create(int id, string address)
    {
        if (id <= 0) throw new ArgumentException("Id can't be null", nameof(id));
        if (string.IsNullOrWhiteSpace(address)) throw new ArgumentException("Address can't be null", nameof(address));
        return new DeliveryPoint(id, address);
    }
    
}