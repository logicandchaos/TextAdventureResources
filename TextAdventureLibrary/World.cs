using System;
using System.Collections.Generic;
using System.Text;

namespace TextAdventureLibrary
{
    public class World
    {
        public string Name { get; private set; }
        public Matrix Map { get; set; }
        public Die die;
        public DateTime DateTimeCurrent { get; private set; }
        public Dictionary<string, Person> everyone;
        public Dictionary<string, Place> everywhere;
        public Dictionary<string, Thing> everything;
        public Dictionary<DateTime, string> history;

        public World()
        {
        }

        public World(string name)
        {
            //create die
            die = new Die(name.GetHashCode());
        }

        public World(int seed)
        {
            //create die
            die = new Die(seed);
        }

        public void AddTimeSpan(TimeSpan timeSpan)
        {
            DateTimeCurrent += timeSpan;
        }
    }
}
