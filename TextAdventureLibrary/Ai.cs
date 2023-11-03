using System;
using System.Collections.Generic;
using System.Text;

namespace TextAdventureLibrary
{
    public class Ai : Brain
    {
        public float Outlook { get; }
        //utility weights
        public Dictionary<Utility, float> Priorities { get; }

        EmotionalState emotionalState;
        EmotionalState mood;
        //Dictionary<Person, List<EmotionalState>> relationships;
        //Dictionary<Person, List<Utility>> relationships;//expectations

        public Ai(float outlook)//, Person person)
        {
            if (outlook > 1)
                outlook = 1;
            if (outlook < -1)
                outlook = -1;
            Outlook = outlook;

            //var utilities = person.FilterAttributesByType<Utility>();
        }

        /*public override bool GetBool()
        {
            throw new NotImplementedException();
        }

        public override char GetChar()
        {
            throw new NotImplementedException();
        }

        public override int GetInt()
        {
            throw new NotImplementedException();
        }

        public override string GetString()
        {
            throw new NotImplementedException();
        }*/

        public void SetMood(float surprise, float joy, float fear, float anger, float disgust, float sadness)
        {
            mood[EmotionType.Surprise] = surprise;
            mood[EmotionType.Joy] = joy;
            mood[EmotionType.Fear] = fear;
            mood[EmotionType.Anger] = anger;
            mood[EmotionType.Disgust] = disgust;
            mood[EmotionType.Sadness] = sadness;
        }

        public override void MakeChoice(Menu menu)
        {
            //use Priorities, outlook, relationships, and currentEmotionalState to make choice
            throw new NotImplementedException();
        }

        public void PredictedOutcomes(Menu menu)
        {
            foreach (MenuItem mi in menu.items)
            {
                //use Priorities and outlook to predict outcome/change to utiliy
            }
        }

        public void UpdateEmotionalState(Utility utility, float expectedChange, float actualChange)
        {
            float priority = Priorities[utility];
        }
    }
}