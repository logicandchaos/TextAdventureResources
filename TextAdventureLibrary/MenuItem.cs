using System;
using System.Collections.Generic;
using System.Text;

namespace TextAdventureLibrary
{
    public class MenuItem
    {
        public string Text { get; set; }
        public Action OnSelected { get; set; }
        //?
        //public float Alignment { get; set; }//good/neutral/evil

        public MenuItem(string text, Action action)
        {
            Text = text;
            OnSelected = action;
        }
    }
}
