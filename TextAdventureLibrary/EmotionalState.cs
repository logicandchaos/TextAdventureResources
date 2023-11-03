using System;
using System.Collections.Generic;
using System.Text;

namespace TextAdventureLibrary
{
    public class EmotionalState
    {
        // An array to store emotional intensities
        private float[] emotions;

        public EmotionalState()
        {
            emotions = new float[6]; // 6 emotions: surprise, joy, fear, anger, disgust, sadness
        }

        // Indexer to access individual emotions by name
        public float this[EmotionType emotion]
        {
            get { return emotions[(int)emotion]; }
            set { emotions[(int)emotion] = (value < 0f) ? 0f : (value > 1f) ? 1f : value; }
        }

        // Reset all emotions to a neutral state
        public void ResetEmotions()
        {
            for (int i = 0; i < emotions.Length; i++)
            {
                emotions[i] = 0.5f; // Neutral emotional state (0.5 intensity)
            }
        }

        //I think I want ot update the emotional state from somewhere else, possibly Ai:Brain
        // Update emotions based on personality, outlook, and utility changes
        //public void UpdateEmotions(Personality personality, Utility utilityChanges)
        //{
        // Perform your calculations here to update emotions
        // You can use personality traits, outlook, and utility values as inputs
        // Adjust the emotions accordingly
        // Example: emotions[(int)EmotionType.Joy] = ...;
        //}
        //might have different methods for different situations
        //get emotion based on current utility levels
        //get emotion from change in utility

        public EmotionType GetHighestEmotion()
        {
            EmotionType highestEmotion = EmotionType.Surprise;
            float highestIntensity = emotions[(int)highestEmotion];

            for (int i = 1; i < emotions.Length; i++)
            {
                if (emotions[i] > highestIntensity)
                {
                    highestEmotion = (EmotionType)i;
                    highestIntensity = emotions[i];
                }
            }

            return highestEmotion;
        }
    }

    public enum EmotionType
    {
        Surprise,
        Joy,
        Fear,
        Anger,
        Disgust,
        Sadness
    }
}
