using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        private readonly List<VehicleData> m_VehiclesContact = new List<VehicleData>();

        public List<VehicleData> VehiclesContact
        {
            get
            {
                return m_VehiclesContact;
            }
        }
        public void AddVehicleContact(VehicleData i_VehicleContact)
        {
            VehiclesContact.Add(i_VehicleContact);
        }
        public void AddVehiclesFromFile(string i_NameOfFile,out int o_NumOfVehiclesLoaded)
        {
            o_NumOfVehiclesLoaded = 0;
            string[] allLinesFromFile;
            string[] splitLine;
            Dictionary<string, string> newVehicleDataInDic;
            Dictionary<string, string> errors;
            Vehicle newVehicle;
            const string k_LastLine = "*****";
            allLinesFromFile = System.IO.File.ReadAllLines(i_NameOfFile);

            foreach (string line in allLinesFromFile)
            {
                if (line.Equals(k_LastLine))
                {
                    break;
                }

                splitLine = line.Split(new char[] { ',' });
                splitLine[1] = splitLine[1].Replace("-", "");
                newVehicle = GarageLogic.VehicleCreator.CreateVehicle(splitLine[0], splitLine[1], splitLine[2]);
                VehicleData vehicleData = new VehicleData(newVehicle);
                newVehicleDataInDic = new Dictionary<string, string>();
                vehicleData.GenerateVehicleDataDicFromString(splitLine, newVehicleDataInDic);
                errors = vehicleData.InitValues(newVehicleDataInDic);

                if (errors.Count == 0)
                {
                    VehiclesContact.Add(vehicleData);
                    o_NumOfVehiclesLoaded++;
                }
            }
        }
        public VehicleData FindVehicleDataByLicense(string i_LicenceNumber)
        {
            VehicleData found = null;

            foreach (VehicleData vehicleData in VehiclesContact)
            {
                if (vehicleData.Vehicle.LicenseID.Equals(i_LicenceNumber))
                {
                    found = vehicleData;
                    break;
                }
            }

            return found;
        }
        public List<VehicleData> GetContactsByStatus(VehicleData.eVehicleStatus i_Status)
        {
            List<VehicleData> filteredContacts = new List<VehicleData>();

            foreach (VehicleData vehicleContact in VehiclesContact)
            {
                if (vehicleContact.VehicleStatus.Equals(i_Status))
                {
                   filteredContacts.Add(vehicleContact);
                }
            }

            return filteredContacts;
        }
        public bool ChangeVehicleStatus(VehicleData.eVehicleStatus i_Status, string i_LicenceNumber)
        {
            bool found = false;
            VehicleData contact = FindVehicleDataByLicense(i_LicenceNumber);
            if (contact != null)
            {
                contact.VehicleStatus = i_Status;
                found = true;
            }

            return found;
        }
        public bool InflateVehicle(string i_LicenceNumber)
        {
            bool found = false;
            VehicleData vechicleContact = FindVehicleDataByLicense(i_LicenceNumber);

            if (vechicleContact != null)
            {
                found = true;

                foreach (Wheel wheel in vechicleContact.Vehicle.Wheels)
                {
                    wheel.Inflate(wheel.MaxAirPressure - wheel.CurrentAirPressure);
                }
            }

            return found;
        }
        public bool FuelVehicle(string i_LicenceNumber, float i_NumOfLitters, FuelEngine.eFuelType i_FuelType)
        {
            bool found = false;
            VehicleData vechicleContact = FindVehicleDataByLicense(i_LicenceNumber);
            
            if (vechicleContact != null)
            {
                if (!(vechicleContact.Vehicle.EnergySource is FuelEngine))
                {
                    throw new ArgumentException("Your vehicle is not able to fuel");
                }
                found = true;
                FuelEngine fuelEngine = vechicleContact.Vehicle.EnergySource as FuelEngine;
                fuelEngine.FuelVehicle(i_NumOfLitters, i_FuelType);
                vechicleContact.Vehicle.UpdateEnergyPercentage();
            }

            return found;
        }
        public bool ChargeVehicle(string i_LicenceNumber, float i_NumOfMineutes)
        {
            bool found = false;
            VehicleData vechicleContact = FindVehicleDataByLicense(i_LicenceNumber);
            
            if (vechicleContact != null)
            {
                if (!(vechicleContact.Vehicle.EnergySource is ElectricEngine))
                {
                    throw new ArgumentException("Your vehicle is not able to charge");
                }

                found = true;
                ElectricEngine electricEngine = vechicleContact.Vehicle.EnergySource as ElectricEngine;
                electricEngine.ChargeBattery(i_NumOfMineutes);
                vechicleContact.Vehicle.UpdateEnergyPercentage();  
            }
            
            return found;
        }
        public Dictionary<string, string> GetVehicleDataInDictionary(string i_LicenceNumber)
        {
            Dictionary<string, string> vehicleDataDic = new Dictionary<string, string>();
            VehicleData vehicleData = FindVehicleDataByLicense(i_LicenceNumber);

            if (vehicleData != null)
            {
                vehicleData.UpdateVehicleDataDictionary(vehicleDataDic);
            }
            else
            {
                vehicleDataDic = null;
            }

            return vehicleDataDic;
        }
    }
}
