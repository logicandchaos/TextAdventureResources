using System.Collections.Generic;

namespace TextAdventureLibrary
{
    public abstract class Noun
    {
        public string Label { get; private set; }
        public string Description { get; private set; }

        public Dictionary<string, Attribute> attributes;
        public Inventory inventory;

        //will be attributes
        //Volume volume;//may make float to simplify
        //float weight;

        public Noun(string label, params Attribute[] attributes)
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
