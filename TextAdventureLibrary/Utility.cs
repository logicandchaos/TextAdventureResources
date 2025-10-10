using System;
namespace TextAdventureLibrary
{
    public enum UtilityCategory { BasicNeeds, SocialDesires, Entertainment }
    public class Utility
    {
        public float Value { get; private set; }
        public float Max { get; private set; }
        public float Percentage { get { return Value / Max; } }
        public float PreviousValue { get; private set; }
        public float PreviousValue2 { get; private set; }
        public UtilityCategory Category { get; private set; }

        public Utility(float value, float max, UtilityCategory category = UtilityCategory.BasicNeeds)
        {
            Max = max;
            Value = Math.Max(0, Math.Min(value, max));
            Category = category;
        }

        public void ModifyValue(float amount)
        {
            Value = Math.Max(0, Math.Min(Value + amount, Max));
        }

        public void SetValue(float amount)
        {
            Value = Math.Max(0, Math.Min(amount, Max));
        }

        public void SetMax(float amount)
        {
            Max = amount;
            // Ensure current value doesn't exceed new max
            Value = Math.Max(0, Math.Min(Value, Max));
        }

        // Override ToString for debugging
        public override string ToString()
        {
            return $"{Category}: {Value:F1}/{Max:F1} ({Percentage:P1}) [Catagory: {Category.ToString()}]";
        }
    }
}