using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class FuelEngine : EnergySource
    {
        private readonly eFuelType m_eFuelType;
        public FuelEngine(eFuelType i_eFuelType)
        {
           m_eFuelType = i_eFuelType;
        }

        public void FuelVehicle(float i_NumOfLitters, eFuelType i_FuelType)
        {
            if (i_FuelType != m_eFuelType)
            {
                throw new ArgumentException("You Entered Fuel from wrong Type!");
            }
            if (CurrentAmoutOfEnergy + i_NumOfLitters > MaxAmoutOfEnergy)
            {
                throw new ValueRangeException("Current num of Litters", 0, MaxAmoutOfEnergy);
            }
            CurrentAmoutOfEnergy += i_NumOfLitters;
        }
        public enum eFuelType
        {
            Soler = 1,
            Octan95,
            Octan96,
            Octan98,
        }
    }
}
