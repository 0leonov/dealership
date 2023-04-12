using Dealership.DatabaseObjects;

namespace Dealership.Helpers;

public static class ConsoleManager
{
    private const string OptionTemplate = @"— {0} — {1}";
    private const int TableCellLength = -20;
    private const int LogoWidth = 30;

    public static int InputOption(string[] options, string header = "")
    {
        while (true)
        {
            if (string.IsNullOrWhiteSpace(header) == false)
                PrintLogo(header);

            Console.WriteLine("Choose an option:");
            for (var i = 0; i < options.Length; i++)
                Console.WriteLine(OptionTemplate, i, options[i]);

            Console.Write("\n>>> ");
            var input = Console.ReadLine();
            Console.Clear();

            if (int.TryParse(input, out var option) && option >= 0 && option < options.Length)
                return option;

            Console.WriteLine("There is no such option\n");
        }
    }

    public static Car InputCar()
    {
        var make = InputString("Enter the make of the car: ");
        var model = InputString("Enter the model of the car: ");
        var year = InputInteger("Enter the year of the car (1981 <= n <= 2023):", Car.MinYear,
            DateTime.Now.Year);
        var color = InputString("Enter the color of the car: ");
        var price = InputInteger("Enter the price of the car (0 < n): ", Car.MinPrice);
        var vin = InputString("Enter the VIN of the car: ");

        Console.Clear();
        return new Car(make, model, year, color, price, vin);
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

        do
        {
            Console.Write(message);

            if (int.TryParse(Console.ReadLine(), out result) == false)
                Console.WriteLine("Input is not a number");
            else if (result < min || result > max)
                Console.WriteLine("Number out of range");
            else break;
        } while (true);

        return result;
    }

    public static int InputPropertyIndex<T>() where T : Enum
    {
        var properties = Enum.GetNames(typeof(T));
        return InputOption(properties, "Property");
    }

    public static Person InputPerson()
    {
        var personalCode = InputString("Enter the personal code: ");
        var firstname = InputString("Enter the firstname: ");
        var lastname = InputString("Enter the lastname: ");
        var phoneNumber = InputString("Enter the phone number: ");

        Console.Clear();
        return new Person(personalCode, firstname, lastname, phoneNumber);
    }

    public static void PrintTable<T>(List<T> entries) where T : DatabaseObject
    {
        if (entries.Count == 0)
        {
            Console.WriteLine("\nEntries not found");
            PrintPressAnyKey();
            return;
        }

        foreach (var propertyInfo in entries[0].GetType().GetProperties())
            Console.Write($"{propertyInfo.Name,TableCellLength}");
        Console.WriteLine();

        foreach (var car in entries)
        {
            foreach (var propertyInfo in car.GetType().GetProperties())
                Console.Write($"{propertyInfo.GetValue(car, null),TableCellLength}");
            Console.WriteLine();
        }

        Console.WriteLine($"\nTotal: {entries.Count} entries");

        PrintPressAnyKey();
    }

    private static void PrintLogo(string header)
    {
        Console.WriteLine(string.Format(
            $"*{header.PadBoth(LogoWidth - 2)}*\n"
        ));
    }

    private static void PrintPressAnyKey()
    {
        Console.Write("Press to continue...");
        Console.ReadKey();
        Console.Clear();
    }

    private static string PadBoth(this string value, int length)
    {
        var spaces = length - value.Length;
        var padLeft = spaces / 2 + value.Length;
        return value.PadLeft(padLeft).PadRight(length);
    }
}