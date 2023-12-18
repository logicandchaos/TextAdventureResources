using System;
using System.Collections.Generic;
using System.Text;

namespace TextAdventureLibrary
{
    public abstract class Ability
    {
        private string governingStat;
        //List<Predicate<object>> requirements;
        List<Condition> conditions;
        List<Effect> effects;

        public Ability(string stat)
        {
            governingStat = stat;
        }

        public bool CanUse()
        {
            return true;
        }

        public void UseAbility(Person user, Person target)
        {
            if (!CanUse())//?
                return;

            int roll = user.die.Roll();
            if (user.GetAttributeValue<Stat>(governingStat).StatCheck(roll) > 0)
            {
                foreach (Effect e in effects)
                {
                    //e(user, target);
                }
            }
        }
    }
}
