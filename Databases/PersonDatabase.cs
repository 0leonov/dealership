using Dealership.DatabaseObjects;
using Dealership.Helpers;
using Dealership.Properties;

namespace Dealership.Databases;

public class PersonDatabase : Database<Person>
{
    public PersonDatabase(string path) : base(path)
    {
    }
    
    public void Delete(PersonProperty property, string value)
    {
        if (ConsoleManager.CheckErrorExistAndPrint("File doesn't exist", CheckFileExist() == false))
            return;

        var persons = Read();
        EmptyFile();
        
        foreach (var person in persons.Where(person => (property == PersonProperty.PersonalCode && person.PersonalCode != value) ||
                                                       (property == PersonProperty.Firstname && person.Firstname != value) ||
                                                       (property == PersonProperty.Lastname && person.Lastname != value) ||
                                                       (property == PersonProperty.PhoneNumber && person.PhoneNumber != value)))
        {
            Insert(person);
        }
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