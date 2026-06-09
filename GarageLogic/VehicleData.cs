using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageLogic
{
    public class VehicleData
    {
        private const string k_VehicleStatus = "VehicleStatus";
        private const string k_VehicleOwnerName = "VehicleOwnerName";
        private const string k_VehicleOwnerPhoneNumber = "VehicleOwnerPhoneNumber";
        public const string k_NoContent = "No content";
        public Vehicle Vehicle { get; set; }
        private string m_NameOfVehicleOwner;
        private string m_VehicleOwnerPhoneNumber;
        public eVehicleStatus VehicleStatus { get; set; }
        public VehicleData(Vehicle i_Vehicle)
        {
            VehicleStatus = eVehicleStatus.InProcess;
            m_NameOfVehicleOwner = k_NoContent;
            m_VehicleOwnerPhoneNumber = k_NoContent;
            Vehicle = i_Vehicle;
        }

        public void GenerateVehicleDataForUser(Dictionary<string, string> io_VehicleDataDic)
        {
            io_VehicleDataDic.Add(k_VehicleOwnerName, "Enter Owner Name");
            io_VehicleDataDic.Add(k_VehicleOwnerPhoneNumber, "Enter Phone Number");
            Vehicle.UpdateVehicleDataForUser(io_VehicleDataDic);
        }

        public Dictionary<string, string> InitValues(Dictionary<string, string> i_DicValues)
        {

            Dictionary<string, string> errors = new Dictionary<string, string>();
            Dictionary<string, string> vehicleErrors;

            if (i_DicValues.ContainsKey(k_VehicleOwnerName))
            {
                m_NameOfVehicleOwner = i_DicValues[k_VehicleOwnerName];
            }
            if (i_DicValues.ContainsKey(k_VehicleOwnerPhoneNumber))
            {
                m_VehicleOwnerPhoneNumber = i_DicValues[k_VehicleOwnerPhoneNumber];
            }

            vehicleErrors = Vehicle.InitValues(i_DicValues);

            foreach (var pair in vehicleErrors)
            {
                errors[pair.Key] = pair.Value; 
            }

            return errors;
        }

        public void UpdateVehicleDataDictionary(Dictionary<string, string> io_VehicleDataDic)
        {
            io_VehicleDataDic.Add(k_VehicleOwnerName,m_NameOfVehicleOwner);
            io_VehicleDataDic.Add(k_VehicleOwnerPhoneNumber, m_VehicleOwnerPhoneNumber);
            io_VehicleDataDic.Add(k_VehicleStatus, VehicleStatus.ToString());
            Vehicle.UpdateVehicleDataDictionary(io_VehicleDataDic);
        }
        public void GenerateVehicleDataDicFromString(string[] i_SpecificValues, Dictionary<string, string> io_VehicleDataDic)
        {
            Vehicle.UpdateVehicleDataDicFromString(i_SpecificValues, io_VehicleDataDic);
            io_VehicleDataDic.Add(k_VehicleOwnerName, i_SpecificValues[6]);
            io_VehicleDataDic.Add(k_VehicleOwnerPhoneNumber, i_SpecificValues[7]);
        }
        public enum eVehicleStatus
        {
            InProcess = 1, Ready = 2, Paid = 3
        }
    }
}
