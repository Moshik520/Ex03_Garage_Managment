using Ex03.GarageLogic;

namespace Ex03.GarageWebApi.Dtos
{
    public class VehicleSummaryDto
    {
        public string LicenseId { get; set; }

        public string ModelName { get; set; }

        public string VehicleType { get; set; }

        public VehicleData.eVehicleStatus Status { get; set; }

        public static VehicleSummaryDto FromVehicleData(VehicleData i_VehicleData)
        {
            return new VehicleSummaryDto
            {
                LicenseId = i_VehicleData.Vehicle.LicenseID,
                ModelName = i_VehicleData.Vehicle.ModelName,
                VehicleType = i_VehicleData.Vehicle.GetType().Name,
                Status = i_VehicleData.VehicleStatus
            };
        }
    }
}
