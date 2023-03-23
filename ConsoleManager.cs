namespace Dealership;

public static class ConsoleManager
{
    private const string OptionTemplate = @"— {0} — {1}";
    private const int TableCellLength = -20;

    public static int InputOption(string[] options)
    {
        while (true)
        {
            Console.WriteLine("Choose an option:");
            for (var i = 0; i < options.Length; i++)
                Console.WriteLine(OptionTemplate, i, options[i]);

            var input = Console.ReadLine();
            Console.Clear();

            if (int.TryParse(input, out var option) && option >= 0 && option < options.Length) 
                return option;
            
            Console.WriteLine("There is no such option\n");
        }
    }

    public static void PrintCars(List<Car> cars)
    {
        if (cars.Count == 0)
        {
            Console.WriteLine("Cars not found");
            PrintPressAnyKey();
            return;
        }
            
        foreach (var propertyInfo in cars[0].GetType().GetProperties())
            Console.Write($"{propertyInfo.Name, TableCellLength}");
        Console.WriteLine();

        foreach (var car in cars)
        {
            foreach (var propertyInfo in car.GetType().GetProperties())
                Console.Write($"{propertyInfo.GetValue(car, null), TableCellLength}");
            Console.WriteLine();
        }

        PrintPressAnyKey();
    }

    public static Car InputCar()
    {
        var make = InputString("Enter the make of the car: ");
        var model = InputString("Enter the model of the car: ");
        var year = InputInteger("Enter the year of the car: ", Car.MinYear, DateTime.Now.Year);
        var color = InputString("Enter the color of the car: ");
        var price = InputInteger("Enter the price of the car: ", Car.MinPrice);
        var vin = InputString("Enter the VIN of the car: ");

        var result = new Car(make, model, year, color, price, vin);
        
        Console.Clear();
        return result;
    }
    
    public static string InputString(string message)
    {
        Console.Write(message);
        var result = Console.ReadLine();
        while (string.IsNullOrWhiteSpace(result))
        {
            Console.WriteLine("String is null or empty");
            Console.Write(message);
            result = Console.ReadLine();
        }

        return result.Trim();
    }

    public static int InputInteger(string message, int min = int.MinValue, int max = int.MaxValue)
    {
        int result;
        
        Console.Write(message);
        var input = Console.ReadLine();
        while (true)
        {
            if (int.TryParse(input, out result) == false)
                Console.Write($"Input is not a number\n{message}");
            else if (result < min)
                Console.Write($"The number must be >= than {min}\n{message}");
            else if (result > max)
                Console.Write($"The number must be <= than {max}\n{message}");
            else
                break;

            input = Console.ReadLine();
        }

        return result;
    }

    private static void PrintPressAnyKey()
    {
        Console.Write("\nPress to continue...");
        Console.ReadKey();
        Console.Clear();
    }
}