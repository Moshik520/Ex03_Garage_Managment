using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageLogic
{
    public class MotorCycle : Vehicle
    {
        private const string k_LicenceType = "LicenceType";
        private const string k_EngineCapacity = "EngineCapacity";
        private const int k_NumberOfWheels = 2;
        private const float k_MaxAirPressure = 30;
        public int EngineCapacity { get; private set; }
        public eLicenseType LicenseType { get; private set; }
        

        public MotorCycle(string i_LicenseID, string i_ModelName) : base(i_LicenseID, i_ModelName, k_NumberOfWheels, k_MaxAirPressure)
        {

        }
        public override void UpdateVehicleDataDictionary(Dictionary<string, string> io_VehicleDataDic)
        {
            base.UpdateVehicleDataDictionary(io_VehicleDataDic);
            io_VehicleDataDic.Add(k_LicenceType, LicenseType.ToString());
            io_VehicleDataDic.Add(k_EngineCapacity, EngineCapacity.ToString());
        }
        public override void UpdateVehicleDataDicFromString(string[] i_SpecificValues, Dictionary<string, string> io_VehicleDataDic)
        {
            base.UpdateVehicleDataDicFromString(i_SpecificValues, io_VehicleDataDic);
            io_VehicleDataDic.Add(k_LicenceType, i_SpecificValues[8]);
            io_VehicleDataDic.Add(k_EngineCapacity, i_SpecificValues[9]);
        }
        public override void UpdateVehicleDataForUser(Dictionary<string, string> io_VehicleDataDic)
        {
            string options = string.Format(@"Enter licence type
1:A1
2:A2
3:AB
4:B2");

            base.UpdateVehicleDataForUser(io_VehicleDataDic);
            io_VehicleDataDic.Add(k_LicenceType, options);
            io_VehicleDataDic.Add(k_EngineCapacity, "Enter engine capacity");
        }
        public override Dictionary<string, string> InitValues(Dictionary<string, string> i_DicValues)
        {
            eLicenseType type;
            int capacity;
            Dictionary<string, string> errors = base.InitValues(i_DicValues);

            if (i_DicValues.ContainsKey(k_LicenceType))
            {
                if (!Enum.TryParse(i_DicValues[k_LicenceType], out type) || !Enum.IsDefined(typeof(eLicenseType), type))
                {
                    errors.Add(k_LicenceType, "please enter a valid licence type");
                }
                else
                {
                    LicenseType = type;
                }
            }
            if (i_DicValues.ContainsKey(k_EngineCapacity))
            {
                if (!int.TryParse(i_DicValues[k_EngineCapacity], out capacity))
                {
                    errors.Add(k_EngineCapacity, "please enter a valid engine capacity");
                }
                else
                {
                    EngineCapacity = capacity;
                }
            }
            return errors;

        }
        public enum eLicenseType
        {
            A = 1, A2 = 2, AB = 3, B2 = 4
        }
    }
}
