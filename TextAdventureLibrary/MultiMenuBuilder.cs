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
        List<Menu> menus;

        public MultiMenuBuilder WithItem(MenuItem menuItem)
        {
            return this;
        }

        public MultiMenuBuilder WithItems(params MenuItem[] menuItem)
        {
            return this;
        }

        public List<Menu> Build()
        {
            //this is where the menu items will be split into different menus
            return menus;
        }
    }
}
