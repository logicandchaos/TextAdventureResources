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

        Map worldMap;

        bool DEBUG = true;

        public Game()
        {
            Program.console.Print("Creating New Game\n");
            ChangeState(GameStates.SETUP);
        }

        public void Setup()
        {
            Program.console.Print("Configuring Game\n");
            SetupItemTemplates();
            SetupPlaces();
            worldMap = new Map();
            worldMap.SetupMap();
            Program.console.Print("Game Configured\n");
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
                            .WithAttribute("name", name)
                            .WithAttribute("gender", gender)
                            .TryBuild();

                        if (player == null)
                        {
                            Program.console.Print("Error character could not be created!");
                            ChangeState(GameStates.QUIT);
                            break;
                        }

                        Program.console.Print("\nRoll Stats");
                        do
                        {
                            ChacterCreator.RollStats(player);
                        }
                        while (Program.console.YesNo("Reroll?"));

                        Story1();
                        ChangeState(GameStates.PLACE);
                        break;
                    case GameStates.PLACE:
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
                        ChangeState(GameStates.WORLDMAP);
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

        public void ChangeState(GameStates gameState)
        {
            prevGameState = this.gameState;
            this.gameState = gameState;
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

        Place iceMountain;
        Place dwarfCave;
        Place dragonLair;
        Place witchDoctorHut;
        Place burnedVillage;
        Place plainsville;
        Place roguesDen;
        Place townopolus;
        Place sandland;
        Place pyramid;

        public static void SetupPlaces()
        {

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