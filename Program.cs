using Dealership.Databases;
using Dealership.Helpers;
using Dealership.Properties;

namespace Dealership;

internal static class Program
{
    private static readonly CarDatabase CarDatabase = new("./car_database.txt");
    private static readonly PersonDatabase PersonDatabase = new("./person_database.txt");

    private static readonly string[] DatabaseInteractChoices =
    {
        "Return",
        "Insert data",
        "View data",
        "Sort data",
        "Search data",
        "Delete data",
        "Create database file"
    };

    public static void Main()
    {
        string[] choices =
        {
            "Exit",
            "Interact with car database",
            "Interact with person database"
        };

        while (true)
        {
            var option = ConsoleManager.InputOption(choices, "Car Dealership");

            switch (option)
            {
                case 0:
                    return;
                case 1:
                    InteractWithCarDatabase();
                    break;
                case 2:
                    InteractWithPersonDatabase();
                    break;
                default:
                    throw new IndexOutOfRangeException(nameof(option));
            }
        }
    }

    private static void InteractWithCarDatabase()
    {
        while (true)
        {
            var option = ConsoleManager.InputOption(DatabaseInteractChoices, "Car Database");

            if (option == 0) return;

            if (option == 1)
            {
                CarDatabase.Insert(ConsoleManager.InputCar());
            }
            else if (option == 2)
            {
                ConsoleManager.PrintTable(CarDatabase.Read());
            }
            else if (option == 3)
            {
                var cars = CarDatabase.Read();
                var property = (CarProperty)ConsoleManager.InputPropertyIndex<CarProperty>();
                cars.Sort(new CarComparer(property));

                string[] orderChoices =
                {
                    "Increasing",
                    "Decreasing"
                };

                var order = ConsoleManager.InputOption(orderChoices, "Order");

                if (order == 1)
                    cars.Reverse();

                ConsoleManager.PrintTable(cars);
            }
            else if (option == 4)
            {
                var cars = CarDatabase.Read();
                var property = (CarProperty)ConsoleManager.InputPropertyIndex<CarProperty>();
                cars = cars.Search(property);
                ConsoleManager.PrintTable(cars);
            }
            else if (option == 5)
            {
                var property = (CarProperty)ConsoleManager.InputPropertyIndex<CarProperty>();
                var value = ConsoleManager.InputString($"Enter value of {property}: ");
                CarDatabase.Delete(property, value);
                Console.Clear();
            }
            else if (option == 6)
            {
                CarDatabase.CreateFile();
            }
            else
            {
                throw new IndexOutOfRangeException(nameof(option));
            }
        }
    }

    private static void InteractWithPersonDatabase()
    {
        while (true)
        {
            var option = ConsoleManager.InputOption(DatabaseInteractChoices, "Person Database");

            if (option == 0) return;
            
            if (option == 1)
            {
                PersonDatabase.Insert(ConsoleManager.InputPerson());
            }
            else if (option == 2)
            {
                ConsoleManager.PrintTable(PersonDatabase.Read());
            }
            else if (option == 3)
            {
                var persons = PersonDatabase.Read();
                var property = (PersonProperty)ConsoleManager.InputPropertyIndex<PersonProperty>();
                persons.Sort(new PersonComparer(property));

                string[] orderChoices =
                {
                    "Increasing",
                    "Decreasing"
                };

                var order = ConsoleManager.InputOption(orderChoices, "Order");

                if (order == 1)
                    persons.Reverse();

                ConsoleManager.PrintTable(persons);
            }
            else if (option == 4)
            {
                var persons = PersonDatabase.Read();
                var property = (PersonProperty)ConsoleManager.InputPropertyIndex<PersonProperty>();
                persons = persons.Search(property);
                ConsoleManager.PrintTable(persons);
            }
            else if (option == 5)
            {
                var property = (PersonProperty)ConsoleManager.InputPropertyIndex<PersonProperty>();
                var value = ConsoleManager.InputString($"Enter value of {property}: ");
                PersonDatabase.Delete(property, value);
                Console.Clear();
            }
            else if (option == 6)
            {
                PersonDatabase.CreateFile();
            }
            else
            {
                throw new IndexOutOfRangeException(nameof(option));
            }
        }
    }
}