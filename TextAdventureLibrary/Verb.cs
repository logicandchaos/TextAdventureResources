using System;

namespace TextAdventureLibrary
{
    public class Verb
    {
        Action action;

        public Verb(Action action)
        {
            this.action = action;
        }

        public void Execute()
        {
            action();
        }
    }
}
