namespace Dealership.Comparers;

public class Contract : IComparer<DatabaseObjects.Contract>
{
    private readonly Properties.Contract _property;

    public Contract(Properties.Contract property)
    {
        _property = property;
    }

    public int Compare(DatabaseObjects.Contract? x, DatabaseObjects.Contract? y)
    {
        if (x == null) throw new ArgumentNullException(nameof(x));
        if (y == null) throw new ArgumentNullException(nameof(y));
        
        return _property switch
        {
            Properties.Contract.Id => string.Compare(x.Id, y.Id, StringComparison.Ordinal),
            Properties.Contract.Vin => string.Compare(x.Vin, y.Vin, StringComparison.Ordinal),
            Properties.Contract.PersonalCode => string.Compare(x.PersonalCode, y.PersonalCode, StringComparison.Ordinal),
            Properties.Contract.Date => DateTime.Compare(x.Date, y.Date),
            _ => throw new ArgumentException("Invalid parameter value")
        };
    }
}