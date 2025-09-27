using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace TextAdventureLibrary
{
    public class Relationship
    {
        // Relationship Spectrums toward another character
        public float Kinship { get; private set; }           // -100: Not Related, 0: Extended Family, +100: Direct Family
        public float RomanticInterest { get; private set; }  // -100: Platonic/Indifferent, 0: Mixed, +100: Romantic/Sexual
        public float Affection { get; private set; }         // -100: Aversion, 0: Neutral, +100: Affectionate
        public float Hierarchy { get; private set; }         // -100: Subordinate, 0: Peer, +100: Superior
        public float Familiarity { get; private set; }       // -100: Stranger, 0: Acquaintance, 50: Well Known, +100: Deeply Intimate
        public float Trust { get; private set; }             // -100: Distrust, 0: Neutral, +100: Full Trust
        public float Respect { get; private set; }           // -100: Contemptuous, 0: Neutral, +100: Respectful
        public float EmotionalDebt { get; private set; }     // -100: Resentment, 0: Neutral, +100: Gratitude
        public void SetKinship(float value) => Kinship = MathFunctions.Clamp(value, -100f, 100f);
        public void SetRomanticInterest(float value) => RomanticInterest = MathFunctions.Clamp(value, -100f, 100f);
        public void SetAffection(float value) => Affection = MathFunctions.Clamp(value, -100f, 100f);
        public void SetHierarchy(float value) => Hierarchy = MathFunctions.Clamp(value, -100f, 100f);
        public void SetFamiliarity(float value) => Familiarity = MathFunctions.Clamp(value, -100f, 100f);
        public void SetTrust(float value) => Trust = MathFunctions.Clamp(value, -100f, 100f);
        public void SetRespect(float value) => Respect = MathFunctions.Clamp(value, -100f, 100f);
        public void SetEmotionalDebt(float value) => EmotionalDebt = MathFunctions.Clamp(value, -100f, 100f);

        public List<Emotion> EmotionalHistory { get; private set; }

        public string Description { get; private set; }

        public Relationship(
        float kinship, float romanticInterest, float affection, float hierarchy,
        float familiarity, float trust, float respect, float emotionalDebt)
        {
            Kinship = MathFunctions.Clamp(kinship, -100f, 100f);
            RomanticInterest = MathFunctions.Clamp(romanticInterest, -100f, 100f);
            Affection = MathFunctions.Clamp(affection, -100f, 100f);
            Hierarchy = MathFunctions.Clamp(hierarchy, -100f, 100f);
            Familiarity = MathFunctions.Clamp(familiarity, -100f, 100f);
            Trust = MathFunctions.Clamp(trust, -100f, 100f);
            Respect = MathFunctions.Clamp(respect, -100f, 100f);
            EmotionalDebt = MathFunctions.Clamp(emotionalDebt, -100f, 100f);

            EmotionalHistory = new List<Emotion>();
        }

        // Example helper properties for common relational traits
        public bool IsCloseFamily => Kinship > 50;
        public bool IsStranger => Familiarity < -50;
        public bool IsRomantic => RomanticInterest > 50;
        public bool IsPlatonic => RomanticInterest < -50;
        public bool Likes => Affection > 30;
        public bool Dislikes => Affection < -30;
        public bool Trusts => Trust > 30;
        public bool Respects => Respect > 30;
        public bool Peer => Math.Abs(Hierarchy) < 20;
        public bool Superior => Hierarchy > 30;
        public bool Subordinate => Hierarchy < -30;
        public bool Grateful => EmotionalDebt > 30;
        public bool Even => Math.Abs(EmotionalDebt) < 20;
        public bool Resentful => EmotionalDebt < -30;

        // Add emotion toward a person
        public void AddEmotion(Emotion emotion)
        {
            EmotionalHistory.Add(emotion);
        }

        // Get average emotion toward a person
        public float GetAverageEmotion()
        {
            float total = 0.0f;
            foreach (var emotion in EmotionalHistory)
            {
                // Positive emotions are positive values, negative emotions are negative
                float value = emotion.Type == Emotion.EmotionType.Positive ? emotion.Intensity : -emotion.Intensity;
                total += value;
            }

            return total / EmotionalHistory.Count;
        }

        public void GenerateDescription()
        {
            StringBuilder sb = new StringBuilder();

            // Kinship
            if (Kinship > 50)
                sb.Append("They are a close family member. ");
            else if (Kinship > 0)
                sb.Append("They are an extended family member. ");
            else
                sb.Append("They are not related by family. ");

            // Familiarity
            if (Familiarity > 75)
                sb.Append("You know them deeply and share a strong bond. ");
            else if (Familiarity > 25)
                sb.Append("You know them well and are familiar with each other. ");
            else if (Familiarity > -25)
                sb.Append("You have met them a few times and are somewhat acquainted. ");
            else
                sb.Append("They are largely a stranger to you. ");

            // Romantic / platonic
            if (RomanticInterest > 50)
                sb.Append("There is a clear romantic or sexual interest in this relationship. ");
            else if (RomanticInterest < -50)
                sb.Append("The relationship is purely platonic. ");

            // Affection / aversion
            if (Affection > 50)
                sb.Append("You feel a strong affection toward them. ");
            else if (Affection < -50)
                sb.Append("You feel a distinct aversion toward them. ");

            // Trust
            if (Trust > 50)
                sb.Append("You trust them and feel confident in their intentions. ");
            else if (Trust < -50)
                sb.Append("You distrust them and are wary of their actions. ");

            // Respect
            if (Respect > 50)
                sb.Append("You respect them and value their qualities. ");
            else if (Respect < -50)
                sb.Append("You do not respect them and may look down on them. ");

            // Hierarchy
            if (Hierarchy > 50)
                sb.Append("In terms of social or professional hierarchy, they are above you. ");
            else if (Hierarchy < -50)
                sb.Append("In terms of hierarchy, they are below you. ");
            else
                sb.Append("They are your peer, on equal footing with you. ");

            if (EmotionalDebt > 50)
                sb.Append("You feel grateful toward them, owing them for past help. ");
            else if (EmotionalDebt < -50)
                sb.Append("You feel resentful toward them, as if they owe you for past wrongs. ");

            // Average emotion
            float average = EmotionalHistory.Count > 0 ? GetAverageEmotion() : 0f;
            if (average >= 0.6f)
                sb.Append("Overall, you love this person.");
            else if (average >= 0.3f)
                sb.Append("Overall, you like this person.");
            else if (average >= 0.1f)
                sb.Append("Overall, you feel friendly toward this person.");
            else if (average >= -0.1f)
                sb.Append("Overall, you feel neutral toward this person.");
            else if (average >= -0.3f)
                sb.Append("Overall, you dislike this person.");
            else if (average >= -0.6f)
                sb.Append("Overall, you feel hostile toward this person.");
            else
                sb.Append("Overall, you hate this person.");

            Description = sb.ToString();
        }

        // Get most recent emotion toward person
        public Emotion GetLastEmotion()
        {
            if (EmotionalHistory.Count > 0)
                return EmotionalHistory.Last();

            return null;
        }

        // Get number of interactions with person
        public int GetInteractionCount()
        {
            return EmotionalHistory.Count;
        }
    }
}
