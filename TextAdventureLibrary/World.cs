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

        public World(string name, Matrix map)
        {
            //create die
            die = new Die(name.GetHashCode());
            //generate dateTime
            //DateTime=
        }

        public void AddTimeSpan(TimeSpan timeSpan)
        {
            DateTimeCurrent += timeSpan;
        }
    }
}
