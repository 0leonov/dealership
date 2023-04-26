using Dealership.Properties;

namespace Dealership.Comparers;

public class Car : IComparer<DatabaseObjects.Car>
{
    private readonly Properties.Car _property;

    public Car(Properties.Car property)
    {
        _property = property;
    }

    public int Compare(DatabaseObjects.Car? x, DatabaseObjects.Car? y)
    {
        if (x == null) throw new ArgumentNullException(nameof(x));
        if (y == null) throw new ArgumentNullException(nameof(y));
        
        return _property switch
        {
            Properties.Car.Make => string.Compare(x.Make, y.Make, StringComparison.Ordinal),
            Properties.Car.Model => string.Compare(x.Model, y.Model, StringComparison.Ordinal),
            Properties.Car.Color => string.Compare(x.Color, y.Color, StringComparison.Ordinal),
            Properties.Car.Vin => string.Compare(x.Vin, y.Vin, StringComparison.Ordinal),
            Properties.Car.Year => x.Year.CompareTo(y.Year),
            Properties.Car.Price => x.Price.CompareTo(y.Price),
            _ => throw new ArgumentException("Invalid parameter value")
        };
    }
}