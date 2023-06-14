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

    public List<DatabaseObjects.Contract> DeleteById(string value)
    {
        var allContracts = CutEntries();
        var deleted = new List<DatabaseObjects.Contract>();
        
        foreach (var contract in allContracts)
        {
            if (contract.Id != value) 
                Insert(contract);
            else 
                deleted.Add(contract);
        }

        return deleted;
    }
    
    public List<DatabaseObjects.Contract> DeleteByVin(string value)
    {
        var allContracts = CutEntries();
        var deleted = new List<DatabaseObjects.Contract>();
        
        foreach (var contract in allContracts)
        {
            if (contract.Vin != value) 
                Insert(contract);
            else 
                deleted.Add(contract);
        }

        return deleted;
    }
    
    public List<DatabaseObjects.Contract> DeleteByPersonalCode(string value)
    {
        var allContracts = CutEntries();
        var deleted = new List<DatabaseObjects.Contract>();
        
        foreach (var contract in allContracts)
        {
            if (contract.PersonalCode != value) 
                Insert(contract);
            else 
                deleted.Add(contract);
        }

        return deleted;
    }
    
    public List<DatabaseObjects.Contract> DeleteByDate(DateTime value)
    {
        var allContracts = CutEntries();
        var deleted = new List<DatabaseObjects.Contract>();
        
        foreach (var contract in allContracts)
        {
            if (contract.Date != value) 
                Insert(contract);
            else 
                deleted.Add(contract);
        }

        return deleted;
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