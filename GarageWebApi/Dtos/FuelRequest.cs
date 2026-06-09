using GarageLogic;

namespace GarageWebApi.Dtos
{
    public class FuelRequest
    {
        public float Liters { get; set; }

        public FuelEngine.eFuelType FuelType { get; set; }
    }
}
