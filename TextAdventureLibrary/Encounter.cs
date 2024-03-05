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
        Terminal terminal;
        int turn = 0;
        string story;
        MultiMenuBuilder menuBuilder;
        World world;

        public Encounter(Terminal terminal, World world, Action<List<Person>> Sort = null, params Person[] people)
        {
            if (people.Length < 2)
                return;
            this.terminal = terminal;
            People.AddRange(people);
            Sort?.Invoke(People);
            Run();
            this.world = world;
        }

        void Run()
        {
            while (true)
            {

                menuBuilder = new MultiMenuBuilder();
                foreach (var action in People[turn].FilterAttributesByType<Action>())
                {
                    menuBuilder.WithItem(new MenuItem(action.Key, action.Value));
                }
                encounterMenus = menuBuilder.Build();
                CurrentMenu = encounterMenus[0];
                terminal.Print(CurrentMenu);
                Menu prevMenu = CurrentMenu;
                SelectedMenu = CurrentMenu;
                CurrentMenu.SelectOption(People[turn].GetAttributeValue<Brain>("Brain").Choose());
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
                    CurrentMenu.SelectOption(People[turn].GetAttributeValue<Brain>("Brain").Choose());
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
            world.AddHistoricalEvent(story);
        }
    }
}