using System;

namespace Ex03.GarageLogic
{
    public class ValueRangeException : Exception
    {
        public float MinValue { get; }
        public float MaxValue { get; }

        public ValueRangeException(string i_FieldName, float i_MinValue, float i_MaxValue) : base(i_FieldName) 
        {
            MinValue = i_MinValue;
            MaxValue = i_MaxValue;
        }

    }
}
