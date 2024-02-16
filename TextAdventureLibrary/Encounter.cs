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
        public Menu SelectedMenu { get; private set; }
        public World World { get; private set; }
        Terminal terminal;
        int turn = 0;
        string story;
        MultiMenuBuilder menuBuilder;

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
                foreach (Action action in ActionSystem.GetAvailableActions(People[turn], World))
                {
                    //menuItems.Add(new MenuItem<Person, Person>("",action));
                    menuBuilder = new MultiMenuBuilder();
                    menuBuilder.WithItem(new MenuItem("", action));
                }
                //encounterMenus = menuBuilder.WithItems(menuItems.ToArray()).Build();
                encounterMenus = menuBuilder.Build();
                CurrentMenu = encounterMenus[0];
                terminal.Print(CurrentMenu);
                Menu prevMenu = CurrentMenu;
                SelectedMenu = CurrentMenu;
                CurrentMenu.SelectOption(DecisionSystem.Decide(People[turn], this));
                if (CurrentMenu == prevMenu)
                {
                    menuBuilder = new MultiMenuBuilder().WithName("Select Target");
                    MenuItem menuItem;
                    foreach (Person p in People)
                    {
                        menuItem = new MenuItem(p.GetAttributeValue<String>("name"), () => People[turn].AddOrSetAttribute("target", p));
                        menuBuilder.WithItem(menuItem);
                    }
                    List<Menu> personSelection = menuBuilder.Build();
                    CurrentMenu = personSelection[0];
                    terminal.Print(CurrentMenu);
                    CurrentMenu.SelectOption(DecisionSystem.Decide(People[turn], this));
                    SelectedMenu.Execute();
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