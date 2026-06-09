using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GarageLogic;

namespace ConsoleUI
{
    public class ConsoleUI
    {
        private readonly Garage m_Garage;
        public ConsoleUI()
        {
            m_Garage = new Garage();
        }

        public void Start()
        {
            bool isExist = false;

            Console.WriteLine("Welcome to the Garage!");

            while (!isExist)
            {
                try
                {
                    DialogUI.PrintMenu();
                    bool isValid = Enum.TryParse(Console.ReadLine(), out eMenuChoice menuChoice);
                    if (!isValid)
                    {
                        throw new FormatException("You Typed invalid number!");
                    }
                    switch (menuChoice)
                    {
                        case eMenuChoice.ChargeVehiclesFromDataBase:
                            DialogUI.ReadVehiclesFromFile(m_Garage);
                            Console.WriteLine();
                            break;
                        case eMenuChoice.AddNewVehicle:
                            DialogUI.AddNewCar(m_Garage);
                            Console.WriteLine();
                            break;
                        case eMenuChoice.GetVehicleLicenceNumbers:
                            DialogUI.PrintAllVehiclesLicenseByStatus(m_Garage);
                            Console.WriteLine();
                            break;
                        case eMenuChoice.InflateVehicle:
                            DialogUI.InflateWheelsToMax(m_Garage);
                            Console.WriteLine();
                            break;
                        case eMenuChoice.FuelVehicle:
                            DialogUI.FuelVehicle(m_Garage);
                            Console.WriteLine();
                            break;
                        case eMenuChoice.ChargeVehicle:
                            DialogUI.ChargeElectricVehicle(m_Garage);
                            Console.WriteLine();
                            break;
                        case eMenuChoice.GetVehicleData:
                            DialogUI.PrintVehicleData(m_Garage);
                            Console.WriteLine();
                            break;
                        case eMenuChoice.ChangeVehicleStatus:
                            DialogUI.ChangeVehicleStatus(m_Garage);
                            Console.WriteLine();
                            break;
                        case eMenuChoice.Exit:
                            isExist = true;
                            break;
                        default:
                            throw new ValueRangeException("Main menu Choice", 1, 9);
                    }
                }
                catch (FormatException formatE)
                {
                    Console.WriteLine($"FormatException: {formatE.Message}");
                }
                catch (ArgumentException argumentE)
                {
                    Console.WriteLine($"ArgumentException: {argumentE.Message}");
                }
                catch (ValueRangeException rangeE)
                {
                    Console.WriteLine($"ValueRangeException: {rangeE.Message} must be between {rangeE.MinValue} - {rangeE.MaxValue}");
                }
            }
            Console.WriteLine("Thank you for using our services!");
        }
        public enum eMenuChoice
        {
            ChargeVehiclesFromDataBase = 1, AddNewVehicle, GetVehicleLicenceNumbers, ChangeVehicleStatus, InflateVehicle, FuelVehicle,
            ChargeVehicle, GetVehicleData, Exit
        }
    }
}
