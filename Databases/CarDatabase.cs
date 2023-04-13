using Dealership.DatabaseObjects;
using Dealership.Properties;

namespace Dealership.Databases;

public class CarDatabase : Database<Car>
{
    public CarDatabase(string path) : base(path)
    {
    }

    public void Delete(CarProperty property, string value)
    {
        // Tut konchil
        if (ConsoleManager.CheckErrorExistAndPrint("File doesn't exist", CheckFileExist() == false))
            return;

        var content = "";
        using (var streamReader = new StreamReader(_path))
        {
            while (streamReader.EndOfStream == false)
            {
                var line = streamReader.ReadLine();

                if (string.IsNullOrWhiteSpace(line))
                    throw new Exception("Empty line in database");

                if (line.Contains(primaryKey) == false)
                    content += line + '\n';
            }
        }

        using (var streamWriter = new StreamWriter(_path))
        {
            streamWriter.Write(content);
        }
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