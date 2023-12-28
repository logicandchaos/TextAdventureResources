using System;
using System.Collections.Generic;
using System.Linq;

namespace TextAdventureLibrary
{
    public abstract class Noun
    {
        public Dictionary<string, object> Attributes { get; private set; } = new Dictionary<string, object>();

        public void AddOrSetAttribute(string name, object value)
        {
            Attributes[name] = value;
        }

        public void RemoveAttribute(string name)
        {
            if (Attributes.ContainsKey(name))
                Attributes.Remove(name);
        }

        public T GetAttributeValue<T>(string key)
        {
            if (Attributes.TryGetValue(key, out var attribute) && attribute is T value)
                return value;

            return default;
        }

        public Dictionary<string, T> FilterAttributesByType<T>()
        {
            return Attributes
                .Where(pair => pair.Value is T)
                .ToDictionary(pair => pair.Key, pair => (T)pair.Value);
        }
    }
}