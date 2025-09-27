using System;
using System.Collections.Generic;
using System.Text;

namespace TextAdventureLibrary
{
    public class Emotion
    {
        public enum EmotionType { Positive, Negative }
        public enum EmotionTimeFrame { Present, Past, Future }
        public EmotionType Type { get; private set; }
        public EmotionTimeFrame TimeFrame { get; private set; }
        public float Intensity { get; private set; }
        public string Description { get; private set; }

        public Emotion(float utilityChange, EmotionTimeFrame timeFrame)
        {
            Type = utilityChange >= 0 ? EmotionType.Positive : EmotionType.Negative;
            TimeFrame = timeFrame;
            Intensity = Math.Min(1.0f, Math.Abs(utilityChange));
            Description = GenerateDescription();
        }

        private string GenerateDescription()
        {
            string intensityLevel;
            if (Intensity < 0.1f)
                intensityLevel = "Mild";
            else if (Intensity < 0.3f)
                intensityLevel = "Moderate";
            else if (Intensity < 0.6f)
                intensityLevel = "Strong";
            else
                intensityLevel = "Intense";

            string emotionWord;
            if (Type == EmotionType.Positive && TimeFrame == EmotionTimeFrame.Present)
                emotionWord = "Happy";
            else if (Type == EmotionType.Positive && TimeFrame == EmotionTimeFrame.Past)
                emotionWord = "Proud";
            else if (Type == EmotionType.Positive && TimeFrame == EmotionTimeFrame.Future)
                emotionWord = "Excited";
            else if (Type == EmotionType.Negative && TimeFrame == EmotionTimeFrame.Present)
                emotionWord = "Sad";
            else if (Type == EmotionType.Negative && TimeFrame == EmotionTimeFrame.Past)
                emotionWord = "Regretful";
            else if (Type == EmotionType.Negative && TimeFrame == EmotionTimeFrame.Future)
                emotionWord = "Anxious";
            else
                emotionWord = "Neutral";

            return $"{intensityLevel} {emotionWord}";
        }

        public override string ToString() => Description;
    }
}