namespace Dealership.Databases;

public class Person : Database<DatabaseObjects.Person>
{
    public Person(string path) : base(path) { }

    public List<DatabaseObjects.Person> DeleteByPersonalCode(string value)
    {
        var allPersons = CutEntries();
        var deleted = new List<DatabaseObjects.Person>();
        
        foreach (var person in allPersons)
        {
            if (person.PersonalCode != value) 
                Insert(person);
            else 
                deleted.Add(person);
        }

        return deleted;
    }
    
    public List<DatabaseObjects.Person> DeleteByFirstname(string value)
    {
        var allPersons = CutEntries();
        var deleted = new List<DatabaseObjects.Person>();
        
        foreach (var person in allPersons)
        {
            if (person.Firstname != value) 
                Insert(person);
            else 
                deleted.Add(person);
        }

        return deleted;
    }
    
    public List<DatabaseObjects.Person> DeleteByLastname(string value)
    {
        var allPersons = CutEntries();
        var deleted = new List<DatabaseObjects.Person>();
        
        foreach (var person in allPersons)
        {
            if (person.Lastname != value) 
                Insert(person);
            else 
                deleted.Add(person);
        }

        return deleted;
    }
    
    public List<DatabaseObjects.Person> DeleteByPhoneNumber(string value)
    {
        var allPersons = CutEntries();
        var deleted = new List<DatabaseObjects.Person>();
        
        foreach (var person in allPersons)
        {
            if (person.PhoneNumber != value) 
                Insert(person);
            else 
                deleted.Add(person);
        }

        return deleted;
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