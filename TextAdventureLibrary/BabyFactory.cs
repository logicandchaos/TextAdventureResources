using System;
using System.Collections.Generic;
using System.Text;

namespace TextAdventureLibrary
{
    public static class BabyFactory
    {
        public static Person Procreate(Person father, Person mother, Random randomGenerator)
        {
            Person person = new Person();

            //make sure same species
            if (father.Attributes["species"] != mother.Attributes["species"])
            {
                person = null;
                return null;
            }

            if (father.GetAttributeValue<string>("gender") != "male"
                && mother.GetAttributeValue<string>("gender") != "female")
            {
                person = null;
                return null;
            }

            // Inherit attributes from father and mother
            foreach (var kvp in father.Attributes)
            {
                if (kvp.Value is Thing)//skip things
                    continue;

                person.AddOrSetAttribute(kvp.Key, kvp.Value);
            }

            foreach (var kvp in mother.Attributes)
            {
                if (kvp.Value is Thing)//skip things
                    continue;

                if (person.Attributes.ContainsKey(kvp.Key))
                {
                    if (person.Attributes[kvp.Key] != kvp.Value)
                    {
                        if (randomGenerator.Next(2) == 0)
                            person.AddOrSetAttribute(kvp.Key, kvp.Value);
                    }
                }
                else
                    person.AddOrSetAttribute(kvp.Key, kvp.Value);
            }

            /*person.AddOrSetAttribute("birthDate", DateTime.Now);
            person.AddOrSetAttribute("birthPlace", mother.GetAttributeValue<Place>("location"));
            person.AddOrSetAttribute("location", mother.GetAttributeValue<Place>("location"));
            if (father.Attributes.ContainsKey("lastName"))
                person.AddOrSetAttribute("lastName", father.GetAttributeValue<string>("lastName"));*/

            //0-3 father, 4-7 mother, 8-9 mutation

            return person;
        }
    }
}