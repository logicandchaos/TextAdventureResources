using System.Collections.Generic;

namespace TextAdventureLibrary
{
    public abstract class Noun
    {
        public Dictionary<string, object> Attributes { get; private set; }

        public void AddAttribute(string name, object attribute)
        {
            Attributes.Add(name, attribute);
        }

        public void RemoveAttribute(string name)
        {
            Attributes.Remove(name);
        }

        public int GetAttributeValue(string key, int defaultValue = 0)
        {
            if (!Attributes.ContainsKey(key))
                return defaultValue;

            object attribute;
            Attributes.TryGetValue(key, out attribute);
            if (attribute is int value)
                return value;
            else
                return defaultValue;
        }

        public string GetAttributeValue(string key, string defaultValue = "")
        {
            if (!Attributes.ContainsKey(key))
                return defaultValue;

            object attribute;
            Attributes.TryGetValue(key, out attribute);
            if (attribute is string value)
                return value;
            else
                return defaultValue;
        }

        public float GetAttributeValue(string key, float defaultValue = 0)
        {
            if (!Attributes.ContainsKey(key))
                return defaultValue;

            object attribute;
            Attributes.TryGetValue(key, out attribute);
            if (attribute is float value)
                return value;
            else
                return defaultValue;
        }

        public Stat GetAttributeValue(string key, Stat defaultValue = null)
        {
            if (!Attributes.ContainsKey(key))
                return defaultValue;

            object attribute;
            Attributes.TryGetValue(key, out attribute);
            if (attribute is Stat value)
                return value;
            else
                return defaultValue;
        }

        public Utility GetAttributeValue(string key, Utility defaultValue = null)
        {
            if (!Attributes.ContainsKey(key))
                return defaultValue;

            object attribute;
            Attributes.TryGetValue(key, out attribute);
            if (attribute is Utility value)
                return value;
            else
                return defaultValue;
        }

        public abstract void GenerateDescription();

    }
}