using System;
using System.Collections.Generic;
using System.Text;

namespace TextAdventureLibrary
{
    public abstract class MenuItemBase
    {
        public string Text { get; set; }
        public abstract void Execute();
    }
}
