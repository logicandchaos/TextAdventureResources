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

        /*private static readonly Lazy<World> lazyInstance = new Lazy<World>(() => new World());
        public static World Instance => lazyInstance.Value;
        private World() { }*/

        public World() { }

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

        public void Save()
        {
            //serialize all the data
            //save to text file
        }

        public void Load(string worldName)
        {
            //read in the textfile for the worldName
            //parse string data into world
        }
    }
}
