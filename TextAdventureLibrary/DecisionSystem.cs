using System;
using System.Collections.Generic;
using System.Text;

namespace TextAdventureLibrary
{
    public static class DecisionSystem
    {
        private static Terminal terminal;
        private static Dictionary<MenuItem, List<string>> tags;
        //I want decisions to be based on Person attributes

        public static void Initialize(Terminal t)
        {
            terminal = t;
        }

        public static int Decide(Person person, Encounter encounter)
        {
            if (person.GetAttributeValue<string>("").Contains("Player"))
            {
                return terminal.GetDigit(encounter.CurrentMenu.Items.Length);
            }
            else
            {
                return new Random().Next(encounter.CurrentMenu.Items.Length);
            }
        }
    }
}
