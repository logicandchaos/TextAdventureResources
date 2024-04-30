using System;
using System.Collections.Generic;
using System.Text;

namespace TextAdventureLibrary
{
    public class MapKey
    {
        public Dictionary<char, Cell> Key { get; private set; } = new Dictionary<char, Cell>();

        public MapKey() { }

        public MapKey(params Tuple<char, Cell>[] keys)
        {
            foreach (var key in keys)
            {
                AddKey(key.Item1, key.Item2);
            }
        }

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
