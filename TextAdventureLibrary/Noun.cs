using System.Collections.Generic;

namespace TextAdventureLibrary
{
    public abstract class Noun
    {
        public string Label { get; private set; }
        public string Description { get; private set; }

        Volume volume;//may make float to simplify
        float weight;

        public List<Thing> inventory;
        public float inventorySize;//bulk, size
        public float inventoryWeight;

        public Noun(string label)
        {
            Label = label;
            GenerateDescription();
        }

        public abstract void GenerateDescription();

        //maybr make some sort of item exchanger class..?
        //public abstract void AddToInventory();
        //public abstract void RemoveFromInventory();
    }
}
