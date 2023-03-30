namespace Dealership;

internal static class Program
{
    public static void Main(string[] args)
    {
        string[] choices = 
        {
            "Interact with car database",
            "Exit"
        };

        while (true)
        {
            var option = ConsoleManager.InputOption(choices);

            if (option == 0)
            {
                InteractWithCarDatabase();
            }
            else if (option == 1)
            {
                return;
            }
            else
            {
                throw new IndexOutOfRangeException(nameof(option));
            }
        }
    }

    private static void InteractWithCarDatabase()
    {
        string[] choices = 
        {
            "Insert data",
            "View data",
            "Sort data",
            "Search data",
            "Update data",
            "Delete data",
            "Return"
        };
        
        while (true)
        {
            var option = ConsoleManager.InputOption(choices);

            if (option == 0)
            {
                // Insert data
                CarDatabase.Insert(ConsoleManager.InputCar());
            }
            else if (option == 1)
            {
                // View data
                ConsoleManager.PrintCars(CarDatabase.Read());
            }
            else if (option == 2)
            {
                // Sort data
                var carParameters = Enum.GetNames(typeof(CarComparisonParameter));
                var index = ConsoleManager.InputOption(carParameters);
                var parameter = (CarComparisonParameter)index;
                
                var cars = CarDatabase.Read();
                cars.Sort(new CarComparer(parameter));
                
                ConsoleManager.PrintCars(cars);
            }
            else if (option == 3)
            {
                // Search data
                var carParameters = Enum.GetNames(typeof(CarComparisonParameter));
                var index = ConsoleManager.InputOption(carParameters);
                var parameter = (CarComparisonParameter)index;

                var allCars= CarDatabase.Read();

                List<Car> filteredCars;
                switch (parameter)
                {
                    case CarComparisonParameter.Make:
                        var make = ConsoleManager.InputString("Enter the make of the car: ");
                        filteredCars = allCars.FindAll(car => car.Make == make);
                        break;
                    case CarComparisonParameter.Model:
                        var model = ConsoleManager.InputString("Enter the model of the car: ");
                        filteredCars = allCars.FindAll(car => car.Model == model);
                        break;
                    case CarComparisonParameter.Year:
                        var year = ConsoleManager.InputInteger("Enter the year of the car: ");
                            filteredCars = allCars.FindAll(car => car.Year == year);
                        break;
                    case CarComparisonParameter.Color:
                        var color = ConsoleManager.InputString("Enter the color of the car: ");
                        filteredCars = allCars.FindAll(car => car.Color == color);
                        break;
                    case CarComparisonParameter.Price:
                        var price = ConsoleManager.InputInteger("Enter the price of the car: ");
                        filteredCars = allCars.FindAll(car => car.Price == price);
                        break;
                    case CarComparisonParameter.Vin:
                        var vin = ConsoleManager.InputString("Enter the vin of the car: ");
                        filteredCars = allCars.FindAll(car => car.Vin == vin);
                        break;
                    default:
                        filteredCars = new List<Car>();
                        break;
                }

                ConsoleManager.PrintCars(filteredCars);
            }
            else if (option == 4)
            {
                // Update data
                CarDatabase.Update(ConsoleManager.InputCar());
            }
            else if (option == 5)
            {
                // Delete data
                CarDatabase.Delete(ConsoleManager.InputString("Enter the VIN of the car: "));
            }
            else if (option == 6)
            {
                // Return
                return;
            }
            else
            {
                throw new IndexOutOfRangeException(nameof(option));
            }
        }
    }
}