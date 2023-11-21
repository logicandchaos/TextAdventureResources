﻿using System;
using System.Collections.Generic;

namespace TextAdventureLibrary
{
    public class PersonBuilder
    {
        Person person;

        public PersonBuilder()
        {
            person = new Person();
        }

        public PersonBuilder WithAttribute(string key, object value)
        {
            person.AddOrSetAttribute(key, value);
            return this;
        }

        public PersonBuilder WithAttributes(Dictionary<string, object> attributes)
        {
            foreach (var kvp in attributes)
            {
                person.AddOrSetAttribute(kvp.Key, kvp.Value);
            }
            return this;
        }

        public Person Build()
        {
            //dice
            //roll stats
            return person;
        }

        public Person Procreate(Person father, Person mother, Random randomGenerator)
        {
            person = new Person();

            //make sure same species
            if (father.Attributes["species"] != mother.Attributes["species"])
            {
                person = null;
                return null;
            }

            // Inherit attributes from father and mother
            foreach (var kvp in father.Attributes)
            {
                if (kvp.Value is Thing)
                    continue;

                person.AddOrSetAttribute(kvp.Key, kvp.Value);
            }

            foreach (var kvp in mother.Attributes)
            {
                if (kvp.Value is Thing)
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