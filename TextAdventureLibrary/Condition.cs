using System;

namespace TextAdventureLibrary
{
    public class Condition
    {
        public Predicate<Noun> Predicate { get; set; }
        bool isTrue;

        public bool Evaluate(Noun noun, World world)
        {
            if (!isTrue)
                return !Predicate(noun);
            return Predicate(noun);
        }
    }
}
