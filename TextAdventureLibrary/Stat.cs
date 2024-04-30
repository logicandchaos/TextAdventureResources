namespace TextAdventureLibrary
{
    public class Stat
    {
        public int Min { get; }
        public int Max { get; }
        public float Value { get; private set; }

        public Stat(int min, int max)
        {
            Min = min;
            Max = max;
        }

        public void RollStat(Die die)
        {
            Value = die.Roll(Min, Max);
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

        public int StatCheck(Die die)
        {
            int roll = die.Roll(Min, Max + 1);
            if (roll > (int)Value)
                return 0;
            return roll;
        }
    }
}