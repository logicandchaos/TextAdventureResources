using System;
using System.Collections.Generic;
using System.Text;

namespace TextAdventureLibrary
{
    public static class Templates
    {
        //Name Generators
        static NameGenerator humanMaleNameGenerator = new NameGenerator
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
        static NameGenerator humanFemaleNameGenerator = new NameGenerator
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
        static NameGenerator humanLastNameGenerator = new NameGenerator
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

        //Species
        public static Dictionary<string, object> human = new Dictionary<string, object>();
        public static Dictionary<string, object> slime = new Dictionary<string, object>();
        public static Dictionary<string, object> wolf = new Dictionary<string, object>();
        public static Dictionary<string, object> bandit = new Dictionary<string, object>();//could use human?
        public static Dictionary<string, object> briggand = new Dictionary<string, object>();
        public static Dictionary<string, object> ogre = new Dictionary<string, object>();
        public static Dictionary<string, object> goblin = new Dictionary<string, object>();
        public static Dictionary<string, object> hobgoblin = new Dictionary<string, object>();
        public static Dictionary<string, object> bugbear = new Dictionary<string, object>();
        public static Dictionary<string, object> troll = new Dictionary<string, object>();
        public static Dictionary<string, object> wyvern = new Dictionary<string, object>();
        public static Dictionary<string, object> manticore = new Dictionary<string, object>();
        public static Dictionary<string, object> skeleton = new Dictionary<string, object>();
        public static Dictionary<string, object> skeletonWarrior = new Dictionary<string, object>();
        public static Dictionary<string, object> mummy = new Dictionary<string, object>();
        public static Dictionary<string, object> jackal = new Dictionary<string, object>();

        public static PersonBuilder personBuilder = new PersonBuilder();

        static Templates()
        {
            //HUMAN
            human.Add("species", "human");
            human.Add("bodyType", "humanoid");
            //stats
            human.Add("strength", new Stat(3, 9));
            human.Add("vitality", new Stat(3, 9));
            human.Add("dexterity", new Stat(3, 9));
            human.Add("speed", new Stat(3, 9));
            human.Add("intelligence", new Stat(3, 9));
            human.Add("charisma", new Stat(3, 9));
            //utilities
            Utility health = new Utility(10, 10);
            human.Add("health", health);

            //SWORD
            Thing sword = new ThingBuilder().Build();

            //Predicate<Person> hasSwordEquiped = person => person.GetAttributeValue<Thing>("equipedItem") == sword;

            //PersonBuilder personBuilder = new PersonBuilder();
            //Person character = personBuilder
            //    .WithAttributes(human)
            //    .Build();
        }
    }
}