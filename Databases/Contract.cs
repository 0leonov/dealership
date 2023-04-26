namespace Dealership.Databases;

public class Contract : Database<DatabaseObjects.Contract>
{
    private readonly Car _carDatabase;
    private readonly Person _personDatabase;
    
    public Contract(string path, Car carDatabase, Person personDatabase) : base(path)
    {
        _carDatabase = carDatabase;
        _personDatabase = personDatabase;
    }

    public override void Insert(DatabaseObjects.Contract entry)
    {
        if (_carDatabase.Read().Any(car => car.Vin == entry.Vin) == false)
            throw new ArgumentException($"Car with this VIN ({entry.Vin}) not found");
        if (_personDatabase.Read().Any(person => person.PersonalCode == entry.PersonalCode) == false)
            throw new ArgumentException($"Person with this person code ({entry.PersonalCode}) not found");
        
        base.Insert(entry);
    }

    public void DeleteById(string value)
    {
        Insert(CutEntries().Where(entry => entry.Id != value));
    }
    
    public void DeleteByVin(string value)
    {
        Insert(CutEntries().Where(entry => entry.Vin != value));
    }
    
    public void DeleteByPersonalCode(string value)
    {
        Insert(CutEntries().Where(entry => entry.PersonalCode != value));
    }
    
    public void DeleteByDate(DateTime value)
    {
        Insert(CutEntries().Where(entry => entry.Date != value));
    }

    protected override DatabaseObjects.Contract ParseLine(string line)
    {
        try
        {
            var props = line.Split(ParameterSeparator);
            return new DatabaseObjects.Contract(props[1], props[2], Convert.ToDateTime(props[3]), props[0]);
        }
        catch (Exception e)
        {
            throw new Exception("Failed to parse string to Contract", e);
        }
    }
}