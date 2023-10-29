using System.Collections.Generic;

namespace TextAdventureLibrary
{
    public class PlaceBuilder
    {
        private Place place;

        public PlaceBuilder()
        {
            place = new Place();
        }

        public PlaceBuilder WithAttribute(string key, object value)
        {
            place.AddOrSetAttribute(key, value);
            return this;
        }

        public PlaceBuilder WithAttributes(Dictionary<string, object> attributes)
        {
            foreach (var kvp in attributes)
            {
                place.AddOrSetAttribute(kvp.Key, kvp.Value);
            }
            return this;
        }

        public Place Build()
        {
            return place;
        }
    }
}
