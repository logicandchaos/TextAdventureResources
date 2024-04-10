namespace TextAdventureLibrary
{
    public class Place : Noun
    {
        public Vector2Int Location { get; private set; }
        public float Size { get; private set; }

        public Place(string name, Vector2Int location, float size)
        {
            Name = name;
            Location = location;
            Size = size;
        }
    }
}