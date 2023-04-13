using Dealership.DatabaseObjects;
using Dealership.Properties;

namespace Dealership.Helpers;

public class CarComparer : IComparer<Car>
{
    private readonly CarProperty _property;

    public CarComparer(CarProperty property)
    {
        _property = property;
    }

    public int Compare(Car x, Car y)
    {
        return _property switch
        {
            CarProperty.Make => string.Compare(x.Make, y.Make, StringComparison.Ordinal),
            CarProperty.Model => string.Compare(x.Model, y.Model, StringComparison.Ordinal),
            CarProperty.Color => string.Compare(x.Color, y.Color, StringComparison.Ordinal),
            CarProperty.Vin => string.Compare(x.Vin, y.Vin, StringComparison.Ordinal),
            CarProperty.Year => x.Year.CompareTo(y.Year),
            CarProperty.Price => x.Price.CompareTo(y.Price),
            _ => throw new ArgumentException("Invalid parameter value")
        };
    }
}