using System.Collections.Generic;

namespace TextAdventureLibrary
{
    public class Place : Noun
    {
        public float Size { get; private set; }
        public List<Person> Population { get; private set; } = new List<Person>();

        public Place()//Blank Constructor is left for Builder Implementation
        {
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
            SetLocation(location);
            Size = size;
        }

        public bool IsLocationWithin(Vector2Int location)
        {
            return Location.DistanceTo(location) < Size;
        }
    }
}