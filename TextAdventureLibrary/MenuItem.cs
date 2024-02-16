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

    /*public class MenuItem<T1, T2> : MenuItemBase where T1 : Noun where T2 : Noun
    {
        public Action<T1, T2> OnSelectedWithParameters { get; set; }
        public T1 parameter1;
        public T2 parameter2;

        public MenuItem(string text, Action<T1, T2> action)
        {
            Text = text;
            OnSelectedWithParameters = action;
        }

        public override void Execute()
        {
            OnSelectedWithParameters?.Invoke(parameter1, parameter2);
        }
    }*/
}
