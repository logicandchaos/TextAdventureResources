using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Runtime.InteropServices;
using TextAdventureLibrary;

/// <summary>
/// This is a remake of my Text Adventure Epic Prose to test out my TextAdventureLibrary
/// </summary>

//Predicate<Person> hasSwordEquiped = person => person.GetAttributeValue<Thing>("equipedItem") == sword;

//PersonBuilder personBuilder = new PersonBuilder();
//Person character = personBuilder
//    .WithAttributes(human)
//    .Build();

namespace EpicProseRedux
{
    class Program
    {
        //FULL SCREEN CODE
        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();
        private static IntPtr ThisConsole = GetConsoleWindow();
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        private const int HIDE = 0;
        private const int MAXIMIZE = 3;
        private const int MINIMIZE = 6;
        private const int RESTORE = 9;

        static CsConsole console = new CsConsole();
        //Game Settings
        static int textSpeed = 20;
        static bool showRolls;
        static bool showEnemyHealth;
        static bool sound;

        enum GameStates { MAIN_MENU, OPTIONS, PLAY_MENU, NEW_GAME, LOAD_GAME, WORLD, ENCOUNTER, QUIT };
        static GameStates gameState = GameStates.MAIN_MENU;

        static Menu mainMenu;
        static Menu playMenu;
        static Menu travelMenu;
        static Menu travelMenu2;
        static Menu worldMenu;
        static Menu characterMenu;
        static Menu pickUpMenu;
        static Menu inventory;
        static Menu encounterMenu;
        static Menu fileMenu;
        static Menu helpMenu;

        //create cells
        //create mapKeys
        //MapKey worldMapKey = new MapKey();
        //create maps
        //Matrix worldMap = new Matrix();

        //might not use templates for bosses and just make the characters with factory
        //static Template iceGiant = new Template("ice giant");
        //king of thieves - human
        //static Template dragon = new Template("dragon");

        //name geberators
        static NameGenerator humanMaleNameGenerator;
        static NameGenerator humanFemaleNameGenerator;
        static NameGenerator humanLastNameGenerator;
        static NameGenerator animalNameGenerator;
        static NameGenerator monsterNameGenerator;

        //Species templates
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

        //item templates
        public static Dictionary<string, object> sword = new Dictionary<string, object>();

        public static Builder<Person> personBuilder = new Builder<Person>(
            "name",
            "age",
            "species",
            "gender",
            "bodyType",
            "humanoid",
            "strength",
            "vitality",
            "dexterity",
            "speed",
            "intelligence",
            "charisma",
            "health"
            );
        public static Builder<Place> placeBuilder = new Builder<Place>(
            "name",
            "location"
            );
        public static Builder<Thing> thingBuilder = new Builder<Thing>(
            "name",
            "weight"
            );
        static Person player;

        static Cell desertCell = new Cell('░', Color.DarkYellow, Color.DarkRed);
        static Cell forestCell = new Cell('♣', Color.DarkYellow, Color.DarkRed);
        static Cell waterCell = new Cell('~', Color.DarkYellow, Color.DarkRed);
        static Cell swampCell = new Cell('▒', Color.DarkYellow, Color.DarkRed);
        static Cell plainsCell = new Cell(' ', Color.DarkYellow, Color.DarkRed);
        static Cell mountainCell0 = new Cell('m', Color.DarkYellow, Color.DarkRed);
        static Cell mountainCell1 = new Cell('m', Color.DarkYellow, Color.DarkRed);
        static Cell mountainCell2 = new Cell('m', Color.DarkYellow, Color.DarkRed);
        static Cell shoreCell0 = new Cell('▒', Color.DarkYellow, Color.DarkRed);
        static Cell shoreCell1 = new Cell('▒', Color.DarkYellow, Color.DarkRed);
        static Cell shoreCell2 = new Cell('▒', Color.DarkYellow, Color.DarkRed);
        static Cell mark0 = new Cell('0', Color.DarkYellow, Color.DarkRed);
        static Cell mark1 = new Cell('1', Color.DarkYellow, Color.DarkRed);
        static Cell mark2 = new Cell('2', Color.DarkYellow, Color.DarkRed);
        static Cell mark3 = new Cell('3', Color.DarkYellow, Color.DarkRed);
        static Cell mark4 = new Cell('4', Color.DarkYellow, Color.DarkRed);
        static Cell mark5 = new Cell('5', Color.DarkYellow, Color.DarkRed);
        static Cell mark6 = new Cell('6', Color.DarkYellow, Color.DarkRed);
        static Cell mark7 = new Cell('7', Color.DarkYellow, Color.DarkRed);
        static Cell mark8 = new Cell('8', Color.DarkYellow, Color.DarkRed);
        static Cell mark9 = new Cell('9', Color.DarkYellow, Color.DarkRed);

        static MapKey mapKey = new MapKey();
        //I have 10 locations, so I might number them 0-9
        static Matrix worldMap = new Matrix
            (
            "Epica",
            "0: Ice Mountain, 1: Cave, 2: Lair, 3: Witch Doctor, 4: Burned Village,"
            + "\n5: Plainsville 6: Rogue's Den, 7: Townopolus, 8: Sandland, 9: Pyramid"
            );

        const bool DEBUG = true;

        static void Main(string[] args)
        {
            //Set full screen
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            ShowWindow(ThisConsole, MAXIMIZE);

            console.Print("Loading.", true);
            Setup();
            console.Print("Finished Loading.", true);

            console.SetDebugMode(DEBUG);
            console.DebugMessage("Debug mode is active");
            if (DEBUG)
                console.Anykey();

            if (!DEBUG)
                Intro();

            while (gameState != GameStates.QUIT)
            {
                switch (gameState)
                {
                    case GameStates.MAIN_MENU:
                        console.Menu(mainMenu);
                        break;
                    case GameStates.PLAY_MENU:
                        console.Menu(playMenu);
                        break;
                    case GameStates.NEW_GAME:
                        console.Print("Create Your Character", true);

                        //name
                        string name = console.GetString("Enter Your Name: ");

                        //choose gender
                        string gender = "";
                        Menu selectGender = new Menu
                            (
                            "Choose your gender:",
                            new MenuItem("male", () => gender = "male"),
                            new MenuItem("female", () => gender = "female")
                            );
                        console.Menu(selectGender);

                        player = personBuilder
                            .WithAttributes(human)
                            .WithAttribute("name", name)
                            .WithAttribute("gender", gender)
                            .TryBuild();

                        if (player == null)
                        {
                            console.Print("Error character could not be created!", true);
                            break;
                        }

                        console.Print("Roll Stats", true);
                        //roll stats
                        Dictionary<string, Stat> stats = player.FilterAttributesByType<Stat>();
                        do
                        {
                            foreach (var stat in stats)
                            {
                                stat.Value.RollStat(player.Die);
                                console.Print($"{stat.Key}: {stat.Value}", true);
                            }
                        }
                        while (console.YesNo("Reroll?"));

                        Story();
                        gameState = GameStates.WORLD;
                        break;
                    case GameStates.LOAD_GAME:
                        gameState = GameStates.WORLD;
                        break;
                    case GameStates.WORLD:
                        console.Menu(worldMenu);
                        break;
                    case GameStates.ENCOUNTER:
                        List<Person> people = new List<Person>();
                        //initiative
                        int[] initiatives = new int[people.Count];
                        //roll for each Person
                        for (int i = 0; i < people.Count; i++)
                        {
                            initiatives[i] = people[i].GetAttributeValue<Stat>("speed").StatCheck(people[i].Die);
                        }

                        // Use LINQ to order people array by the corresponding number in initiatives
                        Person[] orderedPeople = people
                            .Zip(initiatives, (person, initiative) => new { Person = person, Initiative = initiative })
                            .OrderBy(item => item.Initiative)
                            .Select(item => item.Person)
                            .ToArray();

                        int turn = 0;

                        //List<Menu> encounterMenus = new List<Menu>();
                        Menu encounterMenu;

                        //turn based loop
                        while (true)
                        {
                            //build encounter menu
                            encounterMenu = new Menu("What will you do?");
                            console.Print(encounterMenu.title);
                            //orderedPeople[turn].Brain.MakeChoice(encounterMenu);

                            turn = turn < orderedPeople.Length ? turn++ : 0;
                            break;
                        }
                        gameState = GameStates.WORLD;
                        break;
                    default:
                        break;
                }
            }

            if (!DEBUG)
                Outro();
        }

        public static void Setup()
        {
            SetupMenues();
            SetupNameGenerators();
            SetupSpeciesTemplates();
            SetupItemTemplates();
            SetupPlaceTemplates();
            SetupMap();
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

        public static void SetupMenues()
        {
            mainMenu = new Menu
                (
                "Main Menu",
                new MenuItem("Play", () => gameState = GameStates.PLAY_MENU),
                new MenuItem("Options", () => gameState = GameStates.OPTIONS),
                new MenuItem("Quit", () => gameState = GameStates.QUIT)
                );

            playMenu = new Menu
                (
                "Play Menu",
                new MenuItem("New Game", () => gameState = GameStates.NEW_GAME),
                new MenuItem("Load Game", () => gameState = GameStates.LOAD_GAME),
                new MenuItem("Main Menu", () => gameState = GameStates.MAIN_MENU)
                );

            worldMenu = new Menu
                (
                "What would you like to do?",
                     new MenuItem("Travel", () => console.Menu(travelMenu)),
                     new MenuItem("Camp", () => gameState = GameStates.QUIT),
                     new MenuItem("Look", () => gameState = GameStates.QUIT),
                     new MenuItem("Character", () => gameState = GameStates.QUIT),
                     new MenuItem("Pick up", () => gameState = GameStates.QUIT),//dynamic?
                     new MenuItem("Search", () => gameState = GameStates.MAIN_MENU),
                     new MenuItem("File", () => gameState = GameStates.QUIT),
                     new MenuItem("Help", () => gameState = GameStates.QUIT)
                );

            travelMenu = new Menu//this one has to be dynamic ? could keep the same. add distance?
                (
                "Travel",
                new MenuItem("Ice Mountain", () => console.Menu(worldMenu)),
                new MenuItem("Cave", () => console.Menu(worldMenu)),
                new MenuItem("Lair", () => console.Menu(worldMenu)),
                new MenuItem("Witch Doctor", () => console.Menu(worldMenu)),
                new MenuItem("Burned Village", () => console.Menu(worldMenu)),
                new MenuItem("Plainsville", () => console.Menu(worldMenu)),
                new MenuItem("Rogue's Den", () => console.Menu(worldMenu)),
                new MenuItem("Townopolus", () => console.Menu(worldMenu)),
                new MenuItem("Next", () => console.Menu(travelMenu2)),
                new MenuItem("Back", () => console.Menu(worldMenu))
                );

            travelMenu2 = new Menu//this one has to be dynamic ? could keep the same. add distance?
                (
                "Travel",
                new MenuItem("Sandland", () => console.Menu(worldMenu)),
                new MenuItem("Pyramid", () => console.Menu(worldMenu)),
                new MenuItem("Prev", () => console.Menu(travelMenu)),
                new MenuItem("Back", () => console.Menu(worldMenu))
                );

            characterMenu = new Menu//this one has to be dynamic
                (
                "Character",
                new MenuItem("Status", () => console.Menu(inventory)),
                new MenuItem("Inventory", () => console.Menu(inventory)),
                new MenuItem("Back", () => console.Menu(worldMenu))
                );

            inventory = new Menu
                (
                "Inventory",
                new MenuItem("Examine", () => console.Menu(worldMenu)),
                new MenuItem("Equip", () => console.Menu(worldMenu)),
                new MenuItem("Repair", () => console.Menu(worldMenu)),
                new MenuItem("Use", () => console.Menu(worldMenu)),
                new MenuItem("Drop", () => console.Menu(worldMenu)),
                new MenuItem("Back", () => console.Menu(worldMenu))
                );

            encounterMenu = new Menu//this one has to be dynamic
                (
                "Encounter"
                );

            fileMenu = new Menu
                (
                "File Menu",
                new MenuItem("Save Game", () => gameState = GameStates.NEW_GAME),
                new MenuItem("Load Game", () => gameState = GameStates.LOAD_GAME),
                new MenuItem("Options", () => gameState = GameStates.LOAD_GAME),
                new MenuItem("Quit Game", () => gameState = GameStates.MAIN_MENU)
                );
        }

        public static void BuildEncounterMenu()
        {

        }

        public static void BuildTravelMenu()//?
        {

        }

        public static void SetupNameGenerators()
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

        public static void SetupSpeciesTemplates()
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
            Thing sword = thingBuilder
                .TryBuild();
        }

        public static void SetupPlaceTemplates()
        {
        }

        public static void SetupMap()
        {
            mapKey.AddKey('~', waterCell);

            worldMap.CreateMatrixFromString(
            mapKey,
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

        public static void Intro()
        {
            Console.Clear();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            console.Type("                    /\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\", true, 1);
            console.Type("                    \\/\\\\\\///////////", true, 1);
            console.Type("                     \\/\\\\\\               /\\\\\\\\\\\\\\\\\\   /\\\\\\", true, 1);
            console.Type("                      \\/\\\\\\\\\\\\\\\\\\\\\\      /\\\\\\/////\\\\\\ \\///      /\\\\\\\\\\\\\\\\", true, 1);
            console.Type("                       \\/\\\\\\///////      \\/\\\\\\\\\\\\\\\\\\\\   /\\\\\\   /\\\\\\//////", true, 1);
            console.Type("                        \\/\\\\\\             \\/\\\\\\//////   \\/\\\\\\  /\\\\\\", true, 1);
            console.Type("                         \\/\\\\\\             \\/\\\\\\         \\/\\\\\\ \\//\\\\\\", true, 1);
            console.Type("       /\\\\\\\\\\\\\\\\\\\\\\\\\\     \\/\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\ \\/\\\\\\         \\/\\\\\\  \\///\\\\\\\\\\\\\\\\", true, 1);
            console.Type("       \\/\\\\\\/////////\\\\\\   \\///////////////  \\///          \\///     \\////////", true, 1);
            console.Type("        \\/\\\\\\       \\/\\\\\\", true, 1);
            console.Type("         \\/\\\\\\\\\\\\\\\\\\\\\\\\\\/   /\\\\/\\\\\\\\\\\\\\      /\\\\\\\\\\     /\\\\\\\\\\\\\\\\\\\\     /\\\\\\\\\\\\\\\\", true, 1);
            console.Type("          \\/\\\\\\/////////    \\/\\\\\\/////\\\\\\   /\\\\\\///\\\\\\  \\/\\\\\\//////    /\\\\\\/////\\\\\\", true, 1);
            console.Type("           \\/\\\\\\             \\/\\\\\\   \\///   /\\\\\\  \\//\\\\\\ \\/\\\\\\\\\\\\\\\\\\\\  /\\\\\\\\\\\\\\\\\\\\\\", true, 1);
            console.Type("            \\/\\\\\\             \\/\\\\\\         \\//\\\\\\  /\\\\\\  \\////////\\\\\\ \\//\\\\///////", true, 1);
            console.Type("             \\/\\\\\\             \\/\\\\\\          \\///\\\\\\\\\\/    /\\\\\\\\\\\\\\\\\\\\  \\//\\\\\\\\\\\\\\\\\\\\", true, 1);
            console.Type("              \\///              \\///             \\/////     \\//////////    \\//////////", true, 1);
            console.Type("\n                 REDUX!", false, textSpeed);
            Thread.Sleep(500);
            Console.ForegroundColor = ConsoleColor.Gray;
            console.Type("\n                                    By Matt Timmermans", true, textSpeed);
            Thread.Sleep(500);
            console.Anykey();
        }

        public static void Story()
        {
            console.ClearScreen();
            Console.WriteLine("STORY");
            Console.WriteLine("\n<you can skip ahead at anytime by pressing any key>");
            console.Type("\n\tYou live in a small peaceful village, in the land of Epica. One day", true, textSpeed);
            console.Type("you are out exploring. After hiking for sometime, you stop to rest your feet.", true, textSpeed);
            console.Type("You awake it is dark, black clouds are in the sky. You hurry back home to", true, textSpeed);
            console.Type("shelter. As you get closer you see light coming from ahead. You realize it's", true, textSpeed);
            console.Type("not storm clouds, it's smoke! Your entire village is in flames!", true, textSpeed);
            console.Type("By the time you reach your village all that is left is burning rubble and ash.", true, textSpeed);
            console.Type("No survivors, nothing. The only thing that could have caused such destruction", true, textSpeed);
            console.Type("is a dragon! Watching the burning embers of what was once your home, you vow", true, textSpeed);
            console.Type("to avenge them, you will slay the dragon!", true, textSpeed);
            console.Type("...", true, textSpeed * 5);
            console.Anykey();

            console.ClearScreen();
            console.Type("\n\"What a tragity!\" a small voice says, you turn around to see a tiny man walk out from behind a tree.", true, textSpeed);
            console.Type("\"I'm Harold nice to meet you\" he says extending his tiny arm, you bend over and give him a finger to shake.", true, textSpeed);
            console.Type("\"Are you new to the land of Epica?\" (Y/N)\n", true, textSpeed);
            if (console.YesNo())
            {
                console.Type("\"Welcome to the land of Epica. You can go to The Forest, The Town, The Caves,", true, textSpeed);
                console.Type("The Ice Mountain and The Dragon's Lair. The weaker monsters are near the forest", true, textSpeed);
                console.Type("and town. If you need some help just press H. You should look around the area", true, textSpeed);
                console.Type("before you go there's got to be a weapon laying around here somewhere.", true, textSpeed);
                console.Type("Make sure to equip it before battle in the character menu.\n", true, textSpeed);
                //noob = true;
            }
            else
            {
                //noob = false;
            }
            console.Anykey();
        }

        static void Help()
        {
            Console.WriteLine("\nHELP");
            Console.WriteLine("~~~~");
            Console.WriteLine("Epic Prose SE now uses a menu system for most input, but in the");
            Console.WriteLine("cases where you need to type in an input a list is provided.");
            Console.WriteLine("Using the up and down arrows you can scroll through previous input.");
            console.Anykey();
            Console.WriteLine("\nSTATS");
            Console.WriteLine("~~~~~");
            Console.WriteLine("Str - Strength - how much damage you do when you attack.");
            Console.WriteLine("Vit - Vitality - how much max health you have.");
            Console.WriteLine("Dex - Dexterity - your accuracy with fighting.");
            Console.WriteLine("Spe - Speed - running away, dodging and travel speed.");
            Console.WriteLine("Int - Intelligence - used for talking your way out of fights.");
            Console.WriteLine("Cha - Charisma - How liked you are, effects prices and things.");
            console.Anykey();
            Console.WriteLine("\nBASE MENU");
            Console.WriteLine("~~~~~~~~~");
            Console.WriteLine("GO - shows you a list of locations with the distance away from you, you will be asked");
            Console.WriteLine("where you want to go, then type one of the options and you will travel in that direction");
            Console.WriteLine("until you reach it, encounter a monster or press a key. After a battle you may not be at");
            Console.WriteLine("your destination, just repeat the process.");
            Console.WriteLine("LOOK - gives you a description of your surroundings and any people or items around");
            console.Anykey();
            Console.WriteLine("\nCONVERSATIONS");
            Console.WriteLine("~~~~~~~~~~~~~");
            Console.WriteLine("PRICES -> see a list and prices of what's for sale");
            Console.WriteLine("BUY <item name> -> buy an item");
            Console.WriteLine("SELL <item name> -> sell an item");
            Console.WriteLine("LEAVE -> leave the conversation.");
            Console.WriteLine("TRAIN -> they will tell you what they can train you in an for how much.");
            Console.WriteLine("TRAIN <stat> -> they will train the named stat for a price.");
            console.Anykey();
            Console.WriteLine("\nMONSTERS");
            Console.WriteLine("~~~~~~~~");
            Console.WriteLine("Here is a list of terrain types and the types of monsters found there:");
            Console.WriteLine("grassland easy -slime, goblin, wolf, bandit");
            Console.WriteLine("grassland hard - briggand, hobgoblin, bugbear, ogre");
            Console.WriteLine("forest easy - slime, goblin, wolf, bandit");
            Console.WriteLine("forest hard - briggand, hobgoblin, bugbear, troll");
            Console.WriteLine("swamp easy - slime, goblin, wyvern, hobgoblin");
            Console.WriteLine("swamp hard - troll, wyvern, hobgoblin, bugbear");
            Console.WriteLine("rocky easy - bandit, skeleton, briggand, goblin");
            Console.WriteLine("rocky hard - ogre, manticore, troll, skeleton warrior");
            Console.WriteLine("desert easy -skeleton, jackal, bandit, briggand");
            Console.WriteLine("desert hard - mummy, skeleton warrior, manticore, briggand");
            console.Anykey();
        }

        static void Options()
        {
            Console.Clear();
            Console.WriteLine("OPTIONS");
            //Console.WriteLine("\nGame options will be configured here. Text speed");
            Console.WriteLine("The current buffer height is {0} rows.", Console.BufferHeight);
            Console.WriteLine("The current buffer width is {0} columns.", Console.BufferWidth);
            Console.Write("\nEnter text speed (fast) 0 - 5 (slow): ");
            int input;
            do
            {
                while (Console.KeyAvailable) // Flushes the input queue.
                    Console.ReadKey();

                //input = Console.Read();
                try
                {
                    input = Int32.Parse(Console.ReadLine());
                }
                catch (FormatException e)
                {
                    input = 3;
                    Console.WriteLine(e.Message);
                }
                //Console.WriteLine(); // Breaks the line.
                Console.WriteLine("\nInput: " + input);
            }
            while (input != 0 && input != 1 && input != 2 && input != 3 && input != 4 && input != 5);
            //SetTextSpeed(input * 20);
            textSpeed = input * 20;
            Console.WriteLine("\n");
            console.Type("Text speed set, speed test in now process", false, textSpeed);
            console.Type("...", false, textSpeed * 5);
            //Console.WriteLine("\nText speed: " + textSpeed);

            //Show Rolls
            Console.Write("\nShow dice rolls(Y/N): ");
            showRolls = console.YesNo();

            //Show Enemy Health
            Console.Write("\nShow enemy health(Y/N): ");
            showEnemyHealth = console.YesNo();

            //Sound
            Console.Write("\nSound on(Y/N): ");
            sound = console.YesNo();

            console.Anykey();
            //next option
        }

        static void About()
        {
            Console.Clear();
            Console.WriteLine("ABOUT");
            Console.WriteLine("~~~~~");
            Console.WriteLine("\nThis game was created by Matt Timmermans a C# remake of Epic Prose. Epic Prose was made by Matt Timmermans"
            + "\nin 2016 for a school project. This version contains all the things there was not enough time to add in the\noriginal.");
            Console.WriteLine("\tThe rules for Epic Prose are based on a system I came up for a dice");
            Console.WriteLine("and paper RPG, I wanted a system that was simple, versitile and expandable.");
            Console.WriteLine("Basically you have attributes that go up to 9, each action/reaction is governed by");
            Console.WriteLine("one attribute. To succeed in using an action you must roll your attribute score or");
            Console.WriteLine("lower with a d10. Rolling your exact attibute is a critical hit. When you successfully");
            Console.WriteLine("use a skill you get a fractional increase to the governing stat.");
            console.Anykey();
        }

        static void GameOver()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("                /\\\\\\\\\\\\\\\\\\\\\\\\                                                                                          ");
            Console.WriteLine("               /\\\\\\//////////                                                                                          ");
            Console.WriteLine("               /\\\\\\                                                                                                    ");
            Console.WriteLine("               \\/\\\\\\    /\\\\\\\\\\\\\\  /\\\\\\\\\\\\\\\\\\       /\\\\\\\\\\  /\\\\\\\\\\       /\\\\\\\\\\\\\\\\                                      ");
            Console.WriteLine("                \\/\\\\\\   \\/////\\\\\\ \\////////\\\\\\    /\\\\\\///\\\\\\\\\\///\\\\\\   /\\\\\\/////\\\\\\                                    ");
            Console.WriteLine("                 \\/\\\\\\       \\/\\\\\\   /\\\\\\\\\\\\\\\\\\\\  \\/\\\\\\ \\//\\\\\\  \\/\\\\\\  /\\\\\\\\\\\\\\\\\\\\\\                                    ");
            Console.WriteLine("                  \\/\\\\\\       \\/\\\\\\  /\\\\\\/////\\\\\\  \\/\\\\\\  \\/\\\\\\  \\/\\\\\\ \\//\\\\///////                                    ");
            Console.WriteLine("                   \\//\\\\\\\\\\\\\\\\\\\\\\\\/  \\//\\\\\\\\\\\\\\\\/\\\\ \\/\\\\\\  \\/\\\\\\  \\/\\\\\\  \\//\\\\\\\\\\\\\\\\\\\\                                 ");
            Console.WriteLine("                     \\////////////     \\////////\\//  \\///   \\///   \\///    \\//////////                                 ");
            Console.WriteLine("                                     /\\\\\\\\\\                                                                            ");
            Console.WriteLine("                                    /\\\\\\///\\\\\\                                                                         ");
            Console.WriteLine("                                   /\\\\\\/  \\///\\\\\\                                                                      ");
            Console.WriteLine("                                   /\\\\\\      \\//\\\\\\  /\\\\\\    /\\\\\\     /\\\\\\\\\\\\\\\\   /\\\\/\\\\\\\\\\\\\\                          ");
            Console.WriteLine("                                   \\/\\\\\\       \\/\\\\\\ \\//\\\\\\  /\\\\\\    /\\\\\\/////\\\\\\ \\/\\\\\\/////\\\\\\                        ");
            Console.WriteLine("                                    \\//\\\\\\      /\\\\\\   \\//\\\\\\/\\\\\\    /\\\\\\\\\\\\\\\\\\\\\\  \\/\\\\\\   \\///                        ");
            Console.WriteLine("                                      \\///\\\\\\  /\\\\\\      \\//\\\\\\\\\\    \\//\\\\///////   \\/\\\\\\                              ");
            Console.WriteLine("                                         \\///\\\\\\\\\\/        \\//\\\\\\      \\//\\\\\\\\\\\\\\\\\\\\ \\/\\\\\\                             ");
            Console.WriteLine("                                            \\/////           \\///        \\//////////  \\///                             ");
            Console.ForegroundColor = ConsoleColor.Gray;
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
            console.Anykey();
        }

        public static void Outro()
        {
            console.Type("Thank you forplaying Epic Prose Redux");
            console.Anykey();
        }
    }
}
/*
public class Species
    {
        /*public enum Diet
        {
            HERBIVORE,
            CARNIVORE,
            OMNIVORE
        }

        public enum SleepCycle
        {
            DIURNAL, //active during the day and sleeping at night
            NOCTURNAL, //active at night and sleeping during the day
            CREPUSCULAR, //active during twilight (dawn and dusk)
            MATUTINAL, //early-rising and active in the morning
            VESPERTINE, //active in the evening or just after sunset
        }

        public static List<SleepCycle> GetActiveSleepCycles(DateTime time)
        {
            List<SleepCycle> activeCycles = new List<SleepCycle>();

            // Check if time falls within each sleep cycle's range
            if (time.TimeOfDay >= new TimeSpan(6, 0, 0) && time.TimeOfDay < new TimeSpan(18, 0, 0))
            {
                activeCycles.Add(SleepCycle.DIURNAL);
            }
            if (time.TimeOfDay >= new TimeSpan(18, 0, 0) || time.TimeOfDay < new TimeSpan(6, 0, 0))
            {
                activeCycles.Add(SleepCycle.NOCTURNAL);
            }
            if (time.TimeOfDay >= new TimeSpan(5, 0, 0) && time.TimeOfDay < new TimeSpan(8, 0, 0) ||
                time.TimeOfDay >= new TimeSpan(17, 0, 0) && time.TimeOfDay < new TimeSpan(20, 0, 0))
            {
                activeCycles.Add(SleepCycle.CREPUSCULAR);
            }
            if (time.TimeOfDay >= new TimeSpan(4, 0, 0) && time.TimeOfDay < new TimeSpan(7, 0, 0))
            {
                activeCycles.Add(SleepCycle.MATUTINAL);
            }
            if (time.TimeOfDay >= new TimeSpan(19, 0, 0) && time.TimeOfDay < new TimeSpan(22, 0, 0))
            {
                activeCycles.Add(SleepCycle.VESPERTINE);
            }

            return activeCycles;
        }

        public readonly string name;
        //lifespan
        public readonly int lifeExpectancy;
        public readonly Diet diet;
        public readonly SleepCycle sleepCycle;
        public readonly Tuple<int, int> grouping;
        public readonly List<Cell> habitat;
        public readonly int rarity;
        //stats
        //have a min and max for character generation
        //str, vit, dex, spe, tel, cha;
        public readonly Tuple<int, int> strengthRange;
        public readonly Tuple<int, int> vitalityRange;
        public readonly Tuple<int, int> dexterityRange;
        public readonly Tuple<int, int> speedRange;
        public readonly Tuple<int, int> intelligenceRange;
        public readonly Tuple<int, int> charismaRange;

        public readonly float regenRate;//regen per hour /60 in battle add only whole numbers, constitution may add to regen rate

        public readonly int alignment;

        public Body body;

        public Species(string p_name, int p_lifeExpectancy, Diet p_diet, SleepCycle p_sleepCycle,
            Tuple<int, int> p_grouping, List<Cell> p_habitat, int p_rarity, int p_alignment, Body p_body,
            float p_regen, params int[] p_stats)
        {
            if (p_stats == null || p_stats.Length != 12)
            {
                throw new ArgumentException("Array parameter must have a length of 12");
            }
            name = p_name;
            lifeExpectancy = p_lifeExpectancy;
            grouping = p_grouping;
            diet = p_diet;
            sleepCycle = p_sleepCycle;
            body = p_body;
            //set stat ranges
            strengthRange = new Tuple<int, int>(p_stats[0], p_stats[1]);
            vitalityRange = new Tuple<int, int>(p_stats[2], p_stats[3]);
            dexterityRange = new Tuple<int, int>(p_stats[4], p_stats[5]);
            speedRange = new Tuple<int, int>(p_stats[6], p_stats[7]);
            intelligenceRange = new Tuple<int, int>(p_stats[8], p_stats[9]);
            charismaRange = new Tuple<int, int>(p_stats[10], p_stats[11]);
            habitat = p_habitat;
            rarity = p_rarity;
            alignment = p_alignment;
            regenRate = p_regen;
        }
    }
    public class Ai : Brain
    {
        //utility weights
        public Dictionary<Utility, float> Priorities { get; }
        //relationships
        public Dictionary<Person, float> relationships;//friend/foe        

        public Ai()
        {
            //var utilities = person.FilterAttributesByType<Utility>();
        }
        public override void MakeChoice(Menu menu)
        {
            //use Priorities, relationships to make choice
            throw new NotImplementedException();
        }
    }
    People
        //Dictionary<string, Stat> stats;//???
        public Die Die { get; private set; }
        //public Brain Brain { get; private set; }
        //?
        //public float Alignment { get; set; }//good/neutral/evil
        public Person Father { get; }
        public Person Mother { get; }
        public Species species { get; }
        public enum Gender { male, female }
        public Gender gender { get; }
        public DateTime birthdate { get; }
        public DateTime deathdate { get; private set; }
        public Place birthPlace { get; }
        //public int Age { get { return (int)((Program.world.GetDateTime() - birthdate).TotalDays / 365.2425); } }
        public List<Stat> stats = new List<Stat>();
        public int alignment = 0;
        public int health;
        public int MaxHealth => (int)stats[(int)Stats.Vitality].value * 10;
        public Dictionary<string, StatusEffect> statusEffects = new Dictionary<string, StatusEffect>();
*/
