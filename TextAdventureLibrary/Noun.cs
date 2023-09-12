using System.Collections.Generic;

namespace TextAdventureLibrary
{
    public abstract class Noun
    {
        Dictionary<string, Attribute> attributes;
        //public Inventory inventory;

        //will be attributes
        //Volume volume;//may make float to simplify
        //float weight;

        public Noun(string name, params Attribute[] attributes)
        {
            AddAttribute(new Attribute("name", name));
            AddAttribute(new Attribute("description", ""));
            foreach (Attribute a in attributes)
            {
                AddAttribute(a);
            }
            GenerateDescription();
        }

        public void AddAttribute(Attribute attribute)
        {
            attributes.Add(attribute.Name, attribute);
        }

        public void RemoveAttribute(string name)
        {
            attributes.Remove(name);
        }

        public object GetAttributeValue(string name)
        {
            if (!attributes.ContainsKey(name))
                return null;
            Attribute attribute;
            attributes.TryGetValue(name, out attribute);
            return attribute.Value;
        }

        public abstract void GenerateDescription();

    }
}
