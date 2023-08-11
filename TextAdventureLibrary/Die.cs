using System;

namespace TextAdventureLibrary
{
    public class Die
    {
        public Random Random { get; private set; }
        public int Sides { get; private set; }

        public Die(int seed, int sides)
        {
            Random = new Random(seed);
            Sides = sides;
        }

        public int Roll()
        {
            return Random.Next(Sides) + 1;
        }
    }
}
