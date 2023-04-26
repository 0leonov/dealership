namespace Dealership.Databases;

public class Person : Database<DatabaseObjects.Person>
{
    public Person(string path) : base(path) { }

    public void DeleteByPersonalCode(string value)
    {
        Insert(CutEntries().Where(entry => entry.PersonalCode != value));
    }
    
    public void DeleteByFirstname(string value)
    {
        Insert(CutEntries().Where(entry => entry.Firstname != value));
    }
    
    public void DeleteByLastname(string value)
    {
        Insert(CutEntries().Where(entry => entry.Lastname != value));
    }
    
    public void DeleteByPhoneNumber(string value)
    {
        Insert(CutEntries().Where(entry => entry.PhoneNumber != value));
    }

    protected override DatabaseObjects.Person ParseLine(string line)
    {
        try
        {
            var props = line.Split(ParameterSeparator);
            return new DatabaseObjects.Person(props[0], props[1], props[2], props[3]);
        }
        catch (Exception e)
        {
            throw new Exception("Failed to parse string to Person", e);
        }
    }
}