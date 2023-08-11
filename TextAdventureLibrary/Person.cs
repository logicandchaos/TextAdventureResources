using System;
using System.Collections.Generic;
using System.Text;

namespace TextAdventureLibrary
{
    public class Person : Noun
    {
        Person Father { get; }
        Person Mother { get; }
        public Random random;
        public readonly Species species;
        public enum Gender { male, female }
        public Gender gender;
        public List<Stat> stats = new List<Stat>();
        public int alignment = 0;
        public int health;
        public int MaxHealth => (int)stats[(int)Stats.Vitality].value * 10;
        public Dictionary<string, StatusEffect> statusEffects = new Dictionary<string, StatusEffect>();
        public readonly Brain brain;
        public readonly DateTime birthdate;
        public readonly DateTime deathdate;
        public readonly Place birthPlace;
        //public int Age { get { return (int)((Program.world.GetDateTime() - birthdate).TotalDays / 365.2425); } }

        public Person(string label, Person father, Person mother) : base(label)
        {
        }

        public override void GenerateDescription()
        {

        }
    }
}
