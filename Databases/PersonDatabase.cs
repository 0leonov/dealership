using Dealership.DatabaseObjects;

namespace Dealership.Databases;

public class PersonDatabase : Database<Person>
{
    public PersonDatabase(string path) : base(path)
    {
    }

    protected override Person ParseLine(string line)
    {
        try
        {
            var props = line.Split(ParameterSeparator);
            return new Person(props[0], props[1], props[2], props[3]);
        }
        catch (Exception e)
        {
            throw new Exception("Failed to parse string to Person", e);
        }
    }
}