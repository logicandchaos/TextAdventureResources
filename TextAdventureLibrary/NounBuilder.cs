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

        public NounBuilder<T> New()
        {
            noun = new T();
            return this;
        }

        public NounBuilder<T> WithName(string name)
        {
            noun.SetName(name);
            return this;
        }

        public NounBuilder<T> WithDescription(string description)
        {
            noun.SetDescription(description);
            return this;
        }

        public NounBuilder<T> WithLocation(Vector2Int location)
        {
            noun.SetLocation(location);
            return this;
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

        public string GetMissingAttributes()
        {
            string missingAttributes = "";
            foreach (string key in noun.Attributes.Keys)
            {
                if (!requiredAttributes.Contains(key))
                {
                    missingAttributes += key + "\n";
                }
            }
            return missingAttributes;
        }

        public NounBuilder<T> GetCurrentAttributes(out string attributes)
        {
            attributes = "";
            foreach (string key in noun.Attributes.Keys)
            {
                attributes += key + "\n";
            }
            return this;
        }

        private bool CheckRequiredAttributes()
        {
            var presentAttributes = noun.Attributes.Keys;
            return requiredAttributes.All(attr => presentAttributes.Contains(attr));
        }

        public T TryBuild(out string message)
        {
            if (CheckRequiredAttributes())
            {
                message = "Build Successful";
                return Build();
            }
            else
            {
                message = "Missing attributes:\n" + GetMissingAttributes();
                return null;
            }
        }

        private T Build()
        {
            return noun;
        }
    }
}