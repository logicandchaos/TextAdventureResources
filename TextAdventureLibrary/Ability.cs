using System;
using System.Collections.Generic;
using System.Text;

namespace TextAdventureLibrary
{
    public class Ability
    {
        Predicate<Person> condition;
        Action<Person, Person> action;

        public Ability(Predicate<Person> condition, Action<Person, Person> action)
        {
            this.condition = condition;
            this.action = action;
        }
    }
}
