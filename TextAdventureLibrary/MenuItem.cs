using System;
using System.Collections.Generic;
using System.Text;

namespace TextAdventureLibrary
{
    public class MenuItem
    {
        public string Text { get; private set; }
        public Action OnSelected { get; private set; }

        public MenuItem(string text, Action action)
        {
            Text = text;
            OnSelected = action;
        }
    }
}
