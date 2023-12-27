using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TextAdventureLibrary
{

    public class Person : Noun
    {
        public Die Die { get; private set; }
        public Person() { }

        public override void GenerateDescription()
        {
            //based on species, stats and other attributes
        }
    }
}
