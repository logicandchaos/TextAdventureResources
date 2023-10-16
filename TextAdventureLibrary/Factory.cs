using System;
using System.Collections.Generic;
using System.Text;

namespace TextAdventureLibrary
{
    public static class Factory
    {
        public static Person CreatePerson(Dictionary<string, object> template)
        {
            Person person = new Person();

            //add in all attributes from Template
            foreach (KeyValuePair<string, object> attribute in template)
            {
                person.Attributes.Add(attribute.Key, attribute.Value);
            }

            return person;
        }

        public static Person CreatePerson(Person father, Person mother)
        {
            //check same species
            if (father.GetAttributeValue<string>("species").CompareTo(
                mother.GetAttributeValue<string>("species")) != 0)
            {
                return null;
            }

            Person person = new Person();
            /*Template species = father.GetAttributeValue<Template>("species");

            //add in all attributes from Template
            foreach (KeyValuePair<string, object> attribute in species.attributes)
            {
                person.Attributes.Add(attribute.Key, attribute.Value);
            }*/

            person.AddAttribute("father", father);
            person.AddAttribute("mother", mother);
            //add species
            //add and set stats based on parents
            //add gender
            //add name, first middle, last?
            //set dice based off name

            return person;
        }

        public static Place CreatePlace(Dictionary<string, object> template)
        {
            Place place = new Place();

            //add in all attributes from Template
            foreach (KeyValuePair<string, object> attribute in template)
            {
                place.Attributes.Add(attribute.Key, attribute.Value);
            }

            return place;
        }

        public static Thing CreateThing(Dictionary<string, object> template)
        {
            Thing thing = new Thing();

            //add in all attributes from Template
            foreach (KeyValuePair<string, object> attribute in template)
            {
                thing.Attributes.Add(attribute.Key, attribute.Value);
            }

            return thing;
        }
    }
}