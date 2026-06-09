using Ex03.GarageLogic;

namespace Ex03.GarageWebApi.Dtos
{
    public class FuelRequest
    {
        public float Liters { get; set; }

        public FuelEngine.eFuelType FuelType { get; set; }
    }
}
