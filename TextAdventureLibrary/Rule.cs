using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TextAdventureLibrary
{
    public class Rule
    {
        public List<Condition> Conditions { get; private set; }

        public void AddCondition(Condition condition)
        {
            Conditions.Add(condition);
        }

        public bool Evaluate(Noun noun, World world)
        {
            // All conditions must be true for the rule to be true
            return Conditions.All(condition => condition.Evaluate(noun, world));
        }
    }
}
