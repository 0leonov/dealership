using Dealership.DatabaseObjects;
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
        "Create database file"
    };

    public static void Main()
    {
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
                catch (FileNotFoundException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else if (option == 2)
            {
                try
                {
                    ConsoleHelper.PrintTable(CarDatabase.Read());
                }
                catch (FileNotFoundException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else if (option == 3)
            {
                List<DatabaseObjects.Car> cars;
                    
                try
                {
                    cars = CarDatabase.Read();
                }
                catch (FileNotFoundException e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }
                
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
                List<DatabaseObjects.Car> cars;
                
                try
                {
                    cars = CarDatabase.Read();
                }
                catch (FileNotFoundException e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }
                
                var property = (Properties.Car)ConsoleHelper.InputPropertyIndex<Properties.Car>();
                cars = cars.Search(property);
                
                Console.Clear();
                ConsoleHelper.PrintTable(cars);
            }
            else if (option == 5)
            {
                var property = (Properties.Car)ConsoleHelper.InputPropertyIndex<Properties.Car>();

                try
                {
                    switch (property)
                    {
                        case Properties.Car.Make:
                            CarDatabase.DeleteByMake(ConsoleHelper.InputString("Enter the make: "));
                            break;
                        case Properties.Car.Model:
                            CarDatabase.DeleteByModel(ConsoleHelper.InputString("Enter the make: "));
                            break;
                        case Properties.Car.Year:
                            CarDatabase.DeleteByYear(ConsoleHelper.InputInteger("Enter the year: "));
                            break;
                        case Properties.Car.Color:
                            CarDatabase.DeleteByColor(ConsoleHelper.InputString("Enter the color: "));
                            break;
                        case Properties.Car.Price:
                            CarDatabase.DeleteByPrice(ConsoleHelper.InputInteger("Enter the price: "));
                            break;
                        case Properties.Car.Vin:
                            CarDatabase.DeleteByVin(ConsoleHelper.InputString("Enter the VIN: "));
                            break;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(property));
                    }
                }
                catch (FileNotFoundException e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }
                
                Console.Clear();
            }
            else if (option == 6)
            {
                try
                {
                    CarDatabase.CreateFile();
                }
                catch (IOException e)
                {
                    Console.WriteLine(e.Message);
                }
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
                catch (FileNotFoundException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else if (option == 2)
            {
                try
                {
                    ConsoleHelper.PrintTable(PersonDatabase.Read());
                }
                catch (FileNotFoundException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else if (option == 3)
            {
                List<DatabaseObjects.Person> persons;
                
                try
                {
                    persons = PersonDatabase.Read();
                }
                catch (FileNotFoundException e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }
                
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
                List<DatabaseObjects.Person> persons;
                
                try
                {
                    persons = PersonDatabase.Read();
                }
                catch (FileNotFoundException e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }
                
                var property = (Properties.Person)ConsoleHelper.InputPropertyIndex<Properties.Person>();
                persons = persons.Search(property);
                
                Console.Clear();
                ConsoleHelper.PrintTable(persons);
            }
            else if (option == 5)
            {
                var property = (Properties.Person)ConsoleHelper.InputPropertyIndex<Properties.Person>();

                try
                {
                    switch (property)
                    {
                        case Properties.Person.PersonalCode:
                            PersonDatabase.DeleteByPersonalCode(ConsoleHelper.InputString("Enter the personal code: "));
                            break;
                        case Properties.Person.Firstname:
                            PersonDatabase.DeleteByFirstname(ConsoleHelper.InputString("Enter the firstname: "));
                            break;
                        case Properties.Person.Lastname:
                            PersonDatabase.DeleteByLastname(ConsoleHelper.InputString("Enter the lastname: "));
                            break;
                        case Properties.Person.PhoneNumber:
                            PersonDatabase.DeleteByPhoneNumber(ConsoleHelper.InputString("Enter the phone number: "));
                            break;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(property));
                    }
                }
                catch (FileNotFoundException e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }
                
                Console.Clear();
            }
            else if (option == 6)
            {
                try
                {
                    PersonDatabase.CreateFile();
                }
                catch (IOException e)
                {
                    Console.WriteLine(e.Message);
                }
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
                catch (FileNotFoundException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else if (option == 2)
            {
                try
                {
                    ConsoleHelper.PrintTable(ContractDatabase.Read());
                }
                catch (FileNotFoundException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else if (option == 3)
            {
                List<DatabaseObjects.Contract> contracts;
                
                try
                {
                    contracts = ContractDatabase.Read();
                }
                catch (FileNotFoundException e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }
                
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
                List<DatabaseObjects.Contract> contracts;
                
                try
                {
                    contracts = ContractDatabase.Read();
                }
                catch (FileNotFoundException e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }
                
                var property = (Properties.Contract)ConsoleHelper.InputPropertyIndex<Properties.Contract>();
                contracts = contracts.Search(property);
                
                Console.Clear();
                ConsoleHelper.PrintTable(contracts);
            }
            else if (option == 5)
            {
                var property = (Properties.Contract)ConsoleHelper.InputPropertyIndex<Properties.Contract>();

                try
                {
                    switch (property)
                    {
                        case Properties.Contract.PersonalCode:
                            ContractDatabase.DeleteByPersonalCode(ConsoleHelper.InputString("Enter the personal code: "));
                            break;
                        case Properties.Contract.Vin:
                            ContractDatabase.DeleteByVin(ConsoleHelper.InputString("Enter the VIN: "));
                            break;
                        case Properties.Contract.Date:
                            ContractDatabase.DeleteByDate(ConsoleHelper.InputDate());
                            break;
                        case Properties.Contract.Id:
                            ContractDatabase.DeleteById(ConsoleHelper.InputString("Enter the ID: "));
                            break;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(property));
                    }
                }
                catch (FileNotFoundException e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }
                
                Console.Clear();
            }
            else if (option == 6)
            {
                try
                {
                    ContractDatabase.CreateFile();
                }
                catch (IOException e)
                {
                    Console.WriteLine(e.Message);
                }
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
        
        Console.WriteLine($"\nTotal: {total} entries");
        ConsoleHelper.PrintPressAnyKey();
    }
}