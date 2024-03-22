using System;
using System.Threading;
using System.Runtime.InteropServices;
using TextAdventureLibrary;

/// <summary>
/// This is a remake of my Text Adventure Epic Prose to test out my TextAdventureLibrary
/// I might add a couple dungeons to the game, for the boss locations.
/// </summary>

//TODO:
//encounter menu building
//Ai
//build characters and player

//Predicate<Person> hasSwordEquiped = person => person.GetAttributeValue<Thing>("equipedItem") == sword;

//PersonBuilder personBuilder = new PersonBuilder();
//Person character = personBuilder
//    .WithAttributes(human)
//    .Build();

namespace EpicProseRedux
{
    public class Program
    {
        /// <summary>
        /// This code if for the console fullscreen mode
        /// </summary>
        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();
        private static IntPtr ThisConsole = GetConsoleWindow();
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        private const int HIDE = 0;
        private const int MAXIMIZE = 3;
        private const int MINIMIZE = 6;
        private const int RESTORE = 9;

        //Game Settings
        public static int textSpeed = 20;
        public static bool showRolls;
        public static bool showEnemyHealth;
        public static bool sound;

        public static CsConsole console = new CsConsole();

        //items should have attributes for what you can do with them. So like Equip: hand, head, Throwable? Consumable? etc..        

        //might not use templates for bosses and just make the characters with factory
        //static Template iceGiant = new Template("ice giant");
        //king of thieves - human
        //static Template dragon = new Template("dragon");

        /*public static Builder<Person> personBuilder = new Builder<Person>(
            "name",
            "die",
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
            );*/

        const bool DEBUG = true;
        static bool isRunning = true;

        static void Main(string[] args)
        {
            //Set full screen
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            ShowWindow(ThisConsole, MAXIMIZE);

            if (!DEBUG)
                Intro();

            Game game = null;

            Menu mainMenu = new Menu("Main Menu",
                new MenuItem("New Game", () => { game = NewGame(); }),
                new MenuItem("Load Game", () => { game = LoadGame(); }),
                new MenuItem("Settings", () => { Options(); }),
                new MenuItem("About", () => { About(); }),
                new MenuItem("Quit", () => { isRunning = false; })
                );

            while (isRunning)
            {
                if (game == null)
                {
                    console.Print(mainMenu);
                    mainMenu.SelectOption(console.GetDigit(mainMenu.Items.Length));
                    mainMenu.Execute();
                }
                else
                {
                    game.Play();
                }
            }

            if (!DEBUG)
                Outro();
        }

        public static void Intro()
        {
            console.ClearScreen();
            console.Print("\n");
            console.SetColor(Color.Green, Color.Black);
            console.Type("                    /\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\", 1);
            console.Type("\n                    \\/\\\\\\///////////", 1);
            console.Type("\n                     \\/\\\\\\               /\\\\\\\\\\\\\\\\\\   /\\\\\\", 1);
            console.Type("\n                      \\/\\\\\\\\\\\\\\\\\\\\\\      /\\\\\\/////\\\\\\ \\///      /\\\\\\\\\\\\\\\\", 1);
            console.Type("\n                       \\/\\\\\\///////      \\/\\\\\\\\\\\\\\\\\\\\   /\\\\\\   /\\\\\\//////", 1);
            console.Type("\n                        \\/\\\\\\             \\/\\\\\\//////   \\/\\\\\\  /\\\\\\", 1);
            console.Type("\n                         \\/\\\\\\             \\/\\\\\\         \\/\\\\\\ \\//\\\\\\", 1);
            console.Type("\n       /\\\\\\\\\\\\\\\\\\\\\\\\\\     \\/\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\ \\/\\\\\\         \\/\\\\\\  \\///\\\\\\\\\\\\\\\\", 1);
            console.Type("\n       \\/\\\\\\/////////\\\\\\   \\///////////////  \\///          \\///     \\////////", 1);
            console.Type("\n        \\/\\\\\\       \\/\\\\\\", 1);
            console.Type("\n         \\/\\\\\\\\\\\\\\\\\\\\\\\\\\/   /\\\\/\\\\\\\\\\\\\\      /\\\\\\\\\\     /\\\\\\\\\\\\\\\\\\\\     /\\\\\\\\\\\\\\\\", 1);
            console.Type("\n          \\/\\\\\\/////////    \\/\\\\\\/////\\\\\\   /\\\\\\///\\\\\\  \\/\\\\\\//////    /\\\\\\/////\\\\\\", 1);
            console.Type("\n           \\/\\\\\\             \\/\\\\\\   \\///   /\\\\\\  \\//\\\\\\ \\/\\\\\\\\\\\\\\\\\\\\  /\\\\\\\\\\\\\\\\\\\\\\", 1);
            console.Type("\n            \\/\\\\\\             \\/\\\\\\         \\//\\\\\\  /\\\\\\  \\////////\\\\\\ \\//\\\\///////", 1);
            console.Type("\n             \\/\\\\\\             \\/\\\\\\          \\///\\\\\\\\\\/    /\\\\\\\\\\\\\\\\\\\\  \\//\\\\\\\\\\\\\\\\\\\\", 1);
            console.Type("\n              \\///              \\///             \\/////     \\//////////    \\//////////", 1);
            console.Type("\n\n                 REDUX!", textSpeed);
            Thread.Sleep(500);
            console.ResetColor();
            console.Type("\n                                    By Matt Timmermans", textSpeed);
            Thread.Sleep(500);
            console.Anykey();
        }

        static void Options()
        {
            console.ClearScreen();
            console.Print("OPTIONS");
            console.Print("\n\nEnter text speed, (no delay) 0 (fast) 1 - 5 (slow): ");
            int input;
            do
            {
                input = console.GetDigit(10);
            }
            while (input != 0 && input != 1 && input != 2 && input != 3 && input != 4 && input != 5);
            console.Print("\nInput: " + input);
            textSpeed = input * 20;
            console.Print("\n");
            console.Type("Text speed set, speed test in now process", textSpeed);
            console.Type("...", textSpeed * 5);

            //Show Rolls
            console.Print("\nShow dice rolls(Y/N): ");
            showRolls = console.YesNo();

            //Show Enemy Health
            console.Print("\nShow enemy health(Y/N): ");
            showEnemyHealth = console.YesNo();

            //Sound
            console.Print("\nSound on(Y/N): ");
            sound = console.YesNo();

            console.Anykey();
            //next option
        }

        static void About()
        {
            console.ClearScreen();
            console.Print("ABOUT");
            console.Print("\n~~~~~");
            console.Print("\nThis game was created by Matt Timmermans a remake of Epic Prose SE using the");
            console.Print("\nTextAdventureLibrary also created by Matt Timmermans. This also serves as the");
            console.Print("\ntutorial for the TextAdventureLibrary.");
            console.Anykey();
        }

        public static Game NewGame()
        {
            return new Game();
        }

        public static Game LoadGame()
        {
            return Game.Load();
        }

        public static void Outro()
        {
            console.Type("Thank you for playing Epic Prose Redux");
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
