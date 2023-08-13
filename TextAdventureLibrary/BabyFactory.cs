using System;
using System.Collections.Generic;
using System.Text;

namespace TextAdventureLibrary
{
    public static class BabyFactory
    {
        public static Person CreatePerson(Person father, Person mother)
        {
            if (father.species != mother.species)
            {
                //error not same species
                return null;
            }

            //create name
            string name = "";

            Person baby = new Person(name);
            return baby;
        }
    }
}
