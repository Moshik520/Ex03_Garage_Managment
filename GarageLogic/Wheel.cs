using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GarageLogic
{
    public class Wheel
    {
        public float CurrentAirPressure { get; set; }
        public string Manufacturer { get; set; }
        private readonly float m_MaxAirPressure;

        public Wheel(float i_MaxAirPressure)
        {
            CurrentAirPressure = 0;
            m_MaxAirPressure = i_MaxAirPressure;
            Manufacturer = VehicleData.k_NoContent;
        }
        public float MaxAirPressure
        {
            get
            {
                return m_MaxAirPressure;
            }
        }
        public void Inflate(float i_AmountOfAir)
        {
            CurrentAirPressure += i_AmountOfAir;

            if (CurrentAirPressure > MaxAirPressure)
            {
                CurrentAirPressure = MaxAirPressure;
            }
        }
    }
}
