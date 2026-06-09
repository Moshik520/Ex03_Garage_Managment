using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageLogic
{
    public class FuelMotorcycle: MotorCycle
    {
        private const float k_MaxFuel = 5.8f;

        public FuelMotorcycle(string i_LicenseID, string i_ModelName) : base(i_LicenseID, i_ModelName)
        {
            FuelEngine engine = new FuelEngine(FuelEngine.eFuelType.Octan98);
            engine.MaxAmoutOfEnergy = k_MaxFuel;
            EnergySource = engine;
        }
    }
}
