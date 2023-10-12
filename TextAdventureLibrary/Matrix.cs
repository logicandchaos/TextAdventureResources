using System;

namespace TextAdventureLibrary
{
    public class Matrix
    {
        public string Name { get; private set; }
        public string Info { get; private set; }
        public Cell[,] matrix;

        public Matrix(string name, string info, Cell[,] matrix)
        {
            Name = name;
            Info = info;
            this.matrix = matrix;
        }

        public Matrix(string name, string info)
        {
            Name = name;
            Info = info;
        }

        public void CreateMatrixFromString(MapKey key, params string[] rows)
        {
            int length = rows[0].Length;
            foreach (string s in rows)
            {
                if (s.Length != length)
                {
                    throw new ArgumentException("All strings must be equal length.");
                }
            }

            matrix = new Cell[length, rows.Length];

            for (int y = 0; y < rows.Length; y++)
            {
                for (int x = 0; x < length; x++)
                {
                    if (key.Key.TryGetValue(rows[x][y], out var cell))
                    {
                        matrix[x, y] = cell;
                    }
                }
            }
        }
    }
}