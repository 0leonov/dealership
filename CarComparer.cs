namespace Dealership;

public class CarComparer : IComparer<Car>
{
    private readonly CarComparisonParameter _parameter;

    public CarComparer(CarComparisonParameter parameter)
    {
        _parameter = parameter;
    }

    public int Compare(Car? x, Car? y)
    {
        if (x is null)
            throw new ArgumentException("Invalid argument: ", nameof(x));
        if (y is null)
            throw new ArgumentException("Invalid argument: ", nameof(x));
        
        return _parameter switch
        {
            CarComparisonParameter.Make => string.Compare(x.Make, y.Make, StringComparison.Ordinal),
            CarComparisonParameter.Model => string.Compare(x.Model, y.Model, StringComparison.Ordinal),
            CarComparisonParameter.Color => string.Compare(x.Color, y.Color, StringComparison.Ordinal),
            CarComparisonParameter.Vin => string.Compare(x.Vin, y.Vin, StringComparison.Ordinal),
            CarComparisonParameter.Year => x.Year.CompareTo(y.Year),
            CarComparisonParameter.Price => x.Price.CompareTo(y.Price),
            _ => throw new ArgumentException("Invalid parameter value")
        };
    }

}