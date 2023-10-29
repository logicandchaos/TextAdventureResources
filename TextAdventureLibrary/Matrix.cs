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

                matrix = new Cell[numCols, numRows];

                for (int y = 0; y < numRows; y++)
                {
                    for (int x = 0; x < numCols; x++)
                    {
                        if (key.Key.TryGetValue(rows[y][x], out var cell))
                        {
                            matrix[x, y] = cell;
                        }
                    }
                }
            }
        }
    }
}