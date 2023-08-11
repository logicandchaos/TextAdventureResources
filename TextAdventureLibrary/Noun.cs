using System.Collections.Generic;

namespace TextAdventureLibrary
{
    public abstract class Noun
    {
        public string Label { get; private set; }
        public string Description { get; private set; }

        public List<Thing> inventory;

        public Noun(string label)
        {
            Label = label;
            GenerateDescription();
        }

        public abstract void GenerateDescription();
    }
}
