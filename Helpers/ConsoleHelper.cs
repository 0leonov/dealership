using System.Globalization;
using Dealership.DatabaseObjects;

namespace Dealership.Helpers;

public static class ConsoleHelper
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

            Console.WriteLine("There is no such option");
        }
    }

    public static Car InputCar()
    {
        var make = InputString("Enter the make: ");
        var model = InputString("Enter the model: ");
        var year = InputYear(Car.MinYear, DateTime.Now.Year);
        var color = InputString("Enter the color: ");
        var price = InputInteger("Enter the price (0 < n): ", Car.MinPrice);
        var vin = InputVin();

        Console.Clear();
        return new Car(make, model, year, color, price, vin);
    }

    public static Contract InputContract()
    {
        var vin = InputVin();
        var personalCode = InputPersonalCode();
        var date = InputDate();
        
        Console.Clear();
        return new Contract(vin, personalCode, date);
    }

    public static DateTime InputDate()
    {
        DateTime result;
        string? input;

        do
        {
            Console.Write("Enter the date dd.mm.yyyy (skip for today's): ");
            input = Console.ReadLine();
            
            if (string.IsNullOrEmpty(input)) 
                return DateTime.Today;
            
        } while (DateTime.TryParseExact(input, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None,
                     out result) == false);
        
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
    
    public static string InputPhoneNumber()
    {
        Console.Write("Enter the phone number: ");
        var result = Console.ReadLine();
        
        while (result is null || Validator.IsPhoneNumberValid(result) == false)
        {
            Console.WriteLine("Invalid phone number");
            Console.Write("Enter the phone number: ");
            result = Console.ReadLine();
        }

        return result;
    }
    
    public static string InputVin()
    {
        Console.Write("Input the VIN: ");
        var result = Console.ReadLine();
        
        while (result is null || Validator.IsVinValid(result) == false)
        {
            Console.WriteLine("Invalid VIN");
            Console.Write("Input the VIN: ");
            result = Console.ReadLine();
        }

        return result;
    }
    
    public static string InputPersonalCode()
    {
        Console.Write("Enter the personal code: ");
        var result = Console.ReadLine();
        
        while (result is null || Validator.IsPersonalCodeValid(result) == false)
        {
            Console.WriteLine("Invalid personal code");
            Console.Write("Enter the personal code: ");
            result = Console.ReadLine();
        }

        return result;
    }

    public static int InputYear(int min=0, int max=int.MaxValue)
    {
        int result;

        do
        {
            Console.Write($"Enter the year ({min} <= n <= {max}): ");

            if (int.TryParse(Console.ReadLine(), out result) == false)
                Console.WriteLine("Input is not a number");
            else if (result < min || result > max)
                Console.WriteLine("Number out of range");
            else break;
        } while (true);

        return result;
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
        var personalCode = InputPersonalCode();
        var firstname = InputString("Enter the firstname: ");
        var lastname = InputString("Enter the lastname: ");
        var phoneNumber = InputPhoneNumber();

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

        foreach (var entry in entries)
        {
            foreach (var propertyInfo in entry.GetType().GetProperties())
                Console.Write($"{propertyInfo.GetValue(entry, null),TableCellLength}");
            Console.WriteLine();
        }

        Console.WriteLine($"\nTotal: {entries.Count} entries");

        PrintPressAnyKey();
    }
    
    public static void PrintTable<T>(List<T> entries, out int count) where T : DatabaseObject
    {
        count = entries.Count;
        
        if (count == 0)
            return;

        foreach (var propertyInfo in entries[0].GetType().GetProperties())
            Console.Write($"{propertyInfo.Name,TableCellLength}");
        Console.WriteLine();

        foreach (var entry in entries)
        {
            foreach (var propertyInfo in entry.GetType().GetProperties())
                Console.Write($"{propertyInfo.GetValue(entry, null),TableCellLength}");
            Console.WriteLine();
        }
        
        Console.WriteLine();
    }

    private static void PrintLogo(string header)
    {
        Console.WriteLine(string.Format(
            $"*{header.PadBoth(LogoWidth - 2)}*\n\n"
        ));
    }

    public static void PrintPressAnyKey()
    {
        Console.Write("Press to continue...");
        Console.ReadKey();
        Console.Clear();
    }
}