using System;

namespace TextAdventureLibrary
{
    public class Map
    {
        public string Name { get; private set; }
        public string Info { get; private set; }
        public MapKey Key { get; private set; }
        public Cell[,] Matrix { get; private set; }

        public Map(string name, string info, MapKey key)
        {
            Name = name;
            Info = info;
            Key = key;
        }

        public Map(string name, string info, MapKey key, Cell[,] matrix)
        {
            Name = name;
            Info = info;
            Key = key;
            Matrix = matrix;
        }

        public void CreateMatrixFromString(params string[] rows)
        {
            {
                int numRows = rows.Length;
                int numCols = rows[0].Length;

                for (int i = 1; i < numRows; i++)
                {
                    if (rows[i].Length != numCols)
                    {
                        throw new ArgumentException("All strings must be equal length.");
                    }
                }

                Matrix = new Cell[numCols, numRows];

                for (int y = 0; y < numRows; y++)
                {
                    for (int x = 0; x < numCols; x++)
                    {
                        if (Key.Key.TryGetValue(rows[y][x], out var cell))
                        {
                            Matrix[x, y] = cell;
                        }
                    }
                }
            }
        }

        public void RandomMapWalker() { }
        public void RandomMapNoise() { }
    }
}
