using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public class DialogUI
    {
        public DialogUI() { }

        public static void PrintMenu()
        {
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("1 - Load vehicles from file");
            Console.WriteLine("2 - Add a new car");
            Console.WriteLine("3 - Print all vehicles in the garage");
            Console.WriteLine("4 - Change vehicle status");
            Console.WriteLine("5 - Inflate wheels to maximum pressure");
            Console.WriteLine("6 - Fuel a vehicle");
            Console.WriteLine("7 - Charge an electric vehicle");
            Console.WriteLine("8 - Get vehicle details");
            Console.WriteLine("9 - Exit");
        }

        public static void AddNewCar(Garage i_Garage)
        {
            string licenceNumber;
            string userInput;
            string modelName;
            VehicleData foundVehicle;
            Vehicle vehicle;
            Dictionary<string, string> dicOutputs = new Dictionary<string, string>();
            Dictionary<string, string> newVehicleDataInDic = new Dictionary<string, string>();
            Dictionary<string, string> errors = null;

            Console.WriteLine("Please enter the license number:");
            licenceNumber = Console.ReadLine();
            Console.WriteLine();
            foundVehicle = i_Garage.FindVehicleDataByLicense(licenceNumber);
            if (foundVehicle != null)
            {
                Console.WriteLine($"This vehicle is already in the garage. Its status has been updated to: {VehicleData.eVehicleStatus.InProcess}");
                foundVehicle.VehicleStatus = VehicleData.eVehicleStatus.InProcess;
            }
            else
            {
                Console.WriteLine("Please select your vehicle type:");
                printVehicleTypes();
                if (Enum.TryParse(Console.ReadLine(), out eVehicleTypes vehicleType))
                {
                    if (!Enum.IsDefined(typeof(eVehicleTypes), vehicleType))
                    {
                        throw new ValueRangeException("vehicle type menu choice", 1, 5);
                    }

                    modelName = getVehicleModelNameFromUser();
                    vehicle = GarageLogic.VehicleCreator.CreateVehicle(vehicleType.ToString(), licenceNumber, modelName);
                    VehicleData newVehicle = new VehicleData(vehicle);
                    newVehicle.GenerateVehicleDataForUser(newVehicleDataInDic);

                    foreach (var pair in newVehicleDataInDic)
                    {
                        Console.WriteLine($"{pair.Value}");
                        userInput = Console.ReadLine();
                        dicOutputs.Add(pair.Key, userInput);
                    }

                    errors = newVehicle.InitValues(dicOutputs);
                   
                    if (errors.Count != 0)
                    {
                        printErrors(errors);
                    }
                    else
                    {
                        i_Garage.AddVehicleContact(newVehicle);
                        Console.WriteLine();
                        Console.WriteLine($"Vehicle {licenceNumber} has been added to the garage.");
                    }
                }
                else
                {
                    throw new FormatException("Vehicle Type is not Valid");
                }
            }
        }
        private static void printVehicleTypes()
        {
            Console.WriteLine("1- Electric Car");
            Console.WriteLine("2- Fuel Car");
            Console.WriteLine("3- Electric Motorcycle");
            Console.WriteLine("4- Fuel Motorcycle");
            Console.WriteLine("5- Truck");
        }
        private static string getVehicleModelNameFromUser()
        {
            Console.WriteLine("Please enter the model name:");
            string modelName = Console.ReadLine();

            return modelName;
        }
        private static void printErrors(Dictionary<string, string> i_DicErrors)
        {
            if (i_DicErrors.Count != 0)
            {
                foreach (var pairError in i_DicErrors)
                {
                    Console.WriteLine($"{pairError.Key}: {pairError.Value}");
                }
            }
        }
        public static void PrintAllVehiclesLicenseByStatus(Garage i_Garage)
        {
            const string k_PrintAll = "4";
            string userChoice;
            VehicleData.eVehicleStatus status;
            List<VehicleData> vehicleContacts;

            Console.WriteLine("Select the vehicle status to display:");
            Console.WriteLine("1 - In Process");
            Console.WriteLine("2 - Ready");
            Console.WriteLine("3 - Paid");
            Console.WriteLine("4 - All");
            userChoice = Console.ReadLine();

            if (!Enum.TryParse(userChoice, out status))
            {
                throw new FormatException("invalid menu choice");
            }
            else if (userChoice.Equals(k_PrintAll))
            {
                Console.WriteLine($"Total vehicles in all statuses: {i_Garage.VehiclesContact.Count}");
                PrintVehiclesLicense(i_Garage.VehiclesContact);
            }
            else if (!Enum.IsDefined(typeof(VehicleData.eVehicleStatus), status))
            {
                throw new ValueRangeException("status menu choice", 1, 4);
            }
            else
            {
                vehicleContacts = i_Garage.GetContactsByStatus(status);
                Console.WriteLine($"Vehicles in {status} status: {vehicleContacts.Count}");
                PrintVehiclesLicense(vehicleContacts);
            }
        }
        public static void PrintVehiclesLicense(List<VehicleData> i_VehicleDatas)
        {
            if (i_VehicleDatas.Count != 0)
            {
                foreach (VehicleData vehicleContact in i_VehicleDatas)
                {
                    Console.WriteLine($"LicenseID : {vehicleContact.Vehicle.LicenseID}, status : {vehicleContact.VehicleStatus}");
                }
            }
        }
        public static void ChangeVehicleStatus(Garage i_Garage)
        {
            bool isChanged = false;
            string licenceNumber;
            VehicleData.eVehicleStatus status;

            Console.WriteLine("Please enter the license number:");
            licenceNumber = Console.ReadLine();
            Console.WriteLine();
            Console.WriteLine("Select the new vehicle status:");
            Console.WriteLine("1 - In Process");
            Console.WriteLine("2 - Ready");
            Console.WriteLine("3 - Paid");

            if (!Enum.TryParse(Console.ReadLine(), out status))
            {
                throw new FormatException("Vehicle Status is not Valid.");
            }
            else if (!Enum.IsDefined(typeof(VehicleData.eVehicleStatus), status))
            {
                throw new ValueRangeException("type menu choice", 1, 3);
            }
            else
            {
                isChanged = i_Garage.ChangeVehicleStatus(status, licenceNumber);

                if (!isChanged)
                {
                    Console.WriteLine("The license number does not exist.");
                }
                else
                {
                    Console.WriteLine($"The status of vehicle {licenceNumber} has been updated to: {status}");
                }
            }
        }


        public static void InflateWheelsToMax(Garage i_Garage)
        {
            bool isExist;
            string licenceNumber;

            Console.WriteLine("Please enter the license number:");
            licenceNumber = Console.ReadLine();
            Console.WriteLine();
            isExist = i_Garage.InflateVehicle(licenceNumber);

            if (!isExist)
            {
                Console.WriteLine("Licence Number doesn't exist.");
            }
            else
            {
                Console.WriteLine($"All wheels of vehicle {licenceNumber} are now at full pressure.");
            }
        }

        public static void FuelVehicle(Garage i_Garage)
        {
            bool isFound = false;
            float numOfLitters;
            string licenceNumber;

            Console.WriteLine("Please Enter Your License Number: ");
            licenceNumber = Console.ReadLine();
            Console.WriteLine();

            Console.WriteLine("please enter num of litters: ");

            if (!float.TryParse(Console.ReadLine(), out numOfLitters))
            {
                throw new FormatException("Num of litters is not Valid.");
            }
            Console.WriteLine("Select the fuel type:");
            printFuelTypes();

            if (!Enum.TryParse(Console.ReadLine(), out FuelEngine.eFuelType type))
            {
                throw new FormatException("Please press a number between 1 - 4.");
            }
            else if (!Enum.IsDefined(typeof(FuelEngine.eFuelType), type))
            {
                throw new ValueRangeException("type menu choice", 1, 4);
            }
            else
            {
                isFound = i_Garage.FuelVehicle(licenceNumber, numOfLitters, type);

                if (!isFound)
                {
                    Console.WriteLine("The license number does not exist.");
                }
                else
                {
                    Console.WriteLine($"You added {numOfLitters} liters of fuel to vehicle {licenceNumber}.");
                }
            }
        }

        public static void ChargeElectricVehicle(Garage i_Garage)
        {
            string licenceNumber;
            bool isFound = false;
            float numOfMinutes;

            Console.WriteLine("Please enter the license number:");
            licenceNumber = Console.ReadLine();
            Console.WriteLine();
            Console.WriteLine("Please enter the amount of charging time (in minutes):");

            if (float.TryParse(Console.ReadLine(), out numOfMinutes))
            {
                isFound = i_Garage.ChargeVehicle(licenceNumber, numOfMinutes);

                if (!isFound)
                {
                    Console.WriteLine("The license number does not exist.");
                }
                else
                {
                    Console.WriteLine($"You added {numOfMinutes} minutes of charge to vehicle {licenceNumber}.");
                }
            }
            else
            {
                throw new FormatException("Num of minutes is not Valid.");
            }
        }
        private static void printFuelTypes()
        {
            Console.WriteLine("1 - Soler");
            Console.WriteLine("2 - Octan 95");
            Console.WriteLine("3 - Octan 96");
            Console.WriteLine("4 - Octan 98");
        }
        public static void PrintVehicleData(Garage i_Garage)
        {
            string licenceNumber;
            Dictionary<string, string> vehicleData = null;

            Console.WriteLine("Please enter the license number:");
            licenceNumber = Console.ReadLine();
            Console.WriteLine();
            vehicleData = i_Garage.GetVehicleDataInDictionary(licenceNumber);

            if (vehicleData == null)
            {
                Console.WriteLine("The license number does not exist.");
            }
            else
            {
                foreach (var par in vehicleData)
                {
                    Console.WriteLine($"{par.Key}: {par.Value}");
                }
            }
        }
        public static void ReadVehiclesFromFile(Garage i_Garage)
        {
            string nameOfFile = "Vehicles.db";
            int numOfVehiclesLoaded;

            Console.WriteLine("Loading vehicles from file...");
            i_Garage.AddVehiclesFromFile(nameOfFile, out numOfVehiclesLoaded);
            Console.WriteLine($"{numOfVehiclesLoaded} vehicles loaded");
        }
        public enum eVehicleTypes
        {
            ElectricCar = 1,
            FuelCar = 2,
            ElectricMotorcycle = 3,
            FurlMotorcycle = 4,
            Truck = 5
        }
    }
}
