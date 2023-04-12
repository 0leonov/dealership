namespace Dealership.DatabaseObjects;

public class Person : DatabaseObject
{
    public Person(string personalCode, string firstname, string lastname, string phoneNumber)
    {
        PersonalCode = personalCode;
        Firstname = firstname;
        Lastname = lastname;
        PhoneNumber = phoneNumber;
    }

    public string PersonalCode { get; }
    public string Firstname { get; }
    public string Lastname { get; }
    public string PhoneNumber { get; }

    public override string GetPrimaryKey()
    {
        return PersonalCode;
    }
}