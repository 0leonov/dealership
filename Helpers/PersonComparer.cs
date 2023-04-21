using Dealership.DatabaseObjects;
using Dealership.Properties;

namespace Dealership.Helpers;

public class PersonComparer : IComparer<Person>
{
    private readonly PersonProperty _property;

    public PersonComparer(PersonProperty property)
    {
        _property = property;
    }

    public int Compare(Person? x, Person? y)
    {
        if (x == null) throw new ArgumentNullException(nameof(x));
        if (y == null) throw new ArgumentNullException(nameof(y));
        
        return _property switch
        {
            PersonProperty.PersonalCode => string.Compare(x.PersonalCode, y.PersonalCode, StringComparison.Ordinal),
            PersonProperty.Firstname => string.Compare(x.Firstname, y.Firstname, StringComparison.Ordinal),
            PersonProperty.Lastname => string.Compare(x.Lastname, y.Lastname, StringComparison.Ordinal),
            PersonProperty.PhoneNumber => string.Compare(x.PhoneNumber, y.PhoneNumber, StringComparison.Ordinal),
            _ => throw new ArgumentException("Invalid parameter value")
        };
    }
}