using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextAdventureLibrary;

namespace EpicProseRedux
{
    public class Game
    {
        public enum GameStates { MAIN_MENU, OPTIONS, PLAY_MENU, NEW_GAME, LOAD_GAME, WORLD, ENCOUNTER, QUIT };
        public GameStates gameState = GameStates.MAIN_MENU;

        MenuSystem menuSystem;

        Person player;

        Maps maps;

        NameGenerator monsterNameGenerator;
        NameGenerator humanMaleNameGenerator;
        NameGenerator humanFemaleNameGenerator;
        NameGenerator humanLastNameGenerator;
        NameGenerator animalNameGenerator;

        bool DEBUG = true;

        public Game()
        {
            Program.console.Print("New Game Created", true);

            Program.console.SetDebugMode(DEBUG);
            Program.console.DebugMessage("Debug mode is active");
            if (DEBUG)
                Program.console.Anykey();
        }

        public void Play()
        {
            while (gameState != GameStates.QUIT)
            {
                switch (gameState)
                {
                    case GameStates.MAIN_MENU:
                        Program.console.Print(menuSystem.mainMenu);
                        break;
                    case GameStates.PLAY_MENU:
                        Program.console.Print(menuSystem.playMenu);
                        break;
                    case GameStates.NEW_GAME:
                        Program.console.Print("Create Your Character", true);

                        //name
                        string name = Program.console.GetString("Enter Your Name: ");

                        //choose gender
                        string gender = "";
                        Menu selectGender = new Menu
                            (
                            "Choose your gender:",
                            new MenuItem("male", () => gender = "male"),
                            new MenuItem("female", () => gender = "female")
                            );
                        Program.console.Print(selectGender);
                        selectGender.SelectOption(Program.console.GetDigit(selectGender.Items.Length));

                        /*player = personBuilder
                            .WithAttributes(human)
                            .WithAttribute("name", name)
                            .WithAttribute("gender", gender)
                            .TryBuild();*/

                        if (player == null)
                        {
                            Program.console.Print("Error character could not be created!", true);
                            break;
                        }

                        Program.console.Print("Roll Stats", true);
                        //roll stats
                        Dictionary<string, Stat> stats = player.FilterAttributesByType<Stat>();
                        do
                        {
                            foreach (var stat in stats)
                            {
                                stat.Value.RollStat(player.GetAttributeValue<Die>("die"));
                                Program.console.Print($"{stat.Key}: {stat.Value}", true);
                            }
                        }
                        while (Program.console.YesNo("Reroll?"));

                        //Story();
                        gameState = GameStates.WORLD;
                        break;
                    case GameStates.LOAD_GAME:
                        gameState = GameStates.WORLD;
                        break;
                    case GameStates.WORLD:
                        Program.console.Print(menuSystem.worldMenu);
                        break;
                    case GameStates.ENCOUNTER:
                        List<Person> people = new List<Person>();

                        //initiative
                        int[] initiatives = new int[people.Count];
                        //roll for each Person
                        for (int i = 0; i < people.Count; i++)
                        {
                            initiatives[i] = people[i].GetAttributeValue<Stat>("speed").StatCheck(people[i].GetAttributeValue<Die>("die"));
                        }
                        // Use LINQ to order people array by the corresponding number in initiatives
                        Person[] orderedPeople = people
                            .Zip(initiatives, (person, initiative) => new { Person = person, Initiative = initiative })
                            .OrderBy(item => item.Initiative)
                            .Select(item => item.Person)
                            .ToArray();

                        int turn = 0;

                        List<Menu> encounterMenus = new List<Menu>();
                        /*MenuItem attack=new MenuItem("Attack",);
                        MenuItem useItem=new MenuItem("Use Item",);
                        MenuItem talk=new MenuItem("Talk",);
                        MenuItem leave=new MenuItem("Leave",);*/
                        Menu currentMenu = encounterMenus[0];

                        //turn based loop
                        while (true)
                        {
                            //build encounter menu
                            currentMenu = new Menu("What will you do?");
                            Program.console.Print(currentMenu.Title);
                            Program.console.Print(currentMenu);
                            //orderedPeople[turn].GetAttributeValue<Brain>().MakeChoice(encounterMenu);

                            turn = turn < orderedPeople.Length ? turn++ : 0;
                            break;
                        }
                        gameState = GameStates.WORLD;
                        break;
                    default:
                        break;
                }
            }
        }

        public void Setup()
        {
            SetupMenues();
            SetupNameGenerators();
            SetupSpeciesTemplates();
            SetupItemTemplates();
            SetupPlaceTemplates();
            maps.SetupMap();
        }

        public void SetupMenues()
        {
            menuSystem = new MenuSystem(this);
        }

        public void RollStats(Person person)
        {
            Type type = typeof(Stat);

            // Use LINQ to filter objects of the specified type
            List<Stat> stats = person.Attributes
                .Where(kv => kv.Value != null && kv.Value.GetType() == type)
                .Select(kv => (Stat)kv.Value)
                .ToList();

            //roll stats use ranges based on species
            foreach (Stat stat in stats)
            {

            }
        }

        public void SetupNameGenerators()
        {
            humanMaleNameGenerator = new NameGenerator
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

            humanFemaleNameGenerator = new NameGenerator
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

            humanLastNameGenerator = new NameGenerator
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

            animalNameGenerator = new NameGenerator
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

            monsterNameGenerator = new NameGenerator
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
        }

        public void SetupSpeciesTemplates()
        {
            //HUMAN
            SpeciesTemplates.human.Add("species", "human");
            SpeciesTemplates.human.Add("bodyType", "humanoid");
            //stats
            SpeciesTemplates.human.Add("strength", new Stat(3, 9));
            SpeciesTemplates.human.Add("vitality", new Stat(3, 9));
            SpeciesTemplates.human.Add("dexterity", new Stat(3, 9));
            SpeciesTemplates.human.Add("speed", new Stat(3, 9));
            SpeciesTemplates.human.Add("intelligence", new Stat(3, 9));
            SpeciesTemplates.human.Add("charisma", new Stat(3, 9));
            //utilities
            Utility health = new Utility(10, 10, .5f);//maybe use a random number for weight within a certain range based on race??
            SpeciesTemplates.human.Add("health", health);
        }

        public static void SetupNPCs()
        {
            //King of thieves
            //the Ice Giant
            //mummyLord
            //dragon
            //John the Blacksmith
            //Bob the Witchdoctor
            //Frank the Dwarf
            //David the Warrior
            //Steve the Knight
            //Carol the Merchant
            //Joline the Sorcerous
            //Jack the Tavernkeeper
        }

        public static void SetupItemTemplates()
        {
            //SWORD
            /*Thing sword = thingBuilder
                .TryBuild();*/
        }

        public static void SetupPlaceTemplates()
        {
        }        

        public void Story()
        {
            Program.console.ClearScreen();
            Program.console.Print("STORY");
            Program.console.Print("\n\n<you can skip ahead at anytime by pressing any key>");
            Program.console.Type("\n\tYou live in a small peaceful village, in the land of Epica. One day", true, Program.textSpeed);
            Program.console.Type("\nyou are out exploring. After hiking for sometime, you stop to rest your feet.", true, Program.textSpeed);
            Program.console.Type("\nYou awake it is dark, black clouds are in the sky. You hurry back home to", true, Program.textSpeed);
            Program.console.Type("\nshelter. As you get closer you see light coming from ahead. You realize it's", true, Program.textSpeed);
            Program.console.Type("\nnot storm clouds, it's smoke! Your entire village is in flames!", true, Program.textSpeed);
            Program.console.Type("\nBy the time you reach your village all that is left is burning rubble and ash.", true, Program.textSpeed);
            Program.console.Type("\nNo survivors, nothing. The only thing that could have caused such destruction", true, Program.textSpeed);
            Program.console.Type("\nis a dragon! Watching the burning embers of what was once your home, you vow", true, Program.textSpeed);
            Program.console.Type("\nto avenge them, you will slay the dragon!", true, Program.textSpeed);
            Program.console.Type("\n...", true, Program.textSpeed * 5);
            Program.console.Anykey();

            Program.console.ClearScreen();
            Program.console.Type("\n\n\"What a tragity!\" a small voice says, you turn around to see a tiny man walk out from behind a tree.", true, Program.textSpeed);
            Program.console.Type("\n\"I'm Harold nice to meet you\" he says extending his tiny arm, you bend over and give him a finger to shake.", true, Program.textSpeed);
            Program.console.Type("\n\"Are you new to the land of Epica?\" (Y/N)\n", true, Program.textSpeed);
            if (Program.console.YesNo())
            {
                Program.console.Type("\n\"Welcome to the land of Epica. You can go to The Forest, The Town, The Caves,", true, Program.textSpeed);
                Program.console.Type("\nThe Ice Mountain and The Dragon's Lair. The weaker monsters are near the forest", true, Program.textSpeed);
                Program.console.Type("\nand town. If you need some help just press H. You should look around the area", true, Program.textSpeed);
                Program.console.Type("\nbefore you go there's got to be a weapon laying around here somewhere.", true, Program.textSpeed);
                Program.console.Type("\nMake sure to equip it before battle in the character menu.\n", true, Program.textSpeed);
                //noob = true;
            }
            else
            {
                //noob = false;
            }
            Program.console.Anykey();
        }

        void Help()
        {
            Program.console.Print("\nHELP");
            Program.console.Print("\n~~~~");
            Program.console.Print("\n\nEpic Prose SE now uses a menu system for most input, but in the");
            Program.console.Print("\ncases where you need to type in an input a list is provided.");
            Program.console.Print("\nUsing the up and down arrows you can scroll through previous input.");
            Program.console.Anykey();
            Program.console.Print("\n\nSTATS");
            Program.console.Print("\n~~~~~");
            Program.console.Print("\n\nStr - Strength - how much damage you do when you attack.");
            Program.console.Print("\nVit - Vitality - how much max health you have.");
            Program.console.Print("\nDex - Dexterity - your accuracy with fighting.");
            Program.console.Print("\nSpe - Speed - running away, dodging and travel speed.");
            Program.console.Print("\nInt - Intelligence - used for talking your way out of fights.");
            Program.console.Print("\nCha - Charisma - How liked you are, effects prices and things.");
            Program.console.Anykey();
            Program.console.Print("\n\nBASE MENU");
            Program.console.Print("\n~~~~~~~~~");
            Program.console.Print("\nGO - shows you a list of locations with the distance away from you, you will be asked");
            Program.console.Print("\nwhere you want to go, then type one of the options and you will travel in that direction");
            Program.console.Print("\nuntil you reach it, encounter a monster or press a key. After a battle you may not be at");
            Program.console.Print("\nyour destination, just repeat the process.");
            Program.console.Print("\nLOOK - gives you a description of your surroundings and any people or items around");
            Program.console.Anykey();
            Program.console.Print("\n\nCONVERSATIONS");
            Program.console.Print("\n~~~~~~~~~~~~~");
            Program.console.Print("\nPRICES -> see a list and prices of what's for sale");
            Program.console.Print("\nBUY <item name> -> buy an item");
            Program.console.Print("\nSELL <item name> -> sell an item");
            Program.console.Print("\nLEAVE -> leave the conversation.");
            Program.console.Print("\nTRAIN -> they will tell you what they can train you in an for how much.");
            Program.console.Print("\nTRAIN <stat> -> they will train the named stat for a price.");
            Program.console.Anykey();
            Program.console.Print("\n\nMONSTERS");
            Program.console.Print("\n~~~~~~~~");
            Program.console.Print("\nHere is a list of terrain types and the types of monsters found there:");
            Program.console.Print("\ngrassland easy -slime, goblin, wolf, bandit");
            Program.console.Print("\ngrassland hard - briggand, hobgoblin, bugbear, ogre");
            Program.console.Print("\nforest easy - slime, goblin, wolf, bandit");
            Program.console.Print("\nforest hard - briggand, hobgoblin, bugbear, troll");
            Program.console.Print("\nswamp easy - slime, goblin, wyvern, hobgoblin");
            Program.console.Print("\nswamp hard - troll, wyvern, hobgoblin, bugbear");
            Program.console.Print("\nrocky easy - bandit, skeleton, briggand, goblin");
            Program.console.Print("\nrocky hard - ogre, manticore, troll, skeleton warrior");
            Program.console.Print("\ndesert easy -skeleton, jackal, bandit, briggand");
            Program.console.Print("\ndesert hard - mummy, skeleton warrior, manticore, briggand");
            Program.console.Anykey();
        }

        void GameOver()
        {
            Program.console.SetColor(Color.Red, Color.Black);
            Program.console.Print("                /\\\\\\\\\\\\\\\\\\\\\\\\                                                                                          ");
            Program.console.Print("\n               /\\\\\\//////////                                                                                          ");
            Program.console.Print("\n               /\\\\\\                                                                                                    ");
            Program.console.Print("\n               \\/\\\\\\    /\\\\\\\\\\\\\\  /\\\\\\\\\\\\\\\\\\       /\\\\\\\\\\  /\\\\\\\\\\       /\\\\\\\\\\\\\\\\                                      ");
            Program.console.Print("\n                \\/\\\\\\   \\/////\\\\\\ \\////////\\\\\\    /\\\\\\///\\\\\\\\\\///\\\\\\   /\\\\\\/////\\\\\\                                    ");
            Program.console.Print("\n                 \\/\\\\\\       \\/\\\\\\   /\\\\\\\\\\\\\\\\\\\\  \\/\\\\\\ \\//\\\\\\  \\/\\\\\\  /\\\\\\\\\\\\\\\\\\\\\\                                    ");
            Program.console.Print("\n                  \\/\\\\\\       \\/\\\\\\  /\\\\\\/////\\\\\\  \\/\\\\\\  \\/\\\\\\  \\/\\\\\\ \\//\\\\///////                                    ");
            Program.console.Print("\n                   \\//\\\\\\\\\\\\\\\\\\\\\\\\/  \\//\\\\\\\\\\\\\\\\/\\\\ \\/\\\\\\  \\/\\\\\\  \\/\\\\\\  \\//\\\\\\\\\\\\\\\\\\\\                                 ");
            Program.console.Print("\n                     \\////////////     \\////////\\//  \\///   \\///   \\///    \\//////////                                 ");
            Program.console.Print("\n                                     /\\\\\\\\\\                                                                            ");
            Program.console.Print("\n                                    /\\\\\\///\\\\\\                                                                         ");
            Program.console.Print("\n                                   /\\\\\\/  \\///\\\\\\                                                                      ");
            Program.console.Print("\n                                   /\\\\\\      \\//\\\\\\  /\\\\\\    /\\\\\\     /\\\\\\\\\\\\\\\\   /\\\\/\\\\\\\\\\\\\\                          ");
            Program.console.Print("\n                                   \\/\\\\\\       \\/\\\\\\ \\//\\\\\\  /\\\\\\    /\\\\\\/////\\\\\\ \\/\\\\\\/////\\\\\\                        ");
            Program.console.Print("\n                                    \\//\\\\\\      /\\\\\\   \\//\\\\\\/\\\\\\    /\\\\\\\\\\\\\\\\\\\\\\  \\/\\\\\\   \\///                        ");
            Program.console.Print("\n                                      \\///\\\\\\  /\\\\\\      \\//\\\\\\\\\\    \\//\\\\///////   \\/\\\\\\                              ");
            Program.console.Print("\n                                         \\///\\\\\\\\\\/        \\//\\\\\\      \\//\\\\\\\\\\\\\\\\\\\\ \\/\\\\\\                             ");
            Program.console.Print("\n                                            \\/////           \\///        \\//////////  \\///                             ");
            Program.console.ResetColor();
            //if (!DEBUG)
            //{
            /*if (!player.IsAlive())
            {
                Console.WriteLine("\nYou were killed! Your village was not avenged!");
                JingleBad();
            }
            else if (!dragon.IsAlive())
            {
                Console.WriteLine("\nYou killed The Dragon! Your village has been avenged!! You beat Epic Prose SE!!!");
                JingleGood();
            }
            Console.WriteLine("\nYou had " + player.GetGold() + " gold and killed " + player.GetKills() + " monsters.");*/
            Program.console.Anykey();
        }
    }
}