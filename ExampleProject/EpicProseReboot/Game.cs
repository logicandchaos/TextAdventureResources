using System;
using System.Collections.Generic;
using System.Linq;
using TextAdventureLibrary;

namespace EpicProseRedux
{
    public class Game
    {
        public enum GameStates { SETUP, NEWGAME, WORLDMAP, PLACE, ENCOUNTER, DUNGEON, QUIT };
        public GameStates gameState;
        public GameStates prevGameState;

        Person player;

        World world;

        const bool DEBUG = true;

        public Game()
        {
            Program.console.Print("Creating New Game\n");
            ChangeState(GameStates.SETUP);
        }

        public void Setup()
        {
            Program.console.Print("Configuring Game\n");
            SetupWorld();
            SetupPlaces();
            SetupNPCs();
            SetupItemTemplates();
            Program.console.Print("Game Configured\n");
        }

        public void SetupWorld()
        {
            world = new World("Epica",
                "0: Ice Mountain, 1: Cave, 2: Lair, 3: Witch Doctor, 4: Burned Village,\n5: Plainsville 6: Rogue's Den, 7: Townopolus, 8: Sandland, 9: Pyramid",
                WorldMapCells.worldMapKey
                );

            world.Map.CreateMatrixFromString(
                @"~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~",
                @"~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~",
                @"~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~",
                @"~~~~~~~~~~~~~~~~~_________~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~",
                @"~~~~~~~~~~~~~~~~/         \~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~",
                @"~~~~~~~~~~~~~~~~|          \~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~",
                @"~~~~~~~~~~~~~__/   /\/\     \_____~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~",
                @"~~~~~~~~~~~~|     /mm\m\    /\/\  \____~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~",
                @"~~~~~~~~~~~~|              /mm\m\      \____~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~",
                @"~~~~~~~~~~~/   /\ /\                /\/\    \~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~",
                @"~~~~~~~~~~~|  /mm\mm\   0          /mm\m\    |__~~~~~~~~~_____~~~~~~~~~~~~~~~~~~",
                @"~~~~~~~~~~~|                                    \_______/     \_~~~~~~~~~~~~~~~~",
                @"~~~~~~~~~_/           /\/\              /\/\     ▒▒▒▒▒▒▒ /\ /\  \~~~~~~~~~~~~~~~",
                @"~~~~~~~~/     ♣  ♣   /mm\m\       1    /mm\m\  ▒▒▒▒▒▒▒▒ /  \  \  |~~~~~~~~~~~~~~",
                @"~~~~~~~_|   ♣  ♣ ♣ ♣                          ▒▒▒▒▒▒▒▒▒▒   2    /~~~~~~~~~~~~~~~",
                @"~~~~~~/    ♣ ♣  ♣   ♣      /\ /\       /\/\  ▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒ |_~~~~~~~~~~~~~~~",
                @"~~~~~/    ♣  ♣  ♣ ♣  ♣    /mm\mm\     /mm\m\ ▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒\~~~~~~~~~~~~~~",
                @"~~~~~|   ♣  ♣  ♣   ♣   ♣                     ▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒|~~~~~~~~~~~~~",
                @"~~~_/   ♣  ♣  ♣   ♣  ♣ ♣ ♣                    ▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒\~~~~~~~~~~~~~",
                @"~~|      ♣ ♣▒▒▒▒▒   ♣  ♣  ♣                    ▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒|_~~~~~~~~~~~",
                @"~~|   ♣ ♣  ▒▒▒▒▒▒▒ ♣  ♣ ♣  ♣                     ▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒|~~~~~~~~~~",
                @"~~|   ♣  ♣▒▒▒▒▒▒▒3▒ ♣ ♣  ♣ ♣                       ▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒\~~~~~~~~~~",
                @"~~~\    ♣  ▒▒▒▒▒▒▒  ♣  ♣   ♣                   4      ▒▒▒▒▒▒▒▒▒▒▒▒    |~~~~~~~~~",
                @"~~~~|    ♣   ▒▒▒  ♣   ♣   ♣                             ♣   ♣   ♣ ♣ ♣ |~~~~~~~~~",
                @"~~~~\_   ♣  ♣ ♣  ♣   ♣   ♣                            ♣   ♣    ♣   ♣ ♣\~~~~~~~~~",
                @"~~~~~~\    ♣ ♣  ♣  ♣   ♣ ♣                          ♣   ♣  ♣  ♣  ♣  ♣ _|~~~~~~~~",
                @"~~~~~~~\_   ♣  ♣  ♣  ♣  ♣                          ♣  ♣   ♣ ♣  ♣  ♣  ♣\~~~~~~~~~",
                @"~~~~~~~~~\    ♣ ♣  ♣  ♣                             ♣   ♣  ♣  ♣  ♣    ♣|~~~~~~~~",
                @"~~~~~~~~~~\__    ♣  ♣                             ♣   ♣   ♣  ♣   ♣  ♣  |~~~~~~~~",
                @"~~~~~~~~~~~~~~|                   5                ♣   ♣   ♣  ♣  ♣  ♣  |~~~~~~~~",
                @"~~~~~~~~~~~~~~\                                   ♣   ♣  ♣   ♣  ♣ 6   ♣|~~~~~~~~",
                @"~~~~~~~~~~~~~~~\                                    ♣   ♣  ♣   ♣   ♣ ♣ |~~~~~~~~",
                @"~~~~~~~~~~~~~~~|                                     ♣   ♣   ♣   ♣  ♣__|~~~~~~~~",
                @"~~~~~~~~~~~~~~~~\                                      ♣  ♣  ♣  ♣ ♣ |_~~~~~~~~~~",
                @"~~~~~~~~~~~~~~~~|                                        ♣ ♣   ♣  ♣  ♣|~~~~~~~~~",
                @"~~~~~~~~~~~~~~~~~\                                           ♣   ♣     \~~~~~~~~",
                @"~~~~~~~~~~~~~~~~~~\                                                    |~~~~~~~~",
                @"~~~~~~~~~~~~~~~~~~~\                                                   |~~~~~~~~",
                @"~~~~~~~~~~~~~~~~~~~~\                                                   \~~~~~~~",
                @"~~~~~~~~~~~~~~~~~~~~~\                                                  |~~~~~~~",
                @"~~~~~~~~~~~~~~~~~~~~~|                                        7         |~~~~~~~",
                @"~~~~~~~~~~~~~~~~~~~~~|                                                  |~~~~~~~",
                @"~~~~~~~~~~~~~~~~~~~~~\              ░░░░░░░░░░░░░░░░                    /~~~~~~~",
                @"~~~~~~~~~~~~~~~~~~~~~~|        ░░░░░░░░░░░░░░░░░░░░░░░░░░              |~~~~~~~~",
                @"~~~~~~~~~~~~~~~~~~~~~~\    ░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░           |~~~~~~~~",
                @"~~~~~~~~~~~~~~~~~~~~~~~\░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░       /~~~~~~~~",
                @"~~~~~~~~~~~~~~~~~~~~~~~~|░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░ _/~~~~~~~~~",
                @"~~~~~~~~~~~~~~~~~~~~~~~~|░░░░░░░░8░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░|~~~~~~~~~~~",
                @"~~~~~~~~~~~~~~~~~~~~~~~~\░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░ /~~~~~~~~~~~~",
                @"~~~~~~~~~~~~~~~~~~~~~~~~~\░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░__|~~~~~~~~~~~~~",
                @"~~~~~~~~~~~~~~~~~~~~~~~~~~|░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░ ___|~~~~~~~~~~~~~~~~",
                @"~~~~~~~~~~~~~~~~~~~~~~~~~~|░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░_/~~~~~~~~~~~~~~~~~~~~",
                @"~~~~~~~~~~~~~~~~~~~~~~~~~~|░░░░░░░░░░░░░░░░░░░░░░░░░░░░ _/~~~~~~~~~~~~~~~~~~~~~~",
                @"~~~~~~~~~~~~~~~~~~~~~~~~~~\░░░░░░░░░░░░░░░░░░░░░░░░    /~~~~~~~~~~~~~~~~~~~~~~~~",
                @"~~~~~~~~~~~~~~~~~~~~~~~~~~~|░░░░░░░░░░░░░░░░░░░░░░░░  |~~~~~~~~~~~~~~~~~~~~~~~~~",
                @"~~~~~~~~~~~~~~~~~~~~~~~~~~~\░░░░░░░░░░9░░░░░░░░░░░░░░_|~~~~~~~~~~~~~~~~~~~~~~~~~",
                @"~~~~~~~~~~~~~~~~~~~~~~~~~~~~\░░░░░░░░░░░░░░░░░░░░░░░/~~~~~~~~~~~~~~~~~~~~~~~~~~~",
                @"~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\_░░░░░░░░░░░░░░░░  __/~~~~~~~~~~~~~~~~~~~~~~~~~~~~",
                @"~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\░░░░░░░░░░░░░░__/~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~",
                @"~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\_░░░░░░░░░░_/~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~",
                @"~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\___░░░__/~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~",
                @"~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\_/~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~",
                @"~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~",
                @"~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~",
                @"~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~"
            );
            //key
            /*@"0: Ice Mountain,     1: Dwarf Cave,     2: Dragon Lair     3: Witch Doctor,",
            @"4: Burned Village,   5: Plainsville     6: Rogue's Den,",
            @"7: Townopolus,       8: Sandland        9: Pyramid"*/
        }

        public void Play()
        {
            while (gameState != GameStates.QUIT)
            {
                switch (gameState)
                {
                    case GameStates.SETUP:
                        Setup();
                        ChangeState(GameStates.NEWGAME);
                        break;
                    case GameStates.NEWGAME:
                        Program.console.Print("Create Your Character\n");
                        //name
                        string name = Program.console.GetString("\nEnter Your Name: ");
                        //choose gender
                        string gender = "";
                        Menu selectGender = new Menu
                            (
                            "\nChoose your gender:",
                            new MenuItem("male", () => gender = "male"),
                            new MenuItem("female", () => gender = "female")
                            );
                        Program.console.Print(selectGender);
                        selectGender.SelectOption(Program.console.GetDigit(selectGender.Items.Length));

                        player = ChacterCreator.personBuilder
                            .WithAttributes(Species.human)
                            .WithAttribute("gender", gender)
                            .WithAttribute("health", new Utility(1, 1, 1))
                            .WithAttribute("die", new Die(DateTime.Now.GetHashCode()))
                            //.GetCurrentAttributes(out attributes)
                            .TryBuild(out string result);
                        //Program.console.Print(attributes);
                        if (player == null)
                        {
                            Program.console.Print("Error character could not be created!\n");
                            Program.console.Print(result);
                        }

                        player.SetName(name);
                        player.SetDescription("");

                        Program.console.Print("\nRoll Stats");
                        do
                        {
                            Program.console.ClearScreen();
                            ChacterCreator.RollStats(player, true);
                        }
                        while (Program.console.YesNo("Reroll?"));

                        Story1();
                        player.AddOrSetAttribute("location", burnedVillage.Location);
                        ChangeState(GameStates.PLACE);
                        break;
                    case GameStates.PLACE:
                        Place place = world.GetClosestPlace(player.GetAttributeValue<Vector2Int>("location"));
                        Menu placeMenu;
                        if (place.)
                            placeMenu = new Menu
                                (
                                $"\nYou are at {place.Name}",
                                new MenuItem("Look", () => Program.console.Print(place.Description)),
                                new MenuItem("Search", Search),
                                new MenuItem("Leave", () => ChangeState(GameStates.WORLDMAP))
                                );
                        Program.console.Print(placeMenu);
                        placeMenu.SelectOption(Program.console.GetDigit(placeMenu.Items.Length));
                        break;
                    case GameStates.WORLDMAP:

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

                        Menu encounterMenu = new Menu("Encounter");
                        /*MenuItem attack=new MenuItem("Attack",);
                        MenuItem useItem=new MenuItem("Use Item",);
                        MenuItem talk=new MenuItem("Talk",);
                        MenuItem leave=new MenuItem("Leave",);*/
                        Menu attackMenu = new Menu("Select Attack");
                        Menu talkMenu = new Menu("Encounter");
                        Menu dealMenu = new Menu("Encounter");
                        Menu itemMenu = new Menu("Encounter");
                        Menu targetMenu = new Menu("Encounter");
                        Menu currentMenu = encounterMenu;

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
                        ChangeState(prevGameState);
                        break;
                    case GameStates.DUNGEON:
                        //Program.console.Print();
                        break;
                    case GameStates.QUIT:
                        //Program.console.Print();
                        break;
                    default:
                        break;
                }
            }
        }

        public void Search()
        {
            Program.console.Print("You find: ");// +player.GetCurrentLocation().GetDescription());
            Program.console.Type("...", Program.textSpeed * 5);
            Die die = player.GetAttributeValue<Die>("die");
            int roll = die.Roll(0, 99);
            if (roll < 10)
            {
                int gold = die.Roll(0, 4);
                Program.console.Print(gold + " gold!");
                //int playerGold = player.GetAttributeValue<int>("gold");
                //player.AddGold(gold);
            }
            else if (roll < 20)
            {
                Program.console.Print("An Encounter!!!");
                //randomly create enemy
                ChangeState(GameStates.ENCOUNTER);
            }
            else if (roll < 40)
            {
                Program.console.Print("What is that!?");
                Program.console.Type(" ...", Program.textSpeed * 5);
                Program.console.Print("Wait never mind it's nothing.");
            }
            else
                Program.console.Print("Nothing!");
            Program.console.Anykey();
        }

        public void ChangeState(GameStates gameState)
        {
            prevGameState = this.gameState;
            this.gameState = gameState;
        }

        public void SetupNPCs()
        {
            //King of thieves
            Person kingOfThieves = ChacterCreator.personBuilder
                .New()
                .TryBuild(out string result);
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

        public void SetupItemTemplates()
        {
            //SWORD
            /*Thing sword = thingBuilder
                .TryBuild();*/
        }

        public void SetupPlaces()
        {
            Place iceMountain = new Place();
            iceMountain.SetName("Ice Mountain");
            iceMountain.SetDescription("You are at the ice mountains, you can't see the top.");
            iceMountain.SetLocation(new Vector2Int(1, 1));
            iceMountain.SetSize(1);
            world.Everywhere.Add(iceMountain);

            Place dwarfCave = new Place();
            dwarfCave.SetName("Dwarf Cave");
            dwarfCave.SetDescription("You are at the Dwarf's cave home.");
            dwarfCave.SetLocation(new Vector2Int(1, 1));
            dwarfCave.SetSize(1);
            world.Everywhere.Add(dwarfCave);

            Place dragonLair = new Place();
            dragonLair.SetName("Dragon's Lair");
            dragonLair.SetDescription("You are in the lair of the dragon.");
            dragonLair.SetLocation(new Vector2Int(1, 1));
            dragonLair.SetSize(1);
            world.Everywhere.Add(dragonLair);

            Place witchDoctorHut = new Place();
            witchDoctorHut.SetName("Witch Doctor's Hut");
            witchDoctorHut.SetDescription("You are at the ice mountains, you can't see the top.");
            witchDoctorHut.SetLocation(new Vector2Int(1, 1));
            witchDoctorHut.SetSize(1);
            world.Everywhere.Add(witchDoctorHut);

            Place burnedVillage = new Place();
            burnedVillage.SetName("Village");
            burnedVillage.SetDescription("You are surrounded by the ashes of what was once your village.");
            burnedVillage.SetLocation(new Vector2Int(1, 1));
            burnedVillage.SetSize(1);
            world.Everywhere.Add(burnedVillage);

            Place plainsville = new Place();
            plainsville.SetName("PlainsVille");
            plainsville.SetDescription("A rustic village! It's even got a rusty tick!");
            plainsville.SetLocation(new Vector2Int(1, 1));
            plainsville.SetSize(1);
            world.Everywhere.Add(plainsville);

            Place roguesDen = new Place();
            roguesDen.SetName("Thieve's Den");
            roguesDen.SetDescription("You are in the thieve's den.");
            roguesDen.SetLocation(new Vector2Int(1, 1));
            roguesDen.SetSize(1);
            world.Everywhere.Add(roguesDen);

            Place townopolus = new Place();
            townopolus.SetName("Townopolus");
            townopolus.SetDescription("What a town! It's got buildings and everything!");
            townopolus.SetLocation(new Vector2Int(1, 1));
            townopolus.SetSize(1);
            world.Everywhere.Add(townopolus);

            Place sandland = new Place();
            sandland.SetName("Sandland");
            sandland.SetDescription("A shady place in the hot desert.");
            sandland.SetLocation(new Vector2Int(1, 1));
            sandland.SetSize(1);
            world.Everywhere.Add(sandland);

            Place pyramid = new Place();
            pyramid.SetName("Pyramid");
            pyramid.SetDescription("You are in the pyramid.");
            pyramid.SetLocation(new Vector2Int(1, 1));
            pyramid.SetSize(1);
            world.Everywhere.Add(pyramid);
        }

        public void Story1()
        {
            Program.console.ClearScreen();
            Program.console.Print("STORY");
            Program.console.Print("\n\n<you can skip ahead at anytime by pressing any key>");
            Program.console.Type("\n\tYou live in a small peaceful village, in the land of Epica. One day", Program.textSpeed);
            Program.console.Type("\nyou are out exploring. After hiking for sometime, you stop to rest your feet.", Program.textSpeed);
            Program.console.Type("\nYou awake it is dark, black clouds are in the sky. You hurry back home to", Program.textSpeed);
            Program.console.Type("\nshelter. As you get closer you see light coming from ahead. You realize it's", Program.textSpeed);
            Program.console.Type("\nnot storm clouds, it's smoke! Your entire village is in flames!", Program.textSpeed);
            Program.console.Type("\nBy the time you reach your village all that is left is burning rubble and ash.", Program.textSpeed);
            Program.console.Type("\nNo survivors, nothing. The only thing that could have caused such destruction", Program.textSpeed);
            Program.console.Type("\nis a dragon! Watching the burning embers of what was once your home, you vow", Program.textSpeed);
            Program.console.Type("\nto avenge them, you will slay the dragon!", Program.textSpeed);
            Program.console.Type("\n...", Program.textSpeed * 5);
            Program.console.Anykey();

            Program.console.ClearScreen();
            Program.console.Type("\n\n\"What a tragity!\" a small voice says, you turn around to see a tiny man walk out from behind a tree.", Program.textSpeed);
            Program.console.Type("\n\"I'm Harold nice to meet you\" he says extending his tiny arm, you bend over and give him a finger to shake.", Program.textSpeed);
            Program.console.Type("\n\"Are you new to the land of Epica?\" (Y/N)\n", Program.textSpeed);
            if (Program.console.YesNo())
            {
                Program.console.Type("\n\"Welcome to the land of Epica. You can go to The Forest, The Town, The Caves,", Program.textSpeed);
                Program.console.Type("\nThe Ice Mountain and The Dragon's Lair. The weaker monsters are near the forest", Program.textSpeed);
                Program.console.Type("\nand town. If you need some help just press H. You should look around the area", Program.textSpeed);
                Program.console.Type("\nbefore you go there's got to be a weapon laying around here somewhere.", Program.textSpeed);
                Program.console.Type("\nMake sure to equip it before battle in the character menu.\n", Program.textSpeed);
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

        public static Game Load()
        {
            return new Game();
        }
    }
}