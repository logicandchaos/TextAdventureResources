using System;
using System.Collections.Generic;
using System.Text;

namespace TextAdventureLibrary
{
    public abstract class Brain
    {
        public abstract int Choose();
    }

    public class Player : Brain
    {
        public override int Choose()
        {
            return 0;
        }
    }

    public class NPC : Brain
    {
        public override int Choose()
        {
            return 0;
        }
    }
}
