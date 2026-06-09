using System.Collections.Generic;

namespace Ex03.GarageWebApi.Dtos
{
    public class AddVehicleRequest
    {
        public string VehicleType { get; set; }

        public string LicenseId { get; set; }

        public string ModelName { get; set; }

        public Dictionary<string, string> Properties { get; set; } = new Dictionary<string, string>();
    }
}
