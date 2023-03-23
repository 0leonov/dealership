namespace Dealership;

public class Car
{
    // 1981 year - accepted standard for VIN
    public const int MinYear = 1981;
    public const int MinPrice = 1;
    
    public string Make { get; }
    public string Model { get; }
    public int Year { get; }
    public string Color { get; }
    public int Price { get; }
    public string Vin { get; }
    
    public Car(string make, string model, int year, string color, int price, string vin)
    {
        if (year < MinYear || year > DateTime.Now.Year)
            throw new ArgumentOutOfRangeException(nameof(year));
        if (price < MinPrice)
            throw new ArgumentOutOfRangeException(nameof(price));

        string[] properties = { make, model, color, vin };
        foreach (var property in properties)
        {
            if (string.IsNullOrWhiteSpace(property))
                throw new ArgumentException(nameof(property));
        }
        
        Make = make;
        Model = model;
        Year = year;
        Color = color;
        Price = price;
        Vin = vin;
    }
}