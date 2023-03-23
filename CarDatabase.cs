namespace Dealership;

public static class CarDatabase
{
    private const string Path = "./car_database.txt";
    private const string ParameterSeparator = "\t";
    private const string EntrySeparator = "\n";

    public static void Insert(Car newCar)
    {
        if (CheckForDuplicate(newCar))
            throw new ArgumentException("The car is already in the database");
        
        using var streamWriter = File.AppendText(Path);
        foreach (var propertyInfo in newCar.GetType().GetProperties())
            streamWriter.Write(propertyInfo.GetValue(newCar, null) + ParameterSeparator);
        streamWriter.Write(EntrySeparator);
    }

    public static List<Car> Read()
    {
        var cars = new List<Car>();
        
        using var streamReader = new StreamReader(Path);
        while (streamReader.EndOfStream == false)
        {
            var line = streamReader.ReadLine();

            if (string.IsNullOrWhiteSpace(line))
                throw new Exception("Empty line in car database");

            cars.Add(ParseCar(line));
        }

        return cars;
    }

    public static void Replace(Car car)
    {
        if (CheckForDuplicate(car))
            throw new Exception("Car for replace not found");

        Delete(car.Vin);
        Insert(car);
    }

    public static void Delete(string vin)
    {
        var content = "";
        using (var streamReader = new StreamReader(Path))
        {
            while (streamReader.EndOfStream == false)
            {
                var line = streamReader.ReadLine();

                if (string.IsNullOrWhiteSpace(line))
                    throw new Exception("Empty line in car database");

                if (line.Contains(vin) == false)
                    content += line + '\n';
            }
        }

        using (var streamWriter = new StreamWriter(Path))
            streamWriter.Write(content);
    }

    private static Car ParseCar(string car)
    {
        try
        {
            var s = car.Split(ParameterSeparator);
            return new Car(s[0], s[1], int.Parse(s[2]), s[3], int.Parse(s[4]), s[5]);
        }
        catch (Exception e)
        {
            throw new Exception("Failed to parse string to CarInfo", e);
        }
    }

    private static bool CheckForDuplicate(Car car) => Read().Any(t => car.Vin == t.Vin);
}