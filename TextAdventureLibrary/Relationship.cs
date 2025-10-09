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
        public float Attraction { get; private set; }        // -100: Platonic/Indifferent, 0: Mixed, +100: Romantic/Sexual
        public float Affection { get; private set; }         // -100: Aversion, 0: Neutral, +100: Affectionate
        public float Hierarchy { get; private set; }         // -100: Subordinate, 0: Peer, +100: Superior
        public float Familiarity { get; private set; }       // -100: Stranger, 0: Acquaintance, 50: Well Known, +100: Deeply Intimate
        public float Trust { get; private set; }             // -100: Distrust, 0: Neutral, +100: Full Trust
        public float Respect { get; private set; }           // -100: Contemptuous, 0: Neutral, +100: Respectful
        public float EmotionalDebt { get; private set; }     // -100: Resentment, 0: Neutral, +100: Gratitude
        public float Allegiance { get; private set; }        // -100: Opposing sides, 0: Neutral, +100: Allies/same side

        public List<Emotion> EmotionalHistory { get; private set; }
        public string Description { get; private set; }

        public Relationship(
            float kinship, float attraction, float affection, float hierarchy,
            float familiarity, float trust, float respect, float emotionalDebt, float allegiance)
        {
            Kinship = MathFunctions.Clamp(kinship, -100f, 100f);
            Attraction = MathFunctions.Clamp(attraction, -100f, 100f);
            Affection = MathFunctions.Clamp(affection, -100f, 100f);
            Hierarchy = MathFunctions.Clamp(hierarchy, -100f, 100f);
            Familiarity = MathFunctions.Clamp(familiarity, -100f, 100f);
            Trust = MathFunctions.Clamp(trust, -100f, 100f);
            Respect = MathFunctions.Clamp(respect, -100f, 100f);
            EmotionalDebt = MathFunctions.Clamp(emotionalDebt, -100f, 100f);
            Allegiance = MathFunctions.Clamp(allegiance, -100f, 100f);

            EmotionalHistory = new List<Emotion>();
        }

        // Setter methods
        public void SetKinship(float value) => Kinship = MathFunctions.Clamp(value, -100f, 100f);
        public void SetAttraction(float value) => Attraction = MathFunctions.Clamp(value, -100f, 100f);
        public void SetAffection(float value) => Affection = MathFunctions.Clamp(value, -100f, 100f);
        public void SetHierarchy(float value) => Hierarchy = MathFunctions.Clamp(value, -100f, 100f);
        public void SetFamiliarity(float value) => Familiarity = MathFunctions.Clamp(value, -100f, 100f);
        public void SetTrust(float value) => Trust = MathFunctions.Clamp(value, -100f, 100f);
        public void SetRespect(float value) => Respect = MathFunctions.Clamp(value, -100f, 100f);
        public void SetEmotionalDebt(float value) => EmotionalDebt = MathFunctions.Clamp(value, -100f, 100f);
        public void SetAllegiance(float value) => Allegiance = MathFunctions.Clamp(value, -100f, 100f);

        // Helper properties for common relational traits
        public bool IsCloseFamily => Kinship > 50;
        public bool IsExtendedFamily => Kinship > 0 && Kinship <= 50;
        public bool IsStranger => Familiarity < -50;
        public bool IsAcquaintance => Familiarity >= -50 && Familiarity < 25;
        public bool IsWellKnown => Familiarity >= 25 && Familiarity < 75;
        public bool IsDeeplyIntimate => Familiarity >= 75;

        public bool IsRomantic => Attraction > 50;
        public bool IsPlatonic => Attraction < -50;

        public bool Likes => Affection > 30;
        public bool Dislikes => Affection < -30;
        public bool Loves => Affection > 70;
        public bool Hates => Affection < -70;

        public bool Trusts => Trust > 30;
        public bool Distrusts => Trust < -30;

        public bool Respects => Respect > 30;
        public bool Contemptuous => Respect < -30;

        public bool Peer => Math.Abs(Hierarchy) < 20;
        public bool Superior => Hierarchy > 30;
        public bool Subordinate => Hierarchy < -30;

        public bool Grateful => EmotionalDebt > 30;
        public bool Even => Math.Abs(EmotionalDebt) < 20;
        public bool Resentful => EmotionalDebt < -30;

        public bool IsAlly => Allegiance > 30;
        public bool IsEnemy => Allegiance < -30;
        public bool IsNeutral => Math.Abs(Allegiance) < 30;

        // Add emotion toward a person
        public void AddEmotion(Emotion emotion)
        {
            EmotionalHistory.Add(emotion);
        }

        // Get average emotion toward a person
        public float GetAverageEmotion()
        {
            if (EmotionalHistory.Count == 0) return 0f;

            float total = 0.0f;
            foreach (var emotion in EmotionalHistory)
            {
                float value = emotion.Type == Emotion.EmotionType.Positive ? emotion.Intensity : -emotion.Intensity;
                total += value;
            }

            return total / EmotionalHistory.Count;
        }

        public float OverAll()
        {
            //should add modifyers based on personality type
            return (Kinship + Attraction + Affection + Hierarchy + Familiarity
                + Trust + Respect + EmotionalDebt + Allegiance) / 9;
        }

        public void GenerateDescription()
        {
            StringBuilder sb = new StringBuilder();

            // Kinship
            if (IsCloseFamily)
                sb.Append("They are a close family member. ");
            else if (IsExtendedFamily)
                sb.Append("They are an extended family member. ");
            else
                sb.Append("They are not related by family. ");

            // Familiarity
            if (IsDeeplyIntimate)
                sb.Append("You know them deeply and share a strong bond. ");
            else if (IsWellKnown)
                sb.Append("You know them well and are familiar with each other. ");
            else if (IsAcquaintance)
                sb.Append("You have met them a few times and are somewhat acquainted. ");
            else if (IsStranger)
                sb.Append("They are largely a stranger to you. ");

            // Romantic / platonic
            if (IsRomantic)
                sb.Append("There is a clear romantic or sexual interest in this relationship. ");
            else if (IsPlatonic)
                sb.Append("The relationship is purely platonic. ");

            // Affection / aversion
            if (Loves)
                sb.Append("You love them deeply. ");
            else if (Likes)
                sb.Append("You feel a strong affection toward them. ");
            else if (Hates)
                sb.Append("You hate them intensely. ");
            else if (Dislikes)
                sb.Append("You feel a distinct aversion toward them. ");

            // Trust
            if (Trusts)
                sb.Append("You trust them and feel confident in their intentions. ");
            else if (Distrusts)
                sb.Append("You distrust them and are wary of their actions. ");

            // Respect
            if (Respects)
                sb.Append("You respect them and value their qualities. ");
            else if (Contemptuous)
                sb.Append("You hold them in contempt and look down on them. ");

            // Hierarchy
            if (Superior)
                sb.Append("In terms of social or professional hierarchy, they are above you. ");
            else if (Subordinate)
                sb.Append("In terms of hierarchy, they are below you. ");
            else if (Peer)
                sb.Append("They are your peer, on equal footing with you. ");

            // Emotional Debt
            if (Grateful)
                sb.Append("You feel grateful toward them, owing them for past help. ");
            else if (Resentful)
                sb.Append("You feel resentful toward them, as if they owe you for past wrongs. ");

            // Allegiance
            if (IsAlly)
                sb.Append("You are allies, fighting for the same cause. ");
            else if (IsEnemy)
                sb.Append("You are on opposing sides, enemies in conflict. ");
            else if (IsNeutral)
                sb.Append("You are neutral toward each other politically or strategically. ");

            // Average emotion
            float average = GetAverageEmotion();
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