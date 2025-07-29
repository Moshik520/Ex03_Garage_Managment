using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Car : Vehicle
    {
        private const string k_CarColor = "CarColor";
        private const string k_NumOfDoors = "NumOfDoors";
        private const int k_MinNumOfDoors = 2;
        private const int k_MaxNumOfDoors = 5;
        private const int k_NumberOfWheels = 4;
        private const float k_MaxAirPressure = 32;
        public eCarColor CarColor { get; private set; }
        public int NumOfCarDoors { get; private set; }

        public Car(string i_LicenseID, string i_ModelName) : base(i_LicenseID, i_ModelName, k_NumberOfWheels, k_MaxAirPressure)
        {

        }
        public override void UpdateVehicleDataDictionary(Dictionary<string, string> io_VehicleDataDic)
        {
            base.UpdateVehicleDataDictionary(io_VehicleDataDic);
            io_VehicleDataDic.Add(k_CarColor, CarColor.ToString());
            io_VehicleDataDic.Add(k_NumOfDoors, NumOfCarDoors.ToString());
        }

        public override void UpdateVehicleDataDicFromString(string[] i_SpecificValues, Dictionary<string, string> io_VehicleDataDic)
        {
            base.UpdateVehicleDataDicFromString(i_SpecificValues, io_VehicleDataDic);
            io_VehicleDataDic.Add(k_CarColor, i_SpecificValues[8]);
            io_VehicleDataDic.Add(k_NumOfDoors, i_SpecificValues[9]);
        }

        public override void UpdateVehicleDataForUser(Dictionary<string, string> io_VehicleDataDic)
        {
            string options = string.Format($@"Enter color of the cars
1 - {eCarColor.Yellow}
2 - {eCarColor.Black}
3 - {eCarColor.White}
4 - {eCarColor.Silver}");

            base.UpdateVehicleDataForUser(io_VehicleDataDic);
            io_VehicleDataDic.Add(k_CarColor, options);
            io_VehicleDataDic.Add(k_NumOfDoors, $"Enter num of doors between {k_MinNumOfDoors} - {k_MaxNumOfDoors}");
        }

        public override Dictionary<string, string> InitValues(Dictionary<string, string> i_DicValues)
        {
            eCarColor color;
            int numOfDoors;
            Dictionary<string, string> errors = base.InitValues(i_DicValues);

            if (i_DicValues.ContainsKey(k_CarColor))
            {
                if (!Enum.TryParse(i_DicValues[k_CarColor], out color) || !Enum.IsDefined(typeof(eCarColor), color))
                {
                    errors.Add(k_CarColor, "car color menu out of range (1 - 4)");
                }
                else
                {
                    CarColor = color;
                }
            }
            if (i_DicValues.ContainsKey(k_NumOfDoors))
            {
                if (!int.TryParse(i_DicValues[k_NumOfDoors], out numOfDoors) || !(numOfDoors >= k_MinNumOfDoors) || !(numOfDoors <= k_MaxNumOfDoors))
                {
                    errors.Add(k_NumOfDoors, $"num of doors out of range ({k_MinNumOfDoors} - {k_MaxNumOfDoors})");
                }
                else
                {
                    NumOfCarDoors = numOfDoors;
                }
            }

            return errors;
        }

        public enum eCarColor
        {
            Yellow = 1, Black = 2, White = 3, Silver = 4
        }
        public enum eNumOfDoors
        {
            Two = 2, Three, Four, Five
        }
    }
}
