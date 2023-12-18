using System;

namespace TextAdventureLibrary
{
    public class Condition
    {
        private Predicate<object> predicate;

        public Condition(Predicate<object> predicate)
        {
            this.predicate = predicate;
        }

        public bool IsTrue(object obj)
        {
            return predicate(obj);
        }
    }
}
