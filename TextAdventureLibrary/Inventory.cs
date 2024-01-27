using System;
using System.Collections.Generic;
using System.Text;

namespace TextAdventureLibrary
{
    public class Inventory
    {
        public float MaxWeight { get; private set; }
        public float Volume { get; private set; }
        public List<Thing> Things { get; private set; }

        public Inventory(float maxWeight, float volume)
        {
            MaxWeight = maxWeight;
            Volume = volume;
        }

        public void AddToInventory(Thing thing) { }

        public void RemoveFromInventory(Thing thing) { }

        public void RemoveFromInventory(int index) { }

        public Thing SelectItem(int index)
        {
            return null;
        }
    }
}
