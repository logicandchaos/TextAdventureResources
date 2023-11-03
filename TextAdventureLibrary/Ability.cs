using System;
using System.Collections.Generic;
using System.Text;

namespace TextAdventureLibrary
{
    public abstract class Ability
    {
        private string governingStat;

        public Ability(string stat)
        {
            governingStat = stat;
        }

        public abstract void UseAbility(Person user, Person target);
        /*{
            int roll = user.die.Roll();
            if (user.GetAttributeValue<Stat>(governingStat).StatCheck(roll) > 0)
            {
                effect(user, target);
            }
        }*/
    }
}
