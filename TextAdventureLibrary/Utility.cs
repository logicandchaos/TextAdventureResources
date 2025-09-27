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
        public float Weight { get; private set; }
        public UtilityCategory Category { get; private set; }

        // New properties for change detection
        public float ChangeFromPrevious => Value - PreviousValue;
        public bool HasChanged => Math.Abs(ChangeFromPrevious) > 0.001f;

        public Utility(float value, float max, float weight, UtilityCategory category = UtilityCategory.BasicNeeds)
        {
            Max = max;
            Value = Math.Max(0, Math.Min(value, max));
            PreviousValue = Value;  // Start with same value (no initial change)
            PreviousValue2 = Value; // No initial change
            Weight = weight;
            Category = category;
        }

        public void ModifyValue(float amount)
        {
            PreviousValue2 = PreviousValue;
            PreviousValue = Value;
            Value = Math.Max(0, Math.Min(Value + amount, Max));
        }

        public void SetValue(float amount)
        {
            PreviousValue2 = PreviousValue;
            PreviousValue = Value;
            Value = Math.Max(0, Math.Min(amount, Max));
        }

        public void UndoChange()
        {
            Value = PreviousValue;
            PreviousValue = PreviousValue2;
        }

        public void SetMax(float amount)
        {
            Max = amount;
            // Ensure current value doesn't exceed new max
            Value = Math.Max(0, Math.Min(Value, Max));
        }

        public static float SumUtilities(params Utility[] utilities)
        {
            float sum = 0;
            foreach (Utility u in utilities)
            {
                sum += u.Percentage * u.Weight;
            }
            return sum;
        }

        // New method for weighted sum by category (following the article's hierarchy)
        public static float SumUtilitiesByCategory(params Utility[] utilities)
        {
            float sum = 0;

            // Category weights from the article (BasicNeeds > SocialDesires > Entertainment)
            var categoryWeights = new System.Collections.Generic.Dictionary<UtilityCategory, float>
            {
                { UtilityCategory.BasicNeeds, 1.0f },
                { UtilityCategory.SocialDesires, 0.7f },
                { UtilityCategory.Entertainment, 0.4f }
            };

            foreach (Utility u in utilities)
            {
                float categoryWeight = categoryWeights.ContainsKey(u.Category) ? categoryWeights[u.Category] : 1.0f;
                sum += u.Percentage * u.Weight * categoryWeight;
            }
            return sum;
        }

        // Helper method to get normalized change (useful for emotion generation)
        public float GetNormalizedChange()
        {
            return ChangeFromPrevious / Max;
        }

        // Override ToString for debugging
        public override string ToString()
        {
            return $"{Category}: {Value:F1}/{Max:F1} ({Percentage:P1}) [Weight: {Weight:F1}]";
        }
    }
}