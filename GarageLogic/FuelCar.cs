using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageLogic
{
    public class FuelCar : Car
    {
        private const float k_MaxFuel = 48;

        public FuelCar(string i_LicenseID, string i_ModelName) : base(i_LicenseID, i_ModelName)
        {
            FuelEngine engine = new FuelEngine(FuelEngine.eFuelType.Octan95);
            engine.MaxAmoutOfEnergy = k_MaxFuel;
            EnergySource = engine;
        }
    }
}
