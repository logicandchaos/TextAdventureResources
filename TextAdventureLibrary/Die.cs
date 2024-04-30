using System;

namespace TextAdventureLibrary
{
    public class Die
    {
        private Random random;

        public Die(int seed)
        {
            random = new Random(seed);
        }

        public Die(Random random)
        {
            this.random = random;
        }

        public int Roll(int min, int max)
        {
            return random.Next(min, max + 1);
        }
    }
}
