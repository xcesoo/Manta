namespace Manta.Domain.ValueObjects;

public class CarModel
{
    public string Brand { get; private set; }
    public string Model { get; private set; }

    private CarModel(string brand, string model)
    {
        Brand = brand;
        Model = model;
    }

    public static CarModel Create(string brand, string model)
    {
        if (string.IsNullOrWhiteSpace(brand) || string.IsNullOrWhiteSpace(model))
            throw new ArgumentException($"CarModel cannot be null or whitespace. -> {nameof(brand)}: {brand}, {model}");
        if (!brand.All(c => char.IsLetter(c)))
            throw new ArgumentException("The brand should only contain letters.");
        return new CarModel(brand, model);
    }
    
    public static implicit operator string(CarModel carModel) => 
        $"{carModel.Brand} - {carModel.Model}";
    
    public static implicit operator CarModel((string, string) carBrandAndModel) => 
        Create(carBrandAndModel.Item1, carBrandAndModel.Item2);
}