using System;
using System.Collections.Generic;
using System.Text;

namespace TextAdventureLibrary
{
    public class Person : Noun
    {
        string Name { get; }
        Person Father { get; }
        Person Mother { get; }
        public Die die;
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
        public readonly DateTime deathdate;//readonly??
        public readonly Place birthPlace;
        //public int Age { get { return (int)((Program.world.GetDateTime() - birthdate).TotalDays / 365.2425); } }

        //need factory to create people
        public Person(string label) : base(label)
        {
            //create name
            //Name=
            //create die
            die = new Die(Name.GetHashCode(), 10);
        }

        public override void GenerateDescription()
        {

        }
    }
}
