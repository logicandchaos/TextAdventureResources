using System;

namespace TextAdventureLibrary
{
    public class Die
    {
        public Random Random { get; private set; }

        public Die(int seed)
        {
            Random = new Random(seed);
        }

        public int Roll(int min, int max)
        {
            return Random.Next(min, max + 1);
        }
    }
}
