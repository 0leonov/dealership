using Dealership.Helpers;

namespace Dealership;

internal static class Program
{
    private static readonly Databases.Car CarDatabase = new("./car_database.txt");
    private static readonly Databases.Person PersonDatabase = new("./person_database.txt");
    private static readonly Databases.Contract ContractDatabase = new("./contract_database.txt", CarDatabase, PersonDatabase);

    private static readonly string[] DatabaseInteractChoices =
    {
        "Return",
        "Insert data",
        "View data",
        "Sort data",
        "Search data",
        "Delete data",
    };

    public static void Main()
    {
        CarDatabase.CreateFile();
        PersonDatabase.CreateFile();
        ContractDatabase.CreateFile();
        
        string[] choices =
        {
            "Exit",
            "Car database",
            "Person database",
            "Contract database",
            "Summative search"
        };

        while (true)
        {
            var option = ConsoleHelper.InputOption(choices, "Car Dealership");

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
                case 3:
                    InteractWithContractDatabase();
                    break;
                case 4:
                    SummativeSearch(ConsoleHelper.InputString("Enter keyword: "));
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
            var option = ConsoleHelper.InputOption(DatabaseInteractChoices, "Car Database");

            if (option == 0) return;

            if (option == 1)
            {
                try
                {
                    CarDatabase.Insert(ConsoleHelper.InputCar());
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else if (option == 2)
            {
                ConsoleHelper.PrintTable(CarDatabase.Read());
            }
            else if (option == 3)
            {
                var cars = CarDatabase.Read();

                var property = (Properties.Car)ConsoleHelper.InputPropertyIndex<Properties.Car>();
                cars.Sort(new Comparers.Car(property));

                string[] orderChoices =
                {
                    "Increasing",
                    "Decreasing"
                };

                var order = ConsoleHelper.InputOption(orderChoices, "Order");

                if (order == 1)
                    cars.Reverse();

                ConsoleHelper.PrintTable(cars);
            }
            else if (option == 4)
            {
                var cars = CarDatabase.Read();

                var property = (Properties.Car)ConsoleHelper.InputPropertyIndex<Properties.Car>();
                cars = cars.Search(property);
                
                Console.Clear();
                ConsoleHelper.PrintTable(cars);
            }
            else if (option == 5)
            {
                var cars = CarDatabase.Read();
                if (!cars.Any())
                {
                    Console.WriteLine("Nothing to delete!");
                    ConsoleHelper.PrintPressAnyKey();
                    continue;
                }
                
                ConsoleHelper.PrintTable(cars);
                
                var property = (Properties.Car)ConsoleHelper.InputPropertyIndex<Properties.Car>();

                var deleted = property switch
                {
                    Properties.Car.Make => CarDatabase.DeleteByMake(ConsoleHelper.InputString("Enter the make: ")),
                    Properties.Car.Model => CarDatabase.DeleteByModel(ConsoleHelper.InputString("Enter the model: ")),
                    Properties.Car.Year => CarDatabase.DeleteByYear(ConsoleHelper.InputInteger("Enter the year: ")),
                    Properties.Car.Color => CarDatabase.DeleteByColor(ConsoleHelper.InputString("Enter the color: ")),
                    Properties.Car.Price => CarDatabase.DeleteByPrice(ConsoleHelper.InputInteger("Enter the price: ")),
                    Properties.Car.Vin => CarDatabase.DeleteByVin(ConsoleHelper.InputString("Enter the VIN: ")),
                    _ => throw new ArgumentOutOfRangeException(nameof(property))
                };

                Console.WriteLine("Deleted entries: ");
                ConsoleHelper.PrintTable(deleted);
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
            var option = ConsoleHelper.InputOption(DatabaseInteractChoices, "Person Database");
            
            if (option == 0) return;

            if (option == 1)
            {
                try
                {
                    PersonDatabase.Insert(ConsoleHelper.InputPerson());
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else if (option == 2)
            {
                ConsoleHelper.PrintTable(PersonDatabase.Read());
            }
            else if (option == 3)
            {
                List<DatabaseObjects.Person> persons;
                
                persons = PersonDatabase.Read();
                
                var property = (Properties.Person)ConsoleHelper.InputPropertyIndex<Properties.Person>();
                persons.Sort(new Comparers.Person(property));

                string[] orderChoices =
                {
                    "Increasing",
                    "Decreasing"
                };

                var order = ConsoleHelper.InputOption(orderChoices, "Order");

                if (order == 1)
                    persons.Reverse();

                ConsoleHelper.PrintTable(persons);
            }
            else if (option == 4)
            {
                var persons = PersonDatabase.Read();

                var property = (Properties.Person)ConsoleHelper.InputPropertyIndex<Properties.Person>();
                persons = persons.Search(property);
                
                Console.Clear();
                ConsoleHelper.PrintTable(persons);
            }
            else if (option == 5)
            {
                var persons = PersonDatabase.Read();
                if (!persons.Any())
                {
                    Console.WriteLine("Nothing to delete!");
                    ConsoleHelper.PrintPressAnyKey();
                    continue;
                }
                
                ConsoleHelper.PrintTable(persons);
                
                var property = (Properties.Person)ConsoleHelper.InputPropertyIndex<Properties.Person>();

                var deleted = property switch
                {
                    Properties.Person.PersonalCode => PersonDatabase.DeleteByPersonalCode(ConsoleHelper.InputString("Enter the personal code: ")),
                    Properties.Person.Firstname => PersonDatabase.DeleteByFirstname(ConsoleHelper.InputString("Enter the firstname: ")),
                    Properties.Person.Lastname => PersonDatabase.DeleteByLastname(ConsoleHelper.InputString("Enter the lastname: ")),
                    Properties.Person.PhoneNumber => PersonDatabase.DeleteByPhoneNumber(ConsoleHelper.InputString("Enter the phone number: ")),
                    _ => throw new ArgumentOutOfRangeException(nameof(property))
                };
                
                Console.WriteLine("Deleted entries: ");
                ConsoleHelper.PrintTable(deleted);
            }
            else
            {
                throw new IndexOutOfRangeException(nameof(option));
            }
        }
    }

    private static void InteractWithContractDatabase()
    {
        while (true)
        {
            var option = ConsoleHelper.InputOption(DatabaseInteractChoices, "Contract Database");
        
            if (option == 0) return;

            if (option == 1)
            {
                try
                {
                    ContractDatabase.Insert(ConsoleHelper.InputContract());
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else if (option == 2)
            {
                ConsoleHelper.PrintTable(ContractDatabase.Read());
            }
            else if (option == 3)
            {
                var contracts = ContractDatabase.Read();

                var property = (Properties.Contract)ConsoleHelper.InputPropertyIndex<Properties.Contract>();
                contracts.Sort(new Comparers.Contract(property));

                string[] orderChoices =
                {
                    "Increasing",
                    "Decreasing"
                };

                var order = ConsoleHelper.InputOption(orderChoices, "Order");

                if (order == 1)
                    contracts.Reverse();

                ConsoleHelper.PrintTable(contracts);
            }
            else if (option == 4)
            {
                var contracts = ContractDatabase.Read();

                var property = (Properties.Contract)ConsoleHelper.InputPropertyIndex<Properties.Contract>();
                contracts = contracts.Search(property);
                
                Console.Clear();
                ConsoleHelper.PrintTable(contracts);
            }
            else if (option == 5)
            {
                var contracts = ContractDatabase.Read();
                if (!contracts.Any())
                {
                    Console.WriteLine("Nothing to delete!");
                    ConsoleHelper.PrintPressAnyKey();
                    continue;
                }
                
                ConsoleHelper.PrintTable(contracts);
                
                var property = (Properties.Contract)ConsoleHelper.InputPropertyIndex<Properties.Contract>();

                var deleted = property switch
                {
                    Properties.Contract.PersonalCode => ContractDatabase.DeleteByPersonalCode(ConsoleHelper.InputString("Enter the personal code: ")),
                    Properties.Contract.Vin => ContractDatabase.DeleteByVin(ConsoleHelper.InputString("Enter the VIN: ")),
                    Properties.Contract.Date => ContractDatabase.DeleteByDate(ConsoleHelper.InputDate()),
                    Properties.Contract.Id => ContractDatabase.DeleteById(ConsoleHelper.InputString("Enter the ID: ")),
                    _ => throw new ArgumentOutOfRangeException(nameof(property))
                };
                
                Console.WriteLine("Deleted entries: ");
                ConsoleHelper.PrintTable(deleted);
            }
            else
            {
                throw new IndexOutOfRangeException(nameof(option));
            }
        }
    }

    private static void SummativeSearch(string keyword)
    {
        Console.Clear();
        
        var total = 0;
        ConsoleHelper.PrintTable(CarDatabase.Read().Search(keyword), out var count);
        total += count;
        ConsoleHelper.PrintTable(PersonDatabase.Read().Search(keyword), out count);
        total += count;
        ConsoleHelper.PrintTable(ContractDatabase.Read().Search(keyword), out count);
        total += count;
        
        Console.WriteLine($"Total: {total} entries");
        ConsoleHelper.PrintPressAnyKey();
    }
}