using System;
using System.Collections.Generic;
using System.Text;

namespace TextAdventureLibrary
{
    public class World //: Place
    {
        public string Label { get; private set; }
        Matrix worldMap;
        public Die die;
        public DateTime dateTime;
        //List<> history;

        public World(string label, Point2D point)// : base(label, point)
        {
            //create die
            die = new Die(label.GetHashCode(), 10);
            //create world map
            GenerateMap();
            //generate dateTime
            //dateTime=
        }

        void GenerateMap()
        {

        }
    }
}
