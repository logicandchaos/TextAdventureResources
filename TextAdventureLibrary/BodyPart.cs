using System;
using System.Collections.Generic;

namespace TextAdventureLibrary
{
    public class BodyPart
    {
        public float DamageModifier { get; }
        public float HitChance { get; }
        public Dictionary<string, Action> Abilities { get; }

        public BodyPart(float damageModifier, float hitChance, params (string, Action)[] abilities)
        {
            foreach (var ability in abilities)
                Abilities.Add(ability.Item1, ability.Item2);
            DamageModifier = damageModifier;
            HitChance = hitChance;
        }
    }
}
