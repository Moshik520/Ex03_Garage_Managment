using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageLogic
{
    public abstract class EnergySource
    {
        public float CurrentAmoutOfEnergy { get; set; }
        public float MaxAmoutOfEnergy { get; set; }
    }
}
