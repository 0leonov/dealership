using System.Globalization;

namespace Dealership.Helpers;

public static class Helper
{
    public static string PadBoth(this string value, int length)
    {
        var spaces = length - value.Length;
        var padLeft = spaces / 2 + value.Length;
        return value.PadLeft(padLeft).PadRight(length);
    }

    public static List<DatabaseObjects.Car> Search(this List<DatabaseObjects.Car> cars, Properties.Car property)
    {
        if (property == Properties.Car.Make)
        {
            var value = ConsoleHelper.InputString("Enter the make: ");
            return cars.FindAll(car => car.Make.Contains(value, StringComparison.OrdinalIgnoreCase));
        }
        if (property == Properties.Car.Model)
        {
            var value = ConsoleHelper.InputString("Enter the model: ");
            return cars.FindAll(car => car.Model.Contains(value, StringComparison.OrdinalIgnoreCase));
        }
        if (property == Properties.Car.Color)
        {
            var value = ConsoleHelper.InputString("Enter the color: ");
            return cars.FindAll(car => car.Color.Contains(value, StringComparison.OrdinalIgnoreCase));
        }
        if (property == Properties.Car.Vin)
        {
            var value = ConsoleHelper.InputString("Input the VIN: ");
            return cars.FindAll(car => car.Vin.Contains(value, StringComparison.OrdinalIgnoreCase));
        }

        string[] searchChoices =
        {
            "Minimum",
            "Maximum",
            "Equals"
        };

        var choice = ConsoleHelper.InputOption(searchChoices, "Filter Mode");

        if (property == Properties.Car.Year)
        {
            var year = ConsoleHelper.InputInteger("Enter the year: ");

            return choice switch
            {
                0 => cars.FindAll(car => car.Year >= year),
                1 => cars.FindAll(car => car.Year <= year),
                2 => cars.FindAll(car => car.Year == year),
                _ => throw new Exception("Invalid choice")
            };
        }
        if (property == Properties.Car.Price)
        {
            var price = ConsoleHelper.InputInteger("Enter the price: ");

            return choice switch
            {
                0 => cars.FindAll(car => car.Price >= price),
                1 => cars.FindAll(car => car.Price <= price),
                2 => cars.FindAll(car => car.Price == price),
                _ => throw new Exception("Invalid choice")
            };
        }
        
        throw new ArgumentOutOfRangeException(nameof(property));
    }
    
    public static List<DatabaseObjects.Car> Search(this List<DatabaseObjects.Car> cars, string value)
    {
        HashSet<DatabaseObjects.Car> result = new();
        
        foreach (var car in cars.FindAll(car => car.Make.Contains(value, StringComparison.OrdinalIgnoreCase)))
            result.Add(car);
        foreach (var car in cars.FindAll(car => car.Model.Contains(value, StringComparison.OrdinalIgnoreCase)))
            result.Add(car);
        foreach (var car in cars.FindAll(car => car.Color.Contains(value, StringComparison.OrdinalIgnoreCase)))
            result.Add(car);
        foreach (var car in cars.FindAll(car => car.Year.ToString().Contains(value, StringComparison.OrdinalIgnoreCase)))
            result.Add(car);
        foreach (var car in cars.FindAll(car => car.Vin.Contains(value, StringComparison.OrdinalIgnoreCase)))
            result.Add(car);
        foreach (var car in cars.FindAll(car => car.Price.ToString().Contains(value, StringComparison.OrdinalIgnoreCase)))
            result.Add(car);

        return result.ToList();
    }

    public static List<DatabaseObjects.Person> Search(this List<DatabaseObjects.Person> persons, Properties.Person property)
    {
        return property switch
        {
            Properties.Person.PersonalCode => persons.FindAll(person =>
                person.PersonalCode.Contains(ConsoleHelper.InputString("Enter the personal code: "), StringComparison.OrdinalIgnoreCase)),
            Properties.Person.Firstname => persons.FindAll(person =>
                person.Firstname.Contains(ConsoleHelper.InputString("Enter the firstname: "), StringComparison.OrdinalIgnoreCase)),
            Properties.Person.Lastname => persons.FindAll(person =>
                person.Lastname.Contains(ConsoleHelper.InputString("Enter the lastname: "), StringComparison.OrdinalIgnoreCase)),
            Properties.Person.PhoneNumber => persons.FindAll(person =>
                person.PhoneNumber.Contains(ConsoleHelper.InputString("Enter the phone number: "), StringComparison.OrdinalIgnoreCase)),
            _ => throw new ArgumentOutOfRangeException(nameof(property))
        };
    }
    
    public static List<DatabaseObjects.Person> Search(this List<DatabaseObjects.Person> persons, string value)
    {
        HashSet<DatabaseObjects.Person> result = new();
        
        foreach (var person in persons.FindAll(person => person.PersonalCode.Contains(value, StringComparison.OrdinalIgnoreCase)))
            result.Add(person);
        foreach (var person in persons.FindAll(person => person.Firstname.Contains(value, StringComparison.OrdinalIgnoreCase)))
            result.Add(person);
        foreach (var person in persons.FindAll(person => person.Lastname.Contains(value, StringComparison.OrdinalIgnoreCase)))
            result.Add(person);
        foreach (var person in persons.FindAll(person => person.PhoneNumber.Contains(value, StringComparison.OrdinalIgnoreCase)))
            result.Add(person);

        return result.ToList();
    }

    public static List<DatabaseObjects.Contract> Search(this List<DatabaseObjects.Contract> contracts, Properties.Contract property)
    {
        return property switch
        {
            Properties.Contract.PersonalCode => contracts.FindAll(contract =>
                contract.PersonalCode.Contains(ConsoleHelper.InputString("Enter the personal code: "), StringComparison.OrdinalIgnoreCase)),
            Properties.Contract.Vin => contracts.FindAll(contract =>
                contract.Vin.Contains(ConsoleHelper.InputString("Enter the VIN: "), StringComparison.OrdinalIgnoreCase)),
            Properties.Contract.Id => contracts.FindAll(contract =>
                contract.Id.Contains(ConsoleHelper.InputString("Enter the ID: "), StringComparison.OrdinalIgnoreCase)),
            Properties.Contract.Date => contracts.FindAll(contract =>
                contract.Date.ToString(CultureInfo.InvariantCulture).Contains(ConsoleHelper.InputDate().ToString(CultureInfo.InvariantCulture))),
            _ => throw new ArgumentOutOfRangeException(nameof(property))
        };
    }

    public static List<DatabaseObjects.Contract> Search(this List<DatabaseObjects.Contract> contracts, string value)
    {
        HashSet<DatabaseObjects.Contract> result = new();
        
        foreach (var contract in contracts.FindAll(contract => contract.PersonalCode.Contains(value, StringComparison.OrdinalIgnoreCase)))
            result.Add(contract);
        foreach (var contract in contracts.FindAll(contract => contract.Vin.Contains(value, StringComparison.OrdinalIgnoreCase)))
            result.Add(contract);
        foreach (var contract in contracts.FindAll(contract => contract.Id.Contains(value, StringComparison.OrdinalIgnoreCase)))
            result.Add(contract);
        foreach (var contract in contracts.FindAll(contract => contract.Date.ToString(CultureInfo.InvariantCulture).Contains(value, StringComparison.OrdinalIgnoreCase)))
            result.Add(contract);

        return result.ToList();
    }
}