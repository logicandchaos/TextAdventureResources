using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// This class takes in actions and creates a Menu
/// </summary>

namespace TextAdventureLibrary
{
    public class MultiMenuBuilder
    {
        string name;
        List<MenuItem> menuItems = new List<MenuItem>();

        public MultiMenuBuilder WithName(string name)
        {
            this.name = name;
            return this;
        }

        public MultiMenuBuilder WithItem(MenuItem menuItem)
        {
            menuItems.Add(menuItem);
            return this;
        }

        public MultiMenuBuilder WithItems(params MenuItem[] menuItems)
        {
            this.menuItems.AddRange(menuItems);
            return this;
        }

        public List<Menu> Build()
        {
            List<Menu> menus = new List<Menu>();

            if (menuItems.Count > 10)
            {
                int numMenus = menuItems.Count / 8;
                if (menuItems.Count % 8 != 0)
                    numMenus++;

                List<MenuItem> items;

                for (int i = 0; i < numMenus; i++)
                {
                    items = new List<MenuItem>();
                    menus.Add(new Menu(name));
                    for (int j = 0; j < 8; j++)
                    {
                        items.Add(menuItems[i + j]);
                    }
                    //TODO
                    //need to add proper Action to link the menues together
                    items.Add(new MenuItem("prev", () => { }));
                    items.Add(new MenuItem("next", () => { }));
                    menus[i].Items = items.ToArray();
                }
            }
            else
            {
                menus[0] = new Menu(name);
                menus[0].Items = menuItems.ToArray();
            }

            return menus;
        }
    }
}
