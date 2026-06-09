using GarageLogic;

namespace GarageWebApi.Dtos
{
    public class ChangeStatusRequest
    {
        public VehicleData.eVehicleStatus Status { get; set; }
    }
}
