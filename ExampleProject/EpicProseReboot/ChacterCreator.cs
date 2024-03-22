using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventureLibrary;

namespace EpicProseRedux
{
    public static class ChacterCreator
    {
        public static NounBuilder<Person> personBuilder = new NounBuilder<Person>(
            "strength",
            "vitality",
            "dexterity",
            "speed",
            "intelligence",
            "charisma",
            "bodyType",
            "health",
            "name",
            "gender",
            "hostility",//aggression
            "sociability",
            "diet"
            );

        public static void RollStats(Person person)
        {
            Die die = person.GetAttributeValue<Die>("die");
            Dictionary<string, Stat> stats = person.FilterAttributesByType<Stat>();
            foreach (var stat in stats)
            {
                stat.Value.RollStat(die);
                Program.console.Print($"{stat.Key}: {stat.Value}");
            }
        }
    }
}
