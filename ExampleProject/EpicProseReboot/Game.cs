using System;
using System.Collections.Generic;
using System.Linq;
using TextAdventureLibrary;

namespace EpicProseRedux
{
    public class Game
    {
        public enum GameStates
        {
            SETUP, NEWGAME, WORLDMAP, PLACE, CAMP, ENCOUNTER, DUNGEON, QUIT
        };
        public GameStates gameState;
        public GameStates prevGameState;

        Person player;

        World world;
        //Create Areas
        //the areas will be layered in the order they are added to the list
        Place rockyHard2 = new Place("Dragon Mountains", "a spooky mountain range", new Vector2Int(59, 13), 1);
        Place grasslandEasy = new Place("Plains of Epica", "easy plains area", new Vector2Int(40, 32), 16);
        Place grasslandHard = new Place("Rough Lands", "monsters about", new Vector2Int(64, 41), 6);
        Place swampEasy = new Place("Manageable Marsh", "it's swampy but not too bad", new Vector2Int(15, 21), 3);
        Place forestEasy = new Place("Friendly Forest", "nice place for a picnic", new Vector2Int(16, 21), 8);
        Place desertHard = new Place("Dangerous Desert", "hot sandy place", new Vector2Int(41, 59), 5);
        Place rockyHard = new Place("Northern Mountain Range", "cold winds blow in this region", new Vector2Int(22, 8), 4);
        Place swampHard = new Place("Black Bog", "a darky swampy area full of foul beasts", new Vector2Int(42, 14), 8);
        Place rockyEasy = new Place("Dwarven Mountains", "nice mountain area", new Vector2Int(37, 12), 9);
        Place forestHard = new Place("Wicked Woods", "some dangerous woods", new Vector2Int(64, 29), 8);
        Place desertEasy = new Place("Desert", "hot and sandy", new Vector2Int(47, 57), 14);
        //list of all areas in game
        List<Place> worldAreas;
        Dictionary<string, Dictionary<string, object>> itemTemplates;
        NounBuilder<Thing> itemBuilder = new NounBuilder<Thing>();

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
            SetupItemTemplates();
            SetupNPCs();
            SetupPlaces();
            Program.console.Print("Game Configured\n");
        }

        public void SetupWorld()
        {
            world = new World("Epica",
                "0: Ice Mountain, 1: Cave, 2: Lair, 3: Witch Doctor, 4: Burned Village,\n5: Plainsville 6: Rogue's Den, 7: Townopolus, 8: Sandland, 9: Pyramid",
                WorldMapCells.worldMapKey
                );

            world.SetDateTime(new DateTime(900, 10, 24, 9, 0, 0));//y,m,d,h,m,s

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
        }

        public void Play()
        {
            Place within;
            List<MenuItem> menuItems = new List<MenuItem>();
            List<MenuItem> subMenuItems = new List<MenuItem>();

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
                        selectGender.Execute();

                        player = ChacterCreator.personBuilder
                            .New()
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
                        player.SetLocation(world.Everywhere["Village"].Location);

                        Program.console.Print("\nRoll Stats");
                        do
                        {
                            Program.console.ClearScreen();
                            ChacterCreator.RollStats(player, true);
                        }
                        while (Program.console.YesNo("Reroll?"));

                        //Story1();
                        ChangeState(GameStates.PLACE);
                        break;
                    case GameStates.PLACE:
                        within = world.WithinBordersOf(player.Location);
                        //Program.console.Print($"You are at {within.Name}");
                        Map dungeon = within.GetAttributeValue<Map>("dungeon");
                        //Program.console.Print($"Burned Village inventory count: {within.Inventory.Things.Count}.\n");
                        //Program.console.Anykey();
                        menuItems = new List<MenuItem>();
                        menuItems.Add(new MenuItem("Look", () =>
                        {
                            Program.console.Print(within.Description);
                            //Program.console.Print($"You see {within.Population[0].Name}");
                            if (within.Population.Count > 0)
                            {
                                Program.console.Print("\nYou see ");
                                foreach (Person npc in within.Population)
                                {
                                    Program.console.Print($"{npc.Name}, ");
                                }
                            }
                            if (within.Inventory.Things.Count > 0)
                            {
                                Program.console.Print("\nYou see ");
                                foreach (Thing t in within.Inventory.Things)
                                {
                                    Program.console.Print($"a {t.Name}, ");
                                }
                            }
                            Program.console.Anykey();
                        }));
                        menuItems.Add(new MenuItem("Search", Search));
                        if (within.Population.Count > 0)
                        {
                            menuItems.Add(new MenuItem("Talk", () =>
                            {
                                ChangeState(GameStates.ENCOUNTER);
                            }));

                            menuItems.Add(new MenuItem("Examine Person", () =>
                            {
                                subMenuItems = new List<MenuItem>();
                                foreach (Person person in within.Population)
                                {
                                    subMenuItems.Add(new MenuItem(person.Name, () => { Examine(person); }));
                                }
                                Menu subMenu = new Menu($"\nWho do you want to examine?", subMenuItems.ToArray());
                                Program.console.Print(subMenu);
                                subMenu.SelectOption(Program.console.GetDigit(subMenu.Items.Length));
                                subMenu.Execute();
                            }));

                            menuItems.Add(new MenuItem("Attack", () =>
                            {
                                ChangeState(GameStates.ENCOUNTER);
                            }));

                            menuItems.Add(new MenuItem("Pick Pocket", () =>
                            {
                                subMenuItems = new List<MenuItem>();
                                foreach (Person person in within.Population)
                                {
                                    subMenuItems.Add(new MenuItem(person.Name, () => { PickPocket(person); }));
                                }
                                Menu subMenu = new Menu($"\nWhose pocket do you want to pick?", subMenuItems.ToArray());
                                Program.console.Print(subMenu);
                                subMenu.SelectOption(Program.console.GetDigit(subMenu.Items.Length));
                                subMenu.Execute();
                            }));
                        }
                        if (within.Inventory.Things.Count > 0)
                        {
                            menuItems.Add(new MenuItem("Take", () =>
                            {
                                subMenuItems = new List<MenuItem>();
                                foreach (Thing thing in within.Inventory.Things)
                                {
                                    subMenuItems.Add(new MenuItem(thing.Name, () => { TakeThing(thing, within.Inventory); }));
                                }
                                Menu subMenu = new Menu($"\nWhat do you want to take?", subMenuItems.ToArray());
                                Program.console.Print(subMenu);
                                subMenu.SelectOption(Program.console.GetDigit(subMenu.Items.Length));
                                subMenu.Execute();
                            }));

                            menuItems.Add(new MenuItem("Examine item", () =>
                            {
                                subMenuItems = new List<MenuItem>();
                                foreach (Thing thing in within.Inventory.Things)
                                {
                                    subMenuItems.Add(new MenuItem(thing.Name, () => { Examine(thing); }));
                                }
                                Menu subMenu = new Menu($"\nWhat do you want to examine?", subMenuItems.ToArray());
                                Program.console.Print(subMenu);
                                subMenu.SelectOption(Program.console.GetDigit(subMenu.Items.Length));
                                subMenu.Execute();
                            }));
                        }
                        if (dungeon != null)
                        {
                            menuItems.Add(new MenuItem("Enter Dungeon", Search));
                        }
                        //add in a n inn/tavern?
                        menuItems.Add(new MenuItem("Leave", () => ChangeState(GameStates.WORLDMAP)));
                        Menu placeMenu = new Menu($"\nYou are at { within.Name }", menuItems.ToArray());
                        Program.console.Print(placeMenu);
                        placeMenu.SelectOption(Program.console.GetDigit(placeMenu.Items.Length));
                        placeMenu.Execute();
                        break;
                    case GameStates.WORLDMAP:
                        within = world.WithinBordersOf(player.Location);
                        menuItems = new List<MenuItem>();
                        menuItems.Add(new MenuItem("Look", () =>
                        {
                            Program.console.Print($"You are in the ");//area
                            if (within != null)
                                Program.console.Print($" outside of {within.Name}");
                            Program.console.Print("\n");
                            Program.console.Anykey();
                        }));
                        menuItems.Add(new MenuItem("Search", Search));
                        menuItems.Add(new MenuItem("Travel", TravelMenu));
                        menuItems.Add(new MenuItem("Check Map", () =>
                        {
                            Program.console.Print(world.Map);
                            //key
                            Program.console.Print("0: Ice Mountain,     1: Dwarf Cave,     2: Dragon Lair     3: Witch Doctor,\n");
                            Program.console.Print("4: Burned Village,   5: Plainsville     6: Rogue's Den,\n");
                            Program.console.Print("7: Townopolus,       8: Sandland        9: Pyramid\n");
                            Program.console.Anykey();
                        }));
                        menuItems.Add(new MenuItem("Camp", () => ChangeState(GameStates.CAMP)));//formerly character menu
                        if (within != null)
                        {
                            menuItems.Add(new MenuItem("Enter " + within.Name, () => { gameState = GameStates.PLACE; }));
                        }
                        Menu worldMenu = new Menu($"Epica", menuItems.ToArray());
                        Program.console.Print(worldMenu);
                        worldMenu.SelectOption(Program.console.GetDigit(worldMenu.Items.Length));
                        worldMenu.Execute();
                        //character menu
                        //ConsoleKey input = TextFunctions.TextMenu("(L)ook,(E)quipment,(S)tatus,(R)est,(B)ack");
                        break;
                    case GameStates.CAMP:
                        menuItems = new List<MenuItem>();
                        menuItems.Add(new MenuItem("Character", CharacterMenu));
                        menuItems.Add(new MenuItem("Inventory", Inventory));
                        menuItems.Add(new MenuItem("Rest", Rest));
                        menuItems.Add(new MenuItem("Leave", () => ChangeState(GameStates.WORLDMAP)));
                        menuItems.Add(new MenuItem("Save", Save));
                        menuItems.Add(new MenuItem("Quit", () => ChangeState(GameStates.QUIT)));
                        Menu campMenu = new Menu($"\nYou are at camp", menuItems.ToArray());
                        Program.console.Print(campMenu);
                        campMenu.SelectOption(Program.console.GetDigit(campMenu.Items.Length));
                        campMenu.Execute();
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

        public void Save()
        {
            Program.console.Print("SAVING...");
            Program.console.Print("SAVED");
            Program.console.Anykey();
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

        public void TakeThing(Thing thing, Inventory inv)
        {
            if (player.Inventory.TryAddToInventory(thing))
            {
                Program.console.Print($"You take the {thing.Name}");
                inv.TryRemoveFromInventory(thing);
            }
            else
            {
                Program.console.Print($"You can not take the {thing.Name}");
            }
            Program.console.Anykey();
        }

        public void Examine(Thing thing)
        {
            Program.console.Print($"You examine the {thing.Name}:\n");
            Program.console.PrintDescription(thing);
            Program.console.Anykey();
        }

        public void Examine(Person person)
        {
            Program.console.Print($"You examine {person.Name}:\n");
            Program.console.PrintDescription(person);
            Program.console.Anykey();
        }

        public void PickPocket(Person person)
        {
            int playerRoll = player.GetAttributeValue<Stat>("dexterity").StatCheck(player.GetAttributeValue<Die>("die"));
            int personRoll = person.GetAttributeValue<Stat>("dexterity").StatCheck(person.GetAttributeValue<Die>("die"));
            if (playerRoll <= 0)
            {
                Program.console.Print($"You get caught picking {person.Name}'s pocket!\n");
                Program.console.Anykey();
                //encounter
            }
            else if (playerRoll > personRoll)
            {
                Die die = player.GetAttributeValue<Die>("die");
                int roll = die.Roll(0, person.Inventory.Things.Count);
                Thing thing = person.Inventory.Things[roll];
                if (player.Inventory.TryAddToInventory(thing))
                {
                    person.Inventory.TryRemoveFromInventory(thing);
                    Program.console.Print($"You successfully picked {person.Name}'s pocket!\n");
                    Program.console.Print($"{thing.Name} has been added to your inventory.\n");
                }
            }
        }

        public void Encounter(params Person[] people)
        {
            //initiative
            int[] initiatives = new int[people.Length];
            //roll for each Person
            for (int i = 0; i < people.Length; i++)
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
            MenuItem attack = new MenuItem("Attack", () => { });
            MenuItem useItem = new MenuItem("Use Item", () => { });
            MenuItem talk = new MenuItem("Talk", () => { });
            MenuItem leave = new MenuItem("Leave", () => { });
            Menu attackMenu = new Menu("Select attack");
            Menu talkMenu = new Menu("What do you want to say?");
            Menu dealMenu = new Menu("Propose deal");
            Menu itemMenu = new Menu("What do you want to use?");
            Menu currentMenu = encounterMenu;

            //turn based loop
            while (true)
            {
                //build encounter menu
                currentMenu = new Menu("What will you do?");
                //Program.console.Print(currentMenu.Title);
                Program.console.Print(currentMenu);
                //orderedPeople[turn].GetAttributeValue<Brain>().MakeChoice(encounterMenu);
                currentMenu.SelectOption(Program.console.GetDigit(currentMenu.Items.Length));
                currentMenu.Execute();

                turn = turn < orderedPeople.Length ? turn++ : 0;

                break;
            }
        }

        public void Rest()
        {
            if (GetCurrentHealth(player) == GetMaxHealth(player))
            {
                bool wounded = player.GetAttributeValue<bool>("isWounded");
                if (wounded)
                {
                    player.AddOrSetAttribute("isWounded", false);
                    world.AddTimeSpan(new TimeSpan(1, 0, 0));
                    Program.console.Print("You rest for one hour\n");
                    Program.console.Print("You heal your wounds!\n");
                }
                //else
                Program.console.Print("You don't need to rest!\n");
            }
            else
            {
                do
                {
                    world.AddTimeSpan(new TimeSpan(1, 0, 0));
                    Program.console.Print("You rest for one hour\n");
                    Program.console.Type("...", Program.textSpeed * 5);
                    bool wounded = player.GetAttributeValue<bool>("isWounded");
                    if (wounded)
                    {
                        player.AddOrSetAttribute("isWounded", false);
                        Program.console.Print("You heal your wounds!\n");
                    }
                    ModifyHealth(player, 10);
                    Program.console.SetColorToHealth(GetHealthPercent(player));
                    Program.console.Print("\n");
                    Place within = world.WithinBordersOf(player.Location);
                    Place town1;
                    world.Everywhere.TryGetValue("", out town1);
                    Place town2;
                    world.Everywhere.TryGetValue("", out town2);
                    Place town3;
                    world.Everywhere.TryGetValue("", out town3);
                    if (within != town1 && within != town2 && within != town3)
                    {
                        int roll = world.Die.Roll(1, 100);
                        //if (showRolls)
                        Program.console.Print("Encounter Roll: " + roll);
                        //if (roll < player.GetCurrentArea().GetEncounterChance())
                        {
                            //new Battle(player, player.GetCurrentArea().RandomEncounter());
                            //break;
                        }
                    }
                    /*if (Console.KeyAvailable)
                    {
                        Console.WriteLine("You awake!");
                        while (Console.KeyAvailable) // Flushes the input queue.
                            Console.ReadKey();
                        break;
                    }*/
                }
                while (GetCurrentHealth(player) != GetMaxHealth(player));
                if (GetCurrentHealth(player) == GetMaxHealth(player))
                {
                    Program.console.Print("You awake fully healed!\n");
                }
            }
            Program.console.Anykey();
        }

        public void CharacterMenu()
        {
            //print description
            Program.console.Print("DESCRIPTION\n");
            Program.console.Print(player);
            //print stats
            Program.console.Print("STATS\n");
            Dictionary<string, Stat> stats = player.FilterAttributesByType<Stat>();
            foreach (var stat in stats)
            {
                Program.console.Print($"{stat.Key}: {stat.Value.Value}\n");
            }
            Program.console.Print("\n");
            //print health
            Program.console.Print("HEALTH\n");
            Program.console.Print($"{GetCurrentHealth(player)}/{GetMaxHealth(player)}\n\n");
            //print status
            Program.console.Print("STATUS\n");
            Program.console.Print("Healthy\n\n");
        }

        public void Inventory()
        {
            //ConsoleKey input = TextFunctions.TextMenu("(L)ook,(E)quip,(R)epair,(U)se potion,(D)rop,(B)ack");
            Program.console.Print("ITEMS\n");
            Dictionary<string, Thing> items = player.FilterAttributesByType<Thing>();
            int count = 0;
            foreach (var item in items)
            {
                count++;
                Program.console.Print($"{count}: {item.Key}\n");
            }
            Program.console.Print("\n");
        }

        public void TravelMenu()
        {
            List<Place> allPlaces = world.Everywhere.Values.ToList();
            //organize menu items by closest to player

            Menu travelMenu0 = new Menu("Where do you want to go?");
            Menu travelMenu1 = new Menu("Where do you want to go?");
            List<MenuItem> menuItems0 = new List<MenuItem>();
            List<MenuItem> menuItems1 = new List<MenuItem>();
            for (int i = 0; i < allPlaces.Count; i++)
            {
                if (i < 6)
                    menuItems0.Add(new MenuItem(allPlaces[i].Name, () => Travel(allPlaces[i])));
                else
                    menuItems1.Add(new MenuItem(allPlaces[i].Name, () => Travel(allPlaces[i])));
            }
            menuItems0.Add(new MenuItem("Next", () =>
            {
                Program.console.Print(travelMenu1);
                travelMenu1.SelectOption(Program.console.GetDigit(travelMenu1.Items.Length));
                travelMenu1.Execute();
            }));
            //menuItems0.Add(new MenuItem("Cancel", () => { return; }));
            menuItems1.Add(new MenuItem("Prev", () =>
            {
                Program.console.Print(travelMenu0);
                travelMenu0.SelectOption(Program.console.GetDigit(travelMenu0.Items.Length));
                travelMenu0.Execute();
            }));
            menuItems1.Add(new MenuItem("Cancel", () => { }));

            travelMenu0.Items = menuItems0.ToArray();
            travelMenu1.Items = menuItems1.ToArray();

            Program.console.Print(travelMenu0);
            travelMenu0.SelectOption(Program.console.GetDigit(travelMenu0.Items.Length));
            travelMenu0.Execute();
        }

        public void Travel(Place place)
        {
            do
            {
                //if (!player.IsAlive())
                //    return;
                if (!Program.console.IsKeyAvailable())
                {
                    Program.console.Print("You stop!");
                    /*while (Console.KeyAvailable) // Flushes the input queue.
                        Console.ReadKey();
                    break;*/
                }
                else
                {
                    Place tempArea = GetCurrentArea(player.Location);
                    //int e;
                    //if (equipedArmour == null)
                    //    e = 0;
                    //else
                    //    e = equipedArmour.GetEncumberence();
                    double dist = Vector2Int.Distance(place.Location, player.Location);
                    Program.console.Print($"You head towards {place.Name}in the { GetCurrentArea(place.Location)} region.\n");
                    Program.console.Type("...", Program.textSpeed);
                    int speed = (int)player.GetAttributeValue<Stat>("speed").Value;

                    if (player.Location.X > place.Location.X)
                    {
                        if (player.Location.X - place.Location.X <= speed)
                        {
                            player.Location.X = place.Location.X;
                        }
                        else
                        {
                            player.Location.X = player.Location.X - speed;
                        }
                    }
                    else if (player.Location.X < place.Location.X)
                    {
                        if (place.Location.X - player.Location.X <= speed)
                        {
                            player.Location.X = place.Location.X;
                        }
                        else
                        {
                            player.Location.X = player.Location.X + speed;
                        }
                    }
                    if (player.Location.Y > place.Location.Y)
                    {
                        if (player.Location.Y - place.Location.Y <= speed)
                        {
                            player.Location.Y = place.Location.Y;
                        }
                        else
                        {
                            player.Location.Y = player.Location.Y - speed;
                        }
                    }
                    else if (player.Location.Y < place.Location.Y)
                    {
                        if (place.Location.Y - player.Location.Y <= speed)
                        {
                            player.Location.Y = place.Location.Y;
                        }
                        else
                        {
                            player.Location.Y = player.Location.Y + speed;
                        }
                    }

                    foreach (Place p in worldAreas)
                    {
                        Place current = GetCurrentArea(player.Location);
                        if (current != tempArea)
                        {
                            Program.console.Print($"You enter the { current.Name} region.\n");
                        }
                    }

                    world.AddTimeSpan(new TimeSpan(1, 0, 0));
                    Program.console.Print("You travel for one hour\n");
                    Program.console.Type("...", Program.textSpeed * 5);
                    Program.console.Print("\n");
                    int roll = world.Die.Roll(0, 99);
                    //if (showRolls)
                    //    Program.console.Print("Encounter Roll: " + roll);
                    //if (roll < player.GetCurrentArea().GetEncounterChance())
                    //{
                    //    new Battle(player, player.GetCurrentArea().RandomEncounter());
                    //    break;
                    //}
                }

            }
            while (world.WithinBordersOf(player.Location) != place);
            Program.console.Print($"You arrived at {place.Name}.\n");
            Program.console.Anykey();
        }

        public void ChangeState(GameStates gameState)
        {
            prevGameState = this.gameState;
            this.gameState = gameState;
        }

        public void RevertState()
        {
            gameState = prevGameState;
        }

        public Place GetCurrentArea(Vector2Int location)
        {
            //greedy algorythm, areas must be sorted in right order to work right
            foreach (Place p in worldAreas)
            {
                if (p.IsLocationWithin(location))
                    return p;
            }
            return null;
        }

        public void SetupNPCs()
        {
            string result;

            //King of thieves
            Person kingOfThieves = ChacterCreator.personBuilder
                .New()
                .TryBuild(out result);
            world.Everyone.Add("kingOfThieves", kingOfThieves);

            //the Ice Giant
            Person iceGiant = ChacterCreator.personBuilder
                .New()
                .TryBuild(out result);
            world.Everyone.Add("iceGiant", iceGiant);

            //mummyLord
            Person mummyLord = ChacterCreator.personBuilder
                .New()
                .TryBuild(out result);
            world.Everyone.Add("mummyLord", mummyLord);

            //dragon
            Person dragon = ChacterCreator.personBuilder
                .New()
                .TryBuild(out result);
            world.Everyone.Add("dragon", dragon);

            //John the Blacksmith
            Person john = ChacterCreator.personBuilder
                .New()
                .TryBuild(out result);
            world.Everyone.Add("john", john);

            //Bob the Witchdoctor
            Person bob = ChacterCreator.personBuilder
                .New()
                .TryBuild(out result);
            world.Everyone.Add("bob", bob);

            //Frank the Dwarf
            Person frank = ChacterCreator.personBuilder
                .New()
                .TryBuild(out result);
            world.Everyone.Add("frank", frank);

            //David the Warrior
            Person david = ChacterCreator.personBuilder
                .New()
                .TryBuild(out result);
            world.Everyone.Add("david", david);

            //Steve the Knight
            Person knight = ChacterCreator.personBuilder
                .New()
                .TryBuild(out result);
            world.Everyone.Add("knight", knight);

            //Carol the Merchant
            Person carol = ChacterCreator.personBuilder
                .New()
                .TryBuild(out result);
            world.Everyone.Add("carol", carol);

            //Joline the Sorcerous
            Person joline = ChacterCreator.personBuilder
                .New()
                .TryBuild(out result);
            world.Everyone.Add("joline", joline);

            //Jack the Tavernkeeper
            Person jack = ChacterCreator.personBuilder
                .New()
                .TryBuild(out result);
            world.Everyone.Add("jack", jack);
        }

        public void SetupItemTemplates()
        {
            itemTemplates = new Dictionary<string, Dictionary<string, object>>();
            Dictionary<string, object> dagger = new Dictionary<string, object>();
            itemTemplates.Add("dagger", dagger);
            Dictionary<string, object> shortsword = new Dictionary<string, object>();
            itemTemplates.Add("shortsword", shortsword);
            Dictionary<string, object> longsword = new Dictionary<string, object>();
            itemTemplates.Add("longsword", longsword);
            Dictionary<string, object> club = new Dictionary<string, object>();
            itemTemplates.Add("club", club);
            Dictionary<string, object> mace = new Dictionary<string, object>();
            itemTemplates.Add("mace", mace);
            Dictionary<string, object> flail = new Dictionary<string, object>();
            itemTemplates.Add("flail", flail);
            Dictionary<string, object> dragonbane = new Dictionary<string, object>();
            itemTemplates.Add("dragonbane", dragonbane);
            Dictionary<string, object> flamingFlail = new Dictionary<string, object>();
            itemTemplates.Add("flamingFlail", flamingFlail);

            Dictionary<string, object> leather = new Dictionary<string, object>();
            itemTemplates.Add("leather", leather);
            Dictionary<string, object> chainmail = new Dictionary<string, object>();
            itemTemplates.Add("chainmail", chainmail);
            Dictionary<string, object> platemail = new Dictionary<string, object>();
            itemTemplates.Add("platemail", platemail);
            Dictionary<string, object> icearmour = new Dictionary<string, object>();
            itemTemplates.Add("icearmour", icearmour);

            //("dagger", "a pointy thing", 2, 20, 1, Weapon.weaponTypes.edged, false);
            //Thing shortsword = new Thing("shortsword", "a small sword", 4, 100, 2, Weapon.weaponTypes.edged, false);
            //Thing longsword = new Thing("longsword", "a good weapon", 6, 350, 3, Weapon.weaponTypes.edged, false);
            //Thing club = new Weapon("club", "a big stick", 3, 20, 1, Weapon.weaponTypes.blunt, false);
            //Thing mace = new Weapon("mace", "a chunk of iron on a stick", 6, 100, 2, Weapon.weaponTypes.blunt, false);
            //Thing flail = new Weapon("flail", "a ball and chain", 3, 350, 9, Weapon.weaponTypes.blunt, false);
            //Thing dragonbane = new Weapon("dragon's bane", "it shimmers with magic", 7, 1000, 4, Weapon.weaponTypes.edged, true);
            //good against ice and undead
            //Thing flamingFlail = new Weapon("flaming flail", "a magic burning sphere on a chain", 8, 1000, 5, Weapon.weaponTypes.blunt, true);

            //Thing leather = new Armour("leather", "old leather armour", 20, 30, 1, 0, false);
            //Thing chainmail = new Armour("chainmail", "this armour is ok", 40, 150, 2, 1, false);
            //Thing platemail = new Armour("platemail", "good armour", 60, 400, 3, 2, false);
            //Thing icearmour = new Armour("ice armour", "it looks like ice, cool to the touch", 50, 1000, 4, 2, true);

            //Thing potion = new Potion();                       
        }

        public void SetupPlaces()
        {
            Place iceMountain = new Place();
            iceMountain.SetName("Ice Mountain");
            iceMountain.SetDescription("You are at the ice mountains, you can't see the top.");
            iceMountain.SetLocation(new Vector2Int(24, 9));
            iceMountain.SetSize(1);
            world.AddPlace(iceMountain);

            Place dwarfCave = new Place();
            dwarfCave.SetName("Dwarf Cave");
            dwarfCave.SetDescription("You are at the Dwarf's cave home.");
            dwarfCave.SetLocation(new Vector2Int(36, 12));
            dwarfCave.SetSize(1);
            world.AddPlace(dwarfCave);

            Place dragonLair = new Place();
            dragonLair.SetName("Dragon's Lair");
            dragonLair.SetDescription("You are in the lair of the dragon.");
            dragonLair.SetLocation(new Vector2Int(59, 13));
            dragonLair.SetSize(1);
            world.AddPlace(dragonLair);

            Place witchDoctorHut = new Place();
            witchDoctorHut.SetName("Witch Doctor's Hut");
            witchDoctorHut.SetDescription("You are at the ice mountains, you can't see the top.");
            witchDoctorHut.SetLocation(new Vector2Int(17, 21));
            witchDoctorHut.SetSize(1);
            world.AddPlace(witchDoctorHut);

            Place burnedVillage = new Place();
            burnedVillage.SetName("Village");
            burnedVillage.SetDescription("You are surrounded by the ashes of what was once your village.");
            burnedVillage.SetLocation(new Vector2Int(47, 22));
            burnedVillage.SetSize(1);
            burnedVillage.InitializeInventory(1000, 1000, 1000);
            Thing weapon = new Thing();
            string errorMessage;
            if (world.Die.Roll(0, 1) == 0)
                weapon = itemBuilder.WithName("dagger").WithDescription("a pointy thing").WithAttributes(itemTemplates["dagger"]).TryBuild(out errorMessage);
            else
                weapon = itemBuilder.WithName("club").WithDescription("a thick stick").WithAttributes(itemTemplates["club"]).TryBuild(out errorMessage);
            if (errorMessage != null)
                Program.console.Print(errorMessage + "\n");
            burnedVillage.Inventory.TryAddToInventory(weapon);
            Program.console.Print($"Burned Village inventory count: {burnedVillage.Inventory.Things.Count}.\n");
            Program.console.Anykey();
            world.AddPlace(burnedVillage);

            Place plainsville = new Place();
            plainsville.SetName("PlainsVille");
            plainsville.SetDescription("A rustic village! It's even got a rusty tick!");
            plainsville.SetLocation(new Vector2Int(34, 29));
            plainsville.SetSize(1);
            world.AddPlace(plainsville);

            Place roguesDen = new Place();
            roguesDen.SetName("Thieve's Den");
            roguesDen.SetDescription("You are in the thieve's den.");
            roguesDen.SetLocation(new Vector2Int(67, 30));
            roguesDen.SetSize(1);
            world.AddPlace(roguesDen);

            Place townopolus = new Place();
            townopolus.SetName("Townopolus");
            townopolus.SetDescription("What a town! It's got buildings and everything!");
            townopolus.SetLocation(new Vector2Int(62, 41));
            townopolus.SetSize(1);
            world.AddPlace(townopolus);

            Place sandland = new Place();
            sandland.SetName("Sandland");
            sandland.SetDescription("A shady place in the hot desert.");
            sandland.SetLocation(new Vector2Int(33, 48));
            sandland.SetSize(1);
            world.AddPlace(sandland);

            Place pyramid = new Place();
            pyramid.SetName("Pyramid");
            pyramid.SetDescription("You are in the pyramid.");
            pyramid.SetLocation(new Vector2Int(39, 56));
            pyramid.SetSize(1);
            world.AddPlace(pyramid);
        }

        public void SetupAreas()
        {
            //add areas to list
            rockyHard2.AddOrSetAttribute("enemyEncounterChance", 30);
            grasslandEasy.AddOrSetAttribute("enemyEncounterChance", 35);
            grasslandHard.AddOrSetAttribute("enemyEncounterChance", 30);
            swampEasy.AddOrSetAttribute("enemyEncounterChance", 30);
            desertHard.AddOrSetAttribute("enemyEncounterChance", 35);
            rockyHard.AddOrSetAttribute("enemyEncounterChance", 3);
            swampHard.AddOrSetAttribute("enemyEncounterChance", 35);
            rockyEasy.AddOrSetAttribute("enemyEncounterChance", 35);
            forestHard.AddOrSetAttribute("enemyEncounterChance", 30);
            desertEasy.AddOrSetAttribute("enemyEncounterChance", 35);
            worldAreas.Add(grasslandEasy);
            worldAreas.Add(grasslandHard);
            worldAreas.Add(desertEasy);
            worldAreas.Add(forestHard);
            worldAreas.Add(swampHard);
            worldAreas.Add(rockyEasy);
            worldAreas.Add(rockyHard);
            worldAreas.Add(desertHard);
            worldAreas.Add(forestEasy);
            worldAreas.Add(swampEasy);
            worldAreas.Add(rockyHard2);
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
            if (!player.GetAttributeValue<bool>("isAlive"))
            {
                Program.console.Print("\nYou were killed! Your village was not avenged!");
                //JingleBad();
            }
            //else if (!dragon.GetAttributeValue<bool>("isAlive"))
            {
                Program.console.Print("\nYou killed The Dragon! Your village has been avenged!! You beat Epic Prose SE!!!");
                //JingleGood();
            }
            //Program.console.Print("\nYou had " + player.GetGold() + " gold and killed " + player.GetKills() + " monsters.");
            Program.console.Anykey();
        }

        public static Game Load()
        {
            return new Game();
        }

        public static int GetCurrentHealth(Person person)
        {
            return (int)person.GetAttributeValue<Utility>("health").Value;
        }

        public static int GetMaxHealth(Person person)
        {
            return (int)person.GetAttributeValue<Utility>("health").Max;
        }

        public static int GetHealthPercent(Person person)
        {
            return (int)(person.GetAttributeValue<Utility>("health").Max
                / person.GetAttributeValue<Utility>("health").Max * 100);
        }

        public static void ModifyHealth(Person person, int amount)
        {
            person.GetAttributeValue<Utility>("health").ModifyValue(amount);
        }
    }
}