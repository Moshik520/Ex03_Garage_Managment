using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class ElectricMotorcycle : MotorCycle
    {
        private const float k_MaxBattery = 3.8f;

        public ElectricMotorcycle(string i_LicenseID, string i_ModelName) : base(i_LicenseID, i_ModelName)
        {
            ElectricEngine engine = new ElectricEngine();
            engine.MaxAmoutOfEnergy = k_MaxBattery;
            EnergySource = engine;
        }
    }
}
