using Dealership.Databases;
using Dealership.Helpers;
using Dealership.Properties;

namespace Dealership;

internal static class Program
{
    private static readonly CarDatabase CarDatabase = new("./car_database.txt");
    private static readonly PersonDatabase PersonDatabase = new("./person_database.txt");

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
        string[] choices =
        {
            "Return",
            "Insert data",
            "View data",
            "Sort data",
            "Search data",
            "Update data",
            "Delete data"
        };

        while (true)
        {
            var option = ConsoleManager.InputOption(choices, "Car Database");

            switch (option)
            {
                case 0:
                    return;
                case 1:
                    CarDatabase.Insert(ConsoleManager.InputCar());
                    break;
                case 2:
                    ConsoleManager.PrintTable(CarDatabase.Read());
                    break;
                case 3:
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
                    break;
                }
                case 4:
                {
                    var cars = CarDatabase.Read();
                    var property = (CarProperty)ConsoleManager.InputPropertyIndex<CarProperty>();
                    cars = cars.Search(property);
                    ConsoleManager.PrintTable(cars);
                    break;
                }
                case 5:
                    CarDatabase.Update(ConsoleManager.InputCar());
                    break;
                case 6:
                    CarDatabase.Delete(ConsoleManager.InputString("Enter the VIN of the car: "));
                    break;
                default:
                    throw new IndexOutOfRangeException(nameof(option));
            }
        }
    }

    private static void InteractWithPersonDatabase()
    {
        string[] choices =
        {
            "Return",
            "Insert data",
            "View data",
            "Sort data",
            "Search data",
            "Update data",
            "Delete data"
        };

        while (true)
        {
            var option = ConsoleManager.InputOption(choices, "Person Database");

            switch (option)
            {
                case 0:
                    return;
                case 1:
                    PersonDatabase.Insert(ConsoleManager.InputPerson());
                    break;
                case 2:
                    ConsoleManager.PrintTable(PersonDatabase.Read());
                    break;
                case 3:
                {
                    var cars = CarDatabase.Read();
                    var property = (CarProperty)ConsoleManager.InputPropertyIndex<PersonProperty>();
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
                    break;
                }
                case 4:
                {
                    var persons = PersonDatabase.Read();
                    var property = (PersonProperty)ConsoleManager.InputPropertyIndex<PersonProperty>();
                    persons = persons.Search(property);
                    ConsoleManager.PrintTable(persons);
                    break;
                }
                case 5:
                    PersonDatabase.Update(ConsoleManager.InputPerson());
                    break;
                case 6:
                    PersonDatabase.Delete(ConsoleManager.InputString("Enter the personal code of the person: "));
                    break;
                default:
                    throw new IndexOutOfRangeException(nameof(option));
            }
        }
    }
}