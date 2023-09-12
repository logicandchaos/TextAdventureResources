using System;
using System.Collections.Generic;
using System.Text;

namespace TextAdventureLibrary
{
    public class Person : Noun
    {
        public Die die;
        public Person Father { get; }
        public Person Mother { get; }
        public Species species { get; }
        public enum Gender { male, female }
        public Gender gender { get; }
        public Brain brain { get; }
        public DateTime birthdate { get; }
        public DateTime deathdate { get; private set; }
        public Place birthPlace { get; }
        //public int Age { get { return (int)((Program.world.GetDateTime() - birthdate).TotalDays / 365.2425); } }
        /*public List<Stat> stats = new List<Stat>();
        public int alignment = 0;
        public int health;
        public int MaxHealth => (int)stats[(int)Stats.Vitality].value * 10;
        public Dictionary<string, StatusEffect> statusEffects = new Dictionary<string, StatusEffect>();*/

        //need factory to create people
        public Person(string name, Person father, Person mother) : base(name)
        {
            //create name
            //Name=
            //create die
            die = new Die(name.GetHashCode(), 10);
            Father = father;
            Mother = mother;
            //birthPlace = Mother.location;
            //birthdate = World.date.Now;
            species = Mother.species;
        }

        public override void GenerateDescription()
        {

        }
    }
}
