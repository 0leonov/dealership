namespace Dealership.Databases;

public class Car : Database<DatabaseObjects.Car>
{
    public Car(string path) : base(path) { }
    
    public List<DatabaseObjects.Car> DeleteByMake(string value)
    {
        var allCars = CutEntries();
        var deleted = new List<DatabaseObjects.Car>();
        
        foreach (var car in allCars)
        {
            if (car.Make != value) 
                Insert(car);
            else 
                deleted.Add(car);
        }

        return deleted;
    }
    
    public List<DatabaseObjects.Car> DeleteByModel(string value)
    {
        var allCars = CutEntries();
        var deleted = new List<DatabaseObjects.Car>();
        
        foreach (var car in allCars)
        {
            if (car.Model != value) 
                Insert(car);
            else 
                deleted.Add(car);
        }

        return deleted;
    }
    
    public List<DatabaseObjects.Car> DeleteByYear(int value)
    {
        var allCars = CutEntries();
        var deleted = new List<DatabaseObjects.Car>();
        
        foreach (var car in allCars)
        {
            if (car.Year != value) 
                Insert(car);
            else 
                deleted.Add(car);
        }

        return deleted;
    }
    
    public List<DatabaseObjects.Car> DeleteByColor(string value)
    {
        var allCars = CutEntries();
        var deleted = new List<DatabaseObjects.Car>();
        
        foreach (var car in allCars)
        {
            if (car.Color != value) 
                Insert(car);
            else 
                deleted.Add(car);
        }

        return deleted;
    }
    
    public List<DatabaseObjects.Car> DeleteByPrice(int value)
    {
        var allCars = CutEntries();
        var deleted = new List<DatabaseObjects.Car>();
        
        foreach (var car in allCars)
        {
            if (car.Price != value) 
                Insert(car);
            else 
                deleted.Add(car);
        }

        return deleted;
    }
    
    public List<DatabaseObjects.Car> DeleteByVin(string value)
    {
        var allCars = CutEntries();
        var deleted = new List<DatabaseObjects.Car>();
        
        foreach (var car in allCars)
        {
            if (car.Vin != value) 
                Insert(car);
            else 
                deleted.Add(car);
        }

        return deleted;
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