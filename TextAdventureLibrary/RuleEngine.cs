using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TextAdventureLibrary
{
    public class RuleEngine
    {
        static Dictionary<Rule, Action> actions = new Dictionary<Rule, Action>();

        public void AddAction(Rule rule, Action action)
        {
            actions.Add(rule, action);
        }

        public void RemoveAction(Rule rule)
        {
            actions.Remove(rule);
        }

        public static List<Action> GetAvailableActions(Person person, World world)
        {
            List<Action> availableActions = new List<Action>();

            foreach (var rule in actions)
            {
                bool conditionsMet = rule.Key.Conditions.All(condition => condition.Evaluate(person, world));
                if (conditionsMet)
                {
                    availableActions.Add(rule.Value);
                }
            }

            return availableActions;
        }

        public static Action GetFirstAvailableAction(Person person, World world)
        {
            foreach (var rule in actions)
            {
                bool conditionsMet = rule.Key.Conditions.All(condition => condition.Evaluate(person, world));
                if (conditionsMet)
                {
                    return rule.Value;
                }
            }

            return null;
        }
    }
}
