using System;
using System.Collections.Generic;
using System.Text;

namespace TextAdventureLibrary
{
    public class Transaction
    {
        public Person Person1 { get; private set; }
        public Person Person2 { get; private set; }
        Dictionary<string, float> utilityChange1;
        Dictionary<string, float> utilityChange2;

        public Transaction(Person p1, Dictionary<string, float> uc1, Person p2, Dictionary<string, float> uc2)
        {
            Person1 = p1;
            Person2 = p2;
            utilityChange1 = uc1;
            utilityChange2 = uc2;
        }

        public float NetValueChangeP1()
        {
            float netEffect = 0f;
            foreach (var effect in utilityChange1.Values)
            {
                netEffect += effect; //may need to normalize
            }

            return netEffect;
        }

        public float NetValueChangeP2()
        {
            float netEffect = 0f;
            foreach (var effect in utilityChange2.Values)
            {
                netEffect += effect; //may need to normalize
            }

            return netEffect;
        }

        public void MakeTransaction()
        {
            //will modify emotions and relationships here as well as utility values
            List<Emotion> emotions1 = new List<Emotion>();
            Brain brain1 = Person1.GetAttributeValue<Brain>("Brain");
            foreach (KeyValuePair<string, float> kvp in utilityChange1)
            {
                Utility utility = Person1.GetAttributeValue<Utility>(kvp.Key);
                utility.ModifyValue(kvp.Value);
                if (brain1 != null)
                    emotions1.Add(brain1.ProcessUtilityChange(utility, kvp.Value));
            }
            brain1?.UpdateCurrentEmotionalState(emotions1.ToArray());
            brain1?.GetRelationship(Person2).AddEmotion(brain1.CurrentEmotionalState);

            List<Emotion> emotions2 = new List<Emotion>();
            Brain brain2 = Person2.GetAttributeValue<Brain>("Brain");
            foreach (KeyValuePair<string, float> kvp in utilityChange2)
            {
                Utility utility = Person2.GetAttributeValue<Utility>(kvp.Key);
                utility.ModifyValue(kvp.Value);
                if (brain2 != null)
                    emotions1.Add(brain2.ProcessUtilityChange(utility, kvp.Value));
            }
            brain2?.UpdateCurrentEmotionalState(emotions2.ToArray());
            brain2?.GetRelationship(Person1).AddEmotion(brain2.CurrentEmotionalState);
        }
    }
}
