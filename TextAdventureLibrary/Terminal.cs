﻿namespace TextAdventureLibrary
{
    public enum Color
    {
        Black,
        DarkRed,
        DarkGreen,
        DarkYellow,
        DarkBlue,
        DarkMagenta,
        DarkCyan,
        Gray,
        DarkGray,
        Red,
        Green,
        Yellow,
        Blue,
        Magenta,
        Cyan,
        White,
        Transparent
    }

    public abstract class Terminal
    {
        public abstract void SetColor(Color foregroundColor, Color backgroundColor);

        public abstract void Print(string text);

        public abstract void Type(string text, int delay = 20);

        public abstract void Print(Cell cell);

        public abstract void Print(Inventory inventory);

        public abstract void Print(Map map, bool clearScreen = false, bool title = true, bool info = true);

        public abstract void Print(Menu menu);

        public abstract void Print(Noun noun);

        public abstract void PrintDescription(Noun noun);

        public abstract void MoveCurser(Vector2Double point);

        public abstract void ResetColor();

        public abstract void SetColorToHealth(int healthPercent);

        public abstract void Anykey(string message = "<Press any key>");

        public abstract bool YesNo(string question = "");

        public abstract char GetChar();

        public abstract int GetDigit(int max);

        public abstract string GetString(string message = "");

        public abstract bool GraphicText(string input, Color col, bool clearScreen = true);

        public abstract void ClearScreen();

        public abstract void FlushInpuTQueue();

        public bool DEBUGMODE { get; private set; } = false;

        public void SetDebugMode(bool b)
        {
            DEBUGMODE = b;
        }

        //public void SetDebugColor()
        //{
        //}

        public abstract void DebugMessage(string message);
    }
}
