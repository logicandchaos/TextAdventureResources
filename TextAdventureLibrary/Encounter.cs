using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// Encounter is for interactions between 2 or more characters, incuding battles and trading among other things.
/// </summary>

namespace TextAdventureLibrary
{
    public class Encounter
    {
        public List<Person> People { get; private set; }
        List<Menu> encounterMenus = new List<Menu>();
        public Menu CurrentMenu { get; private set; }
        public World World { get; private set; }
        Terminal terminal;
        int turn = 0;
        string story;
        MultiMenuBuilder menuBuilder = new MultiMenuBuilder();

        public Encounter(Terminal terminal, World world, Action<List<Person>> Sort = null, params Person[] people)
        {
            if (people.Length < 2)
                return;
            World = world;
            this.terminal = terminal;
            People.AddRange(people);
            Sort?.Invoke(People);
            Run();
        }

        void Run()
        {
            while (true)
            {
                //build menu with ActionSystem
                List<MenuItem> menuItems = new List<MenuItem>();
                foreach (var action in ActionSystem.GetAvailableActions(People[turn], World))
                {

                }
                encounterMenus = menuBuilder.WithItem();
                //encounterMenus=
                //set current menu
                //currentMenu=encounterMenus[0];
                terminal.Print(CurrentMenu);
                Menu prevMenu = CurrentMenu;
                DecisionSystem.Decide(People[turn], this);
                if (CurrentMenu == prevMenu)
                {
                    story += "";
                    turn++;
                    if (turn > People.Count)
                        turn = 0;
                }
                else
                {

                }
                //test
                break;
            }
            //results
            World.AddHistoricalEvent(story);
        }
    }
}
