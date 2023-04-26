namespace Dealership.DatabaseObjects;

public class Car : DatabaseObject
{
    // 1981 year - accepted standard for VIN
    public const int MinYear = 1981;
    public const int MinPrice = 1;

    public Car(string make, string model, int year, string color, int price, string vin)
    {
        Make = make;
        Model = model;
        Year = year;
        Color = color;
        Price = price;
        Vin = vin;
    }

    public string Make { get; }
    public string Model { get; }
    public int Year { get; }
    public string Color { get; }
    public int Price { get; }
    public string Vin { get; }

    public override string GetPrimaryKey() => Vin;
}