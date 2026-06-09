using Ex03.GarageLogic;

namespace Ex03.GarageWebApi.Dtos
{
    public class ChangeStatusRequest
    {
        public VehicleData.eVehicleStatus Status { get; set; }
    }
}
