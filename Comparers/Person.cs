using Dealership.Properties;

namespace Dealership.Comparers;

public class Person : IComparer<DatabaseObjects.Person>
{
    private readonly Properties.Person _property;

    public Person(Properties.Person property)
    {
        _property = property;
    }

    public int Compare(DatabaseObjects.Person? x, DatabaseObjects.Person? y)
    {
        if (x == null) throw new ArgumentNullException(nameof(x));
        if (y == null) throw new ArgumentNullException(nameof(y));
        
        return _property switch
        {
            Properties.Person.PersonalCode => string.Compare(x.PersonalCode, y.PersonalCode, StringComparison.Ordinal),
            Properties.Person.Firstname => string.Compare(x.Firstname, y.Firstname, StringComparison.Ordinal),
            Properties.Person.Lastname => string.Compare(x.Lastname, y.Lastname, StringComparison.Ordinal),
            Properties.Person.PhoneNumber => string.Compare(x.PhoneNumber, y.PhoneNumber, StringComparison.Ordinal),
            _ => throw new ArgumentException("Invalid parameter value")
        };
    }
}