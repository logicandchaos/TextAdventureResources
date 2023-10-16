using System;

namespace TextAdventureLibrary
{
    public class Stat
    {
        public int Min { get; }
        public int Max { get; }
        public object Value { get; private set; }

        public Stat(int min, int max)
        {
            Min = min;
            Max = max;
        }

        public void SetValue(float value)
        {
            value = (value < Min) ? Min : value;
            value = (value < Max) ? Max : value;

            Value = value;
        }

        public void IncreaseStat(float amount)
        {
            float value = (float)Value;
            value += amount;
            if (value > Max)
                value = Max;
            Value = value;
        }

        public int StatCheck(int roll)
        {
            if (roll > (int)Value)
            {
                roll = 0;
            }
            if (roll == (int)Value)
            {
                roll *= 2;
            }
            return roll;
        }
    }
}