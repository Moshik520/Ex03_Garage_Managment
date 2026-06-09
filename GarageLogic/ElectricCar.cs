using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageLogic
{
    public class ElectricCar : Car
    {
        private const float k_MaxBattery = 4.8f;

        public ElectricCar(string i_LicenseID, string i_ModelName) : base(i_LicenseID, i_ModelName)
        {
            ElectricEngine engine = new ElectricEngine();
            engine.MaxAmoutOfEnergy = k_MaxBattery;
            EnergySource = engine;
        }
    }
}
