using System;
using System.Collections.Generic;
using System.Text;

namespace TextAdventureLibrary
{
    public class Utility
    {
        public float Value { get; private set; }
        public float Max { get; private set; }
        public float Ratio { get { return Value / Max; } }
        public float PreviousValue { get; private set; }

        public Utility(float value, float max)
        {
            Value = value;
            Max = max;
            PreviousValue = 0;//?
        }

        public void ModifyValue(float amount)
        {
            PreviousValue = Value;
            Value += amount;
        }

        public void SetValue(float amount)
        {
            PreviousValue = Value;
            Value = amount;
        }

        public void SetMax(float amount)
        {
            Max = amount;
        }
    }
}
