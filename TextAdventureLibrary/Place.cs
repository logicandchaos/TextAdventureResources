using System.Collections.Generic;

namespace TextAdventureLibrary
{
    public class Place : Noun
    {
        public Vector2Int Location { get; private set; }
        public float Size { get; private set; }
        public List<Person> Population { get; private set; } = new List<Person>();

        public Place()//Blank Constructor is left for Builder Implementation
        {
        }

        public void SetLocation(Vector2Int location)
        {
            Location = location;
        }

        public void SetSize(float size)
        {
            Size = size;
        }

        public void AdjustSize(float size)
        {
            Size += size;
        }

        public void AddPerson(Person person)
        {
            if (!Population.Contains(person))
            {
                Population.Add(person);
            }
        }

        public void RemovePerson(Person person)
        {
            if (Population.Contains(person))
            {
                Population.Remove(person);
            }
        }

        public Place(string name, string description, Vector2Int location, float size)
        {
            Name = name;
            Description = description;
            Location = location;
            Size = size;
        }
    }
}