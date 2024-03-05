namespace TextAdventureLibrary
{
    public class Utility
    {
        public float Value { get; private set; }
        public float Max { get; private set; }
        public float Percentage { get { return Value / Max; } }
        public float PreviousValue { get; private set; }
        public float PreviousValue2 { get; private set; }
        public float Weight { get; private set; }

        public Utility(float value, float max, float weight)
        {
            Value = value;
            Max = max;
            PreviousValue = 0;//?
            PreviousValue2 = 0;//?
            Weight = weight;
        }

        public void ModifyValue(float amount)
        {
            PreviousValue2 = PreviousValue;
            PreviousValue = Value;
            Value += amount;
        }

        public void SetValue(float amount)
        {
            PreviousValue2 = PreviousValue;
            PreviousValue = Value;
            Value = amount;
        }

        public void UndoChange()
        {
            Value = PreviousValue;
            PreviousValue = PreviousValue2;
        }

        public void SetMax(float amount)
        {
            Max = amount;
        }

        public static float SumUtilities(params Utility[] utilities)
        {
            float sum = 0;
            foreach (Utility u in utilities)
            {
                sum += u.Percentage * u.Weight;//may change to u.Value..
            }
            return sum;
        }
    }
}
