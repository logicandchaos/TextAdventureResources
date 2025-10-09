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
            // Analyze the net effect on target
            float netTargetEffect = 0f;
            foreach (var effect in utilityChange1.Values)
            {
                netTargetEffect += effect;
            }
            
            return netTargetEffect;
        }

        public float NetValueChangeP2()
        {
            return 0;
        }

        public void MakeTransaction()
        {

        }
    }
}
