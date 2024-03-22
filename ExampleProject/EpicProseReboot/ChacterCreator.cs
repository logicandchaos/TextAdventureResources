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
            "name",
            "gender",
            "hostility",//aggression
            "sociability",
            "diet"
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

        public static void RollStats(Person person)
        {
            Die die = person.GetAttributeValue<Die>("die");

            Dictionary<string, Stat> stats = person.FilterAttributesByType<Stat>();
            // Use LINQ to filter objects of the specified type
            /*List<Stat> stats = person.Attributes
                .Where(kv => kv.Value != null && kv.Value.GetType() == type)
                .Select(kv => (Stat)kv.Value)
                .ToList();*/

            foreach (var stat in stats)
            {
                stat.Value.RollStat(die);
                Program.console.Print($"{stat.Key}: {stat.Value}");
            }
        }
    }
}
