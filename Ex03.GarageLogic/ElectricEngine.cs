using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class ElectricEngine : EnergySource
    {
        public void ChargeBattery(float i_MinutesCharge)
        {
            float hoursCharge = i_MinutesCharge / 60;

            if (CurrentAmoutOfEnergy + hoursCharge > MaxAmoutOfEnergy)
            {
                throw new ValueRangeException("Hours of Charge", 0, MaxAmoutOfEnergy);
            }
            CurrentAmoutOfEnergy += hoursCharge;
        }
    }
}
