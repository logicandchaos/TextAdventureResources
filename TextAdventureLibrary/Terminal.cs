namespace TextAdventureLibrary
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

        public abstract void Print(string text, bool newLine = false);

        public abstract void Type(string text, bool newLine = false, int delay = 20);

        public abstract void Print(Cell cell);

        public abstract void Print(Matrix matrix);

        public abstract void Menu(Menu menu);

        public abstract void Print(Noun textGameObject);

        public abstract void PrintDescription(Noun textGameObject);

        public abstract void MoveCurser(Point point);

        public abstract void ResetColor();

        public abstract void SetColorToHealth(float healthPercent);

        public abstract void Anykey(string message = "<Press any key>");

        public abstract bool YesNo();

        public abstract char GetChar();

        public abstract int GetDigit(int max);

        public abstract string GetString(string message = "");

        public abstract bool GraphicText(string input, Color col, bool clearScreen = true);

        public abstract void ClearScreen();
    }
}
