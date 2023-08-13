using System;
using System.Collections.Generic;
using System.Text;

namespace TextAdventureLibrary
{
    public class City : Place
    {
        List<Building> buildings;

        public City(string label, Point2D location) : base(label, location)
        {
        }
    }
}
