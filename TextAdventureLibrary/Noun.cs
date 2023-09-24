using System.Collections.Generic;

namespace TextAdventureLibrary
{
    public abstract class Noun
    {
        Dictionary<string, object> attributes;        

        public void AddAttribute(string name, object attribute)
        {
            attributes.Add(name, attribute);
        }

        public void RemoveAttribute(string name)
        {
            attributes.Remove(name);
        }

        public int GetAttributeValue(string key, int defaultValue = 0)
        {
            if (!attributes.ContainsKey(key))
                return defaultValue;

            object attribute;
            attributes.TryGetValue(key, out attribute);
            if (attribute is int value)
                return value;
            else
                return defaultValue;
        }

        public string GetAttributeValue(string key, string defaultValue = "")
        {
            if (!attributes.ContainsKey(key))
                return defaultValue;

            object attribute;
            attributes.TryGetValue(key, out attribute);
            if (attribute is string value)
                return value;
            else
                return defaultValue;
        }

        public float GetAttributeValue(string key, float defaultValue = 0)
        {
            if (!attributes.ContainsKey(key))
                return defaultValue;

            object attribute;
            attributes.TryGetValue(key, out attribute);
            if (attribute is float value)
                return value;
            else
                return defaultValue;
        }

        public Stat GetAttributeValue(string key, Stat defaultValue = null)
        {
            if (!attributes.ContainsKey(key))
                return defaultValue;

            object attribute;
            attributes.TryGetValue(key, out attribute);
            if (attribute is Stat value)
                return value;
            else
                return defaultValue;
        }

        public Utility GetAttributeValue(string key, Utility defaultValue = null)
        {
            if (!attributes.ContainsKey(key))
                return defaultValue;

            object attribute;
            attributes.TryGetValue(key, out attribute);
            if (attribute is Utility value)
                return value;
            else
                return defaultValue;
        }

        public abstract void GenerateDescription();

    }
}