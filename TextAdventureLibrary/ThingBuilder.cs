using System.Collections.Generic;

namespace TextAdventureLibrary
{
    public class ThingBuilder
    {
        private Thing thing;

        public ThingBuilder()
        {
            thing = new Thing();
            //default attributes
        }

        public ThingBuilder WithAttribute(string key, object value)
        {
            thing.AddOrSetAttribute(key, value);
            return this;
        }

        public ThingBuilder WithAttributes(Dictionary<string, object> attributes)
        {
            foreach (var kvp in attributes)
            {
                thing.AddOrSetAttribute(kvp.Key, kvp.Value);
            }
            return this;
        }

        public Thing Build()
        {
            return thing;
        }
    }
}
