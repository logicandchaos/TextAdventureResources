using System;
using System.Collections.Generic;

namespace TextAdventureLibrary
{
    public class MenuItem
    {
        public string Text { get; private set; }
        public Action OnSelected { get; private set; }
        public Dictionary<string, float> OwnerUtilityEffects { get; set; }
        public Dictionary<string, float> TargetUtilityEffects { get; set; }
        public Person TargetPerson { get; private set; }

        public MenuItem(
            string text, Action action, Dictionary<string, float> ownerUtilityEffects = null,
            Dictionary<string, float> targetUtilityEffects = null, Person targetPerson = null)
        {
            Text = text;
            OnSelected = action;
            OwnerUtilityEffects = ownerUtilityEffects ?? new Dictionary<string, float>();
            TargetUtilityEffects = targetUtilityEffects ?? new Dictionary<string, float>();
            TargetPerson = targetPerson;
        }
    }
}
