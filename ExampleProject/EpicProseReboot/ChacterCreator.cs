using System;
using System.Collections.Generic;
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
            "gender",
            "die"
            );

        public static NameGenerator humanMaleNameGenerator = new NameGenerator
                (
                "Aelfric",
                "Eadmund",
                "Leofwine",
                "Wulfstan",
                "Godwin",
                "Aldred",
                "Cuthbert",
                "Hrothgar",
                "Ecgbert",
                "Aethelred",
                "Alfred",
                "Cenric",
                "Osgood",
                "Sigurd",
                "Guthrum",
                "Hengest",
                "Ceolwulf",
                "Osric",
                "Wilfrid",
                "Ethelbert"
                );

        public static NameGenerator humanFemaleNameGenerator = new NameGenerator
            (
                "Aeliana",
                "Aelis",
                "Aldora",
                "Aldwynn",
                "Aveline",
                "Berhtrude",
                "Brunhilda",
                "Edith",
                "Elizabeth",
                "Ethelwyn",
                "Gisela",
                "Grimilda",
                "Guinevere",
                "Hildeburg",
                "Hildred",
                "Hrothwyn",
                "Mildrith",
                "Thyra",
                "Wynfrith",
                "Zelda"
                );

        public static NameGenerator humanLastNameGenerator = new NameGenerator
            (
                "Glover",
                "Osricson",
                "Wynstan",
                "Wilfridson",
                "Eadmundson",
                "Cuthbert",
                "Fletcher",
                "Miller",
                "Taylor",
                "Carpenter",
                "Cooper",
                "Baker",
                "Fisher",
                "Archer",
                "Mason",
                "Shepherd",
                "Sexton",
                "Tanner",
                "Bowman",
                "Cartwright"
                );

        public static NameGenerator animalNameGenerator = new NameGenerator
            (
                "Glover",
                "Osricson",
                "Wynstan",
                "Wilfridson",
                "Eadmundson",
                "Cuthbert",
                "Fletcher",
                "Miller",
                "Taylor",
                "Carpenter",
                "Cooper",
                "Baker",
                "Fisher",
                "Archer",
                "Mason",
                "Shepherd",
                "Sexton",
                "Tanner",
                "Bowman",
                "Cartwright"
                );

        public static NameGenerator monsterNameGenerator = new NameGenerator
            (
                "Glover",
                "Osricson",
                "Wynstan",
                "Wilfridson",
                "Eadmundson",
                "Cuthbert",
                "Fletcher",
                "Miller",
                "Taylor",
                "Carpenter",
                "Cooper",
                "Baker",
                "Fisher",
                "Archer",
                "Mason",
                "Shepherd",
                "Sexton",
                "Tanner",
                "Bowman",
                "Cartwright"
                );

        public static void RollStats(Person person, bool display = false)
        {
            if (display)
                Program.console.Print($"{person.Name}'s stats\n");

            Die die = person.GetAttributeValue<Die>("die");

            Dictionary<string, Stat> stats = person.FilterAttributesByType<Stat>();

            foreach (var stat in stats)
            {
                stat.Value.RollStat(die);
                if (display)
                    Program.console.Print($"{stat.Key}: {stat.Value.Value}\n");
            }

            Utility health = person.GetAttributeValue<Utility>("health");
            health.SetMax(person.GetAttributeValue<Stat>("vitality").Value * 10);
            health.SetValue(health.Max);
            if (display)
                Program.console.Print($"\nHealth: {health.Value}/{health.Max}\n");
        }
    }
}
