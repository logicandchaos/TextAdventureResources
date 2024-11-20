using System.Collections.Generic;
using System.Linq;

namespace TextAdventureLibrary
{
    public abstract class Noun
    {
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public Vector2Double Location { get; private set; }
        public Dictionary<string, object> Attributes { get; private set; } = new Dictionary<string, object>();
        public Inventory Inventory { get; protected set; }

        public void SetName(string name)
        {
            Name = name;
        }

        public void InitializeInventory(float maxWeight, float space, int maxItems)
        {
            Inventory = new Inventory(maxWeight, space, maxItems);
        }

        public void SetDescription(string text)
        {
            Description = text;
        }

        public void AddToDescription(string text)
        {
            Description += text;
        }

        public void SetLocation(Vector2Double location)
        {
            Location = location;
        }

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