using System;
using System.Collections.Generic;
using System.Text;

namespace TextAdventureLibrary
{
    public class World
    {
        public string Label { get; private set; }
        Matrix worldMap;
        public Die die;
        public DateTime dateTime;
        public Dictionary<string, Noun> stuff;
        public List<string> history;

        public World(string label)
        {
            //create die
            die = new Die(label.GetHashCode(), 10);
            //generate dateTime
            //dateTime=
            //create world map
            GenerateMap();
        }

        void GenerateMap()
        {

        }
    }
}
