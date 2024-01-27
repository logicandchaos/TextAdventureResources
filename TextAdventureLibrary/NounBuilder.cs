using System;
using System.Collections.Generic;
using System.Linq;

namespace TextAdventureLibrary
{
    public class NounBuilder<T> where T : Noun, new()
    {
        private T noun;
        private string[] requiredAttributes;

        public NounBuilder(params string[] requiredAttributes)
        {
            noun = new T();
            this.requiredAttributes = requiredAttributes;
        }

        public NounBuilder<T> WithAttribute(string key, object value)
        {
            noun.AddOrSetAttribute(key, value);
            return this;
        }

        public NounBuilder<T> WithAttributes(Dictionary<string, object> attributes)
        {
            foreach (var kvp in attributes)
            {
                noun.AddOrSetAttribute(kvp.Key, kvp.Value);
            }
            return this;
        }

        private bool CheckRequiredAttributes()
        {
            var presentAttributes = noun.Attributes.Keys;
            return requiredAttributes.All(attr => presentAttributes.Contains(attr));
        }

        public T TryBuild()
        {
            if (CheckRequiredAttributes())
                return Build();
            else
                return null;
        }

        private T Build()
        {
            return noun;
        }
    }
}