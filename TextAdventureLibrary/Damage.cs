using System;
using System.Collections.Generic;
using System.Text;

namespace TextAdventureLibrary
{
    public class Damage:Effect
    {
        public enum DamageType
        {
            Blunt,
            Slicing,
            Piercing,
            Choping
        }
        public DamageType damageType;

        public float damageAmount;

        //public StatusEffect statusEffect;
        //public float statisEffectChance;
    }
}
