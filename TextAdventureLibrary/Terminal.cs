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

        public abstract void Print(string message, bool newLine = false);

        public abstract void Type(string message, bool newLine = false, int delay = 20);

        public abstract void Print(Cell cell);

        public abstract void Print(Matrix matrix);

        public abstract void Print(Menu menu);

        public abstract void Print(Noun textGameObject);

        public abstract void PrintDescription(Noun textGameObject);

        public abstract void MoveCurser(Point point);

        public abstract void ResetColor();

        public abstract void SetColorToHealth(float healthPercent);

        public abstract void Anykey(string p_message = "<Press any key>");

        public abstract bool YesNo();

        public abstract bool GraphicText(string input, Color col, bool clearScreen = true);
    }
}
