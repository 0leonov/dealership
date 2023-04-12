using Dealership.DatabaseObjects;

namespace Dealership.Databases;

public class CarDatabase : Database<Car>
{
    public CarDatabase(string path) : base(path)
    {
    }

    protected override Car ParseLine(string line)
    {
        try
        {
            var props = line.Split(ParameterSeparator);
            return new Car(props[0], props[1], int.Parse(props[2]), props[3], int.Parse(props[4]), props[5]);
        }
        catch (Exception e)
        {
            throw new Exception("Failed to parse string to Car", e);
        }
    }
}