using Dealership.DatabaseObjects;
using Dealership.Helpers;
using Dealership.Properties;

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

    public void Delete(CarProperty property, string value)
    {
        if (ConsoleManager.CheckErrorExistAndPrint("File doesn't exist", CheckFileExist() == false))
            return;

        var cars = Read();
        EmptyFile();
        Console.WriteLine(property);
        
        foreach (var car in cars.Where(car => (property == CarProperty.Make && car.Make != value) ||
                                              (property == CarProperty.Model && car.Model != value) ||
                                              (property == CarProperty.Color && car.Color != value) ||
                                              (property == CarProperty.Vin && car.Vin != value) ||
                                              (property == CarProperty.Year && car.Year.ToString() != value) ||
                                              (property == CarProperty.Price && car.Price.ToString() != value)))
        {
            Insert(car);
        }
    }
}