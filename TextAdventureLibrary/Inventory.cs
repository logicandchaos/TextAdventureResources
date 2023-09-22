using System;
using System.Collections.Generic;
using System.Text;

namespace TextAdventureLibrary
{
    public class Inventory
    {
        //needs attributes volume and weight limit
        List<Attribute> attributes;
        List<Thing> things;
        public void AddToInventory(Thing thing) { }
        public void RemoveFromInventory(Thing thing) { }
        public void RemoveFromInventory(int index) { }
    }
}
