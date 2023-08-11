namespace TextAdventureLibrary
{
    public struct Cell
    {
        public readonly char symbol;
        public readonly Color fore;
        public readonly Color back;

        public Cell(char p_symbol, Color p_fore, Color p_back)
        {
            symbol = p_symbol;
            fore = p_fore;
            back = p_back;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Cell))
            {
                return false;
            }

            var cell = (Cell)obj;
            return symbol == cell.symbol &&
                   fore == cell.fore &&
                   back == cell.back;
        }

        public static bool operator ==(Cell a, Cell b)
        {
            if (ReferenceEquals(a, b))
            {
                return true;
            }

            return a.fore == b.fore && a.back == b.back && a.symbol == b.symbol;
        }

        public static bool operator !=(Cell a, Cell b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return symbol.ToString();
        }
    }
}
