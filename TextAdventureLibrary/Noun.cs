using System;
using System.Collections.Generic;
using System.Linq;

namespace TextAdventureLibrary
{
    public abstract class Noun
    {
        public Dictionary<string, object> Attributes { get; private set; }

        public void AddAttribute(string name, object attribute)
        {
            if (!Attributes.ContainsKey(name))
                Attributes.Add(name, attribute);
        }

        public void SetAttribute(string name, object attribute)
        {
            if (Attributes.ContainsKey(name))
                Attributes[name] = attribute;
            else
                Attributes.Add(name, attribute);
        }

        public void RemoveAttribute(string name)
        {
            if (Attributes.ContainsKey(name))
                Attributes.Remove(name);
        }

        public T GetAttributeValue<T>(string key)
        {
            if (!Attributes.ContainsKey(key))
                return default;

            object attribute;
            Attributes.TryGetValue(key, out attribute);

            if (attribute is T value)
            {
                return value;
            }

            return default;
        }

        public Dictionary<string, T> FilterAttributesByType<T>()
        {
            return Attributes
                .Where(pair => pair.Value != null && pair.Value.GetType() == typeof(T))
                .ToDictionary(pair => pair.Key, pair => (T)pair.Value);
        }

        public abstract void GenerateDescription();

    }
}