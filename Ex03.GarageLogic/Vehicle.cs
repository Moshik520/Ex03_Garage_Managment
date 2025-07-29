using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        private const string k_TierModel = "TierModel";
        private const string k_LicensePlate = "LicensePlate";
        private const string k_CurrAirPressure = "CurrAirPressure";
        private const string k_VehicleType = "VehicleType";
        private const string k_EnergyPercentage = "EnergyPercentage";
        private const string k_ModelName = "ModelName";
        private const string k_NumOfWheels = "NumOfWheels";
        private readonly string m_ModelName;
        private readonly string m_LicenseID;
        public float SourceEnergyRemaining { get; private set; }
        public int NumOfWheels { get; private set; }
        public List<Wheel> Wheels { get; set; } = new List<Wheel>();
        public EnergySource EnergySource { get; protected set; }
       
        public string ModelName
        { 
            get
            {
                return m_ModelName;
            }
        }
        public string LicenseID
        { 
            get
            {
                return m_LicenseID;
            }
        }
        public Vehicle(string i_LicenseID, string i_ModelName, int i_NumOfWheels, float i_MaxAirPressure)
        {
            NumOfWheels = i_NumOfWheels;
            addWheels(i_MaxAirPressure);
            m_LicenseID = i_LicenseID;
            m_ModelName = i_ModelName;
        }
        private void addWheels(float i_MaxAirPressure)
        {
            for (int i = 0; i < NumOfWheels; i++)
            {
                Wheel wheel = new Wheel(i_MaxAirPressure);
                Wheels.Add(wheel);
            }  
        }
        public void UpdateEnergyPercentage()
        {
            float newPercentage = EnergySource.CurrentAmoutOfEnergy / EnergySource.MaxAmoutOfEnergy * 100;

            SourceEnergyRemaining = newPercentage;
        }

        public virtual void UpdateVehicleDataForUser(Dictionary<string, string> io_VehicleDataDic)
        {
            io_VehicleDataDic.Add(k_TierModel, "Enter Wheel Manufacturer");
            io_VehicleDataDic.Add(k_CurrAirPressure, "Please Enter Current air pressure in wheels");
            io_VehicleDataDic.Add(k_EnergyPercentage, "Please Enter Current Energy Percentage");
        }
        public virtual void UpdateVehicleDataDictionary(Dictionary<string, string> io_VehicleDataDic)
        {
            Wheel wheel;

            io_VehicleDataDic.Add(k_LicensePlate, m_LicenseID);
            io_VehicleDataDic.Add(k_ModelName, m_ModelName);
            io_VehicleDataDic.Add(k_EnergyPercentage, SourceEnergyRemaining.ToString());
            io_VehicleDataDic.Add(k_NumOfWheels, NumOfWheels.ToString());

            for (int i = 0; i < NumOfWheels; i++)
            {
                wheel = Wheels[i];
                io_VehicleDataDic.Add($"Wheel {i + 1}", $"{k_TierModel} - {wheel.Manufacturer} , {k_CurrAirPressure} - {wheel.CurrentAirPressure}");
            }
        }
        public virtual void UpdateVehicleDataDicFromString(string[] i_SpecificValues, Dictionary<string, string> io_VehicleDataDic)
        {
            io_VehicleDataDic.Add(k_VehicleType, i_SpecificValues[0]);
            io_VehicleDataDic.Add(k_LicensePlate, i_SpecificValues[1]);
            io_VehicleDataDic.Add(k_ModelName, i_SpecificValues[2]);
            io_VehicleDataDic.Add(k_EnergyPercentage, i_SpecificValues[3]);
            io_VehicleDataDic.Add(k_TierModel, i_SpecificValues[4]); 
            io_VehicleDataDic.Add(k_CurrAirPressure, i_SpecificValues[5]);
        }
        public virtual Dictionary<string, string>  InitValues(Dictionary<string, string> i_DicValues)
        {
            Dictionary<string, string> errors = new Dictionary<string, string>();
            
            if (i_DicValues.ContainsKey(k_EnergyPercentage))
            {
                if (float.TryParse(i_DicValues[k_EnergyPercentage], out float energyPercentage))
                {
                    if (energyPercentage >= 0 && energyPercentage <=100)
                    {
                        SourceEnergyRemaining = energyPercentage;
                        EnergySource.CurrentAmoutOfEnergy = EnergySource.MaxAmoutOfEnergy * SourceEnergyRemaining / 100;
                    }
                    else
                    {
                        if (!errors.ContainsKey(k_EnergyPercentage))
                        {
                            errors.Add(k_EnergyPercentage, $"Please enter energy percentage between 0 - 100");
                        }
                    }
                }
            }
            foreach (Wheel wheel in Wheels)
            {
                if (i_DicValues.ContainsKey(k_TierModel))
                {
                    wheel.Manufacturer = i_DicValues[k_TierModel];
                }
                if (i_DicValues.ContainsKey(k_CurrAirPressure))
                {
                    if (float.TryParse(i_DicValues[k_CurrAirPressure], out float airPressure))
                    {
                        if (airPressure <= wheel.MaxAirPressure)
                        { 
                            wheel.CurrentAirPressure = airPressure; 
                        }
                        else
                        {
                            if (!errors.ContainsKey(k_CurrAirPressure))
                            {
                                errors.Add(k_CurrAirPressure, $"Please enter air pressure less than Max : {wheel.MaxAirPressure}");
                            }
                        }
                    }
                    else
                    {
                        if (!errors.ContainsKey(k_CurrAirPressure))
                        {
                            errors.Add(k_CurrAirPressure, "Please enter valid wheel Air Pressure");
                        }
                    }
                }
            }

            return errors;
        }
    }
}

   

