using Dealership.DatabaseObjects;
using Dealership.Properties;

namespace Dealership.Helpers;

public static class Helper
{
    public static List<Car> Search(this List<Car> cars, CarProperty property)
    {
        if (property == CarProperty.Make)
        {
            var make = ConsoleManager.InputString("Enter the make of the car: ");
            return cars.FindAll(car => car.Make.Contains(make));
        }

        if (property == CarProperty.Model)
        {
            var model = ConsoleManager.InputString("Enter the model of the car: ");
            return cars.FindAll(car => car.Model.Contains(model));
        }

        if (property == CarProperty.Color)
        {
            var color = ConsoleManager.InputString("Enter the color of the car: ");
            return cars.FindAll(car => car.Color.Contains(color));
        }

        if (property == CarProperty.Vin)
        {
            var vin = ConsoleManager.InputString("Enter the vin of the car: ");
            return cars.FindAll(car => car.Vin == vin);
        }

        string[] searchChoices =
        {
            "Minimum price",
            "Maximum price",
            "Exact price"
        };

        var choice = ConsoleManager.InputOption(searchChoices, "Filter Mode");

        if (property == CarProperty.Year)
        {
            var year = ConsoleManager.InputInteger("Enter the year of the car: ");

            return choice switch
            {
                0 => cars.FindAll(car => car.Year >= year),
                1 => cars.FindAll(car => car.Year <= year),
                2 => cars.FindAll(car => car.Year == year),
                _ => throw new Exception("Invalid choice")
            };
        }

        if (property == CarProperty.Price)
        {
            var price = ConsoleManager.InputInteger("Enter the price of the car: ");

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

    public static List<Person> Search(this List<Person> persons, PersonProperty property)
    {
        if (property == PersonProperty.PersonalCode)
        {
            var personalCode = ConsoleManager.InputString("Enter the personal code: ");
            return persons.FindAll(person => person.PersonalCode == personalCode);
        }

        if (property == PersonProperty.Firstname)
        {
            var firstname = ConsoleManager.InputString("Enter the firstname: ");
            return persons.FindAll(person => person.Firstname.Contains(firstname));
        }

        if (property == PersonProperty.Lastname)
        {
            var lastname = ConsoleManager.InputString("Enter the lastname: ");
            return persons.FindAll(person => person.Lastname.Contains(lastname));
        }

        if (property == PersonProperty.PhoneNumber)
        {
            var phoneNumber = ConsoleManager.InputString("Enter the phone number: ");
            return persons.FindAll(person => person.PhoneNumber == phoneNumber);
        }

        throw new ArgumentOutOfRangeException(nameof(property));
    }
}