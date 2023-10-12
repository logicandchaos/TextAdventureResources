using System;
using System.Collections.Generic;
using System.Text;

namespace TextAdventureLibrary
{
    public class MapKey
    {
        public Dictionary<char, Cell> Key { get; private set; }

        public void AddKey(char key, Cell cell)
        {
            if (!Key.ContainsKey(key))
            {
                if (!Key.ContainsValue(cell))
                {
                    Key.Add(key, cell);
                }
            }
        }
    }
}
