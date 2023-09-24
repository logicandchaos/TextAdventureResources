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
            return person;
        }

        public Person CreatePerson(Person father, Person mother)
        {
            Person person = new Person();
            return person;
        }

        public Place CreatePlace(Template template)
        {
            Place place = new Place();
            return place;
        }

        public Thing CreateThing(Template template)
        {
            Thing thing = new Thing();
            return thing;
        }
    }
}