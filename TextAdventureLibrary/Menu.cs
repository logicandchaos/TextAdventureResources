using System;

//I need to make some sort of menu builder that uses a rule engine

namespace TextAdventureLibrary
{
    public class Menu
    {
        public string Title { get; set; }
        public MenuItem[] Items { get; set; }
        int selected;

        public Menu(string p_title, params MenuItem[] items)
        {
            if (items.Length > 10)
            {
                throw new ArgumentException("Too many menu options. Maximum allowed is 10.");
            }

            Title = p_title;
            Items = items;
        }

        public void SelectOption(int option)
        {
            if (option == 0)
                option = 10;
            else
                option--;//to start at 1

            //MenuItem selectedItem = Items[option];
            selected = option;
        }

        public void Execute()
        {
            Items[selected].OnSelected?.Invoke();
        }
    }

    /*public class Menu<T1, T2> where T1 : Noun where T2 : Noun
    {
        public string Title { get; set; }
        public MenuItem<T1, T2>[] Items { get; set; }
        //selectedItem.OnSelectedWithParameters(person1, person2);
        public Menu(string p_title, params MenuItem<T1, T2>[] items)
        {
            if (items.Length > 10)
            {
                throw new ArgumentException("Too many menu options. Maximum allowed is 10.");
            }

            Title = p_title;
            Items = items;
        }

        public void SelectOption(int option)
        {
            if (option == 0)
                option = 10;
            else
                option--;//to start at 1

            MenuItem<T1, T2> selectedItem = Items[option];

            if (selectedItem.OnSelectedWithParameters != null)
            {
                selectedItem.OnSelectedWithParameters?.Invoke(null, null);
            }
        }
    }*/
}