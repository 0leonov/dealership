using Dealership.DatabaseObjects;

namespace Dealership.Databases;

public class Car : Database<DatabaseObjects.Car>
{
    public Car(string path) : base(path) { }
    
    public void DeleteByMake(string value)
    {
        var allCars = CutEntries();
        var remainingCars = allCars.Where(entry => entry.Make != value);
        Insert(remainingCars);
    }
    
    public void DeleteByModel(string value)
    {
        Insert(CutEntries().Where(entry => entry.Model != value));
    }
    
    public void DeleteByYear(int value)
    {
        Insert(CutEntries().Where(entry => entry.Year != value));
    }
    
    public void DeleteByColor(string value)
    {
        Insert(CutEntries().Where(entry => entry.Color != value));
    }
    
    public void DeleteByPrice(int value)
    {
        Insert(CutEntries().Where(entry => entry.Price != value));
    }
    
    public void DeleteByVin(string value)
    {
        Insert(CutEntries().Where(entry => entry.Vin != value));
    }

    protected override DatabaseObjects.Car ParseLine(string line)
    {
        try
        {
            var props = line.Split(ParameterSeparator);
            return new DatabaseObjects.Car(props[0], props[1], int.Parse(props[2]), props[3], int.Parse(props[4]), props[5]);
        }
        catch (Exception e)
        {
            throw new Exception("Failed to parse string to Car", e);
        }
    }
}