using System;
using System.Collections.Generic;

namespace TextAdventureLibrary
{
    public class MenuItem
    {
        public string Text { get; private set; }
        public Action OnSelected { get; private set; }
        public List<string> Tags { get; private set; }

        public MenuItem(string text, Action action, params string[] tags)
        {
            Text = text;
            OnSelected = action;
            Tags = new List<string>(tags);
        }
    }
}
