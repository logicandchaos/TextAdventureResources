using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// This class takes in actions and creates a Menu
/// </summary>

namespace TextAdventureLibrary
{
    public class MenuBuilder
    {
        Menu menu;

        public MenuBuilder WithItem(MenuItem menuItem)
        {
            if (menu.items.Length > 10)
                return this;

            return this;
        }

        public MenuBuilder WithItems(params MenuItem[] menuItem)
        {
            if (menu.items.Length > 10)
                return this;

            return this;
        }

        public Menu Builder()
        {
            return menu;
        }
    }
}
