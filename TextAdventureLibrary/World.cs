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
        public DateTime dateTime;
        //public Dictionary<string, Noun> stuff;
        public Dictionary<string, Person> everyone;
        public Dictionary<string, Place> everywhere;
        public Dictionary<string, Thing> everything;
        public List<string> history;

        public World(string name, Matrix map)
        {
            //create die
            die = new Die(name.GetHashCode(), 10);
            //generate dateTime
            //dateTime=
        }
    }
}
