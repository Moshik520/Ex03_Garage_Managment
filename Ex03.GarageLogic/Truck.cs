using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Truck: Vehicle
    {
        private const string k_IsDangerousMaterials = "IsDangerousMaterials";
        private const string k_CarriageCapacity = "CarriageCapacity";
        private const float k_MaxFuel = 135f;
        private const int k_NumberOfWheels = 12;
        private const float k_MaxAirPressure = 27;
        public bool IsDangerousMaterials { set; private get;}
        public float CarriageCapacity { get; private set; }

        public Truck(string i_LicenseID, string i_ModelName) : base(i_LicenseID, i_ModelName, k_NumberOfWheels, k_MaxAirPressure)
        {
            FuelEngine engine = new FuelEngine(FuelEngine.eFuelType.Octan95);
            engine.MaxAmoutOfEnergy = k_MaxFuel;
            EnergySource = engine;
        }

        public override void UpdateVehicleDataDictionary(Dictionary<string, string> io_VehicleDataDic)
        {
            base.UpdateVehicleDataDictionary(io_VehicleDataDic);
            io_VehicleDataDic.Add(k_IsDangerousMaterials, IsDangerousMaterials ? eIsDangerous.Yes.ToString() : eIsDangerous.No.ToString());
            io_VehicleDataDic.Add(k_CarriageCapacity, CarriageCapacity.ToString());
        }

        public override void UpdateVehicleDataDicFromString(string[] i_SpecificValues, Dictionary<string, string> io_VehicleDataDic)
        {
            base.UpdateVehicleDataDicFromString(i_SpecificValues, io_VehicleDataDic);
            io_VehicleDataDic.Add(k_IsDangerousMaterials, i_SpecificValues[8] == "true" ? eIsDangerous.Yes.ToString() : eIsDangerous.No.ToString());
            io_VehicleDataDic.Add(k_CarriageCapacity, i_SpecificValues[9]);
        }

        public override void UpdateVehicleDataForUser(Dictionary<string, string> io_VehicleDataDic)
        {
            string options = string.Format($@"is there dangerous materials?
1.{eIsDangerous.Yes}
2.{eIsDangerous.No}");

            base.UpdateVehicleDataForUser(io_VehicleDataDic);
            io_VehicleDataDic.Add(k_IsDangerousMaterials, options);
            io_VehicleDataDic.Add(k_CarriageCapacity, "Enter carriage capacity");
        }
        public override Dictionary<string, string> InitValues(Dictionary<string, string> i_DicValues)
        {
            eIsDangerous isDangerous;
            float capacity;
            Dictionary<string, string> errors = base.InitValues(i_DicValues);

            if (i_DicValues.ContainsKey(k_IsDangerousMaterials))
            {
                if (!Enum.TryParse(i_DicValues[k_IsDangerousMaterials], out isDangerous) || !Enum.IsDefined(typeof(eIsDangerous), isDangerous))
                {
                    errors.Add(k_IsDangerousMaterials, $"please enter a {eIsDangerous.Yes} or {eIsDangerous.No} for dangerous materials");
                }
                else
                {
                    if (isDangerous.Equals(eIsDangerous.No))
                    {
                        IsDangerousMaterials = false;
                    }
                    else
                    {
                        IsDangerousMaterials = true;
                    }
                }
            }
            if (i_DicValues.ContainsKey(k_CarriageCapacity))
            {
                if (!float.TryParse(i_DicValues[k_CarriageCapacity], out capacity))
                {
                    errors.Add(k_CarriageCapacity, "please enter a valid capcity");
                }
                else
                {
                    CarriageCapacity = capacity;
                }
            }
            
            return errors;
        }
        public enum eIsDangerous
        {
          Yes = 1,No = 2
        }
    }
}
