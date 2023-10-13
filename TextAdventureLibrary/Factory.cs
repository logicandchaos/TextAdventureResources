using System;
using System.Collections.Generic;
using System.Text;

namespace TextAdventureLibrary
{
    public class Factory
    {
        public Person CreatePerson(Template species)
        {
            Person person = new Person();

            //add in all attributes from Template
            foreach (KeyValuePair<string, object> attribute in species.attributes)
            {
                person.Attributes.Add(attribute.Key, attribute.Value);
            }

            return person;
        }

        public Person CreatePerson(Person father, Person mother)
        {
            //check same species
            if (father.GetAttributeValue<Template>("species")
                != mother.GetAttributeValue<Template>("species"))
            {
                return null;
            }

            Person person = new Person();
            Template species = father.GetAttributeValue<Template>("species");

            //add in all attributes from Template
            foreach (KeyValuePair<string, object> attribute in species.attributes)
            {
                person.Attributes.Add(attribute.Key, attribute.Value);
            }

            person.AddAttribute("father", father);
            person.AddAttribute("mother", mother);
            //add species
            //add and set stats based on parents
            //add gender
            //add name, first middle, last?
            //set dice based off name

            return person;
        }

        public Place CreatePlace(Template template)
        {
            Place place = new Place();

            //add in all attributes from Template
            foreach (KeyValuePair<string, object> attribute in template.attributes)
            {
                place.Attributes.Add(attribute.Key, attribute.Value);
            }

            return place;
        }

        public Thing CreateThing(Template template)
        {
            Thing thing = new Thing();

            //add in all attributes from Template
            foreach (KeyValuePair<string, object> attribute in template.attributes)
            {
                thing.Attributes.Add(attribute.Key, attribute.Value);
            }

            return thing;
        }
    }
}