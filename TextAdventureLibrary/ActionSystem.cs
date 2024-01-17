using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// ActionSystem - This is a static class that holds a dictionary of all actions and the rules
/// under which the Action can be performed.
/// Actions are (Person, Person) where the 1st parameter is the user and the 2nd is the target
/// Target can be the user.
/// </summary>

namespace TextAdventureLibrary
{
    public static class ActionSystem
    {
        //the rules
        static Dictionary<Rule, Action<Person, Person>> actions = new Dictionary<Rule, Action<Person, Person>>();

        public static void AddAction(Rule rule, Action<Person, Person> action)
        {
            actions.Add(rule, action);
        }

        public static void RemoveAction(Rule rule)
        {
            actions.Remove(rule);
        }

        public static List<Action<Person, Person>> GetAvailableActions(Person person, World world)
        {
            List<Action<Person, Person>> availableActions = new List<Action<Person, Person>>();

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

        public static Action<Person, Person> GetFirstAvailableAction(Person person, World world)
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
