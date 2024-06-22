using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

/// <summary>
/// World class contains everything contained in the world. Lists for Everyone, Everywhere, Everything.
/// </summary>

namespace TextAdventureLibrary
{
    public class World
    {
        static string saveFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SaveData");
        //public static World CurrentWorld { get; private set; }

        public string Name { get; private set; }
        public Map Map { get; private set; }
        public Die Die { get; private set; }
        public DateTime CurrentDateTime { get; private set; }
        public Dictionary<string, Person> Everyone { get; private set; } = new Dictionary<string, Person>();
        public Dictionary<string, Place> Everywhere { get; private set; } = new Dictionary<string, Place>();
        public Dictionary<string, Thing> Everything { get; private set; } = new Dictionary<string, Thing>();
        public Dictionary<DateTime, string> History { get; private set; } = new Dictionary<DateTime, string>();

        public World(string name, string info, MapKey key, Cell[,] matrix)
        {
            Name = name;
            Die = new Die(name.GetHashCode());
            Map = new Map(name, info, key, matrix);
        }

        public World(string name, string info, MapKey key)
        {
            Name = name;
            Die = new Die(name.GetHashCode());
            Map = new Map(name, info, key);
        }

        public World(string name, string info, MapKey key, string mapData)
        {
            Name = name;
            Die = new Die(name.GetHashCode());
            Map = new Map(name, info, key);
            Map.CreateMatrixFromString(mapData);
        }

        public void SetDateTime(DateTime dateTime)
        {
            CurrentDateTime = dateTime;
        }

        public void AddTimeSpan(TimeSpan timeSpan)
        {
            CurrentDateTime += timeSpan;
        }

        public void AddPerson(Person person)
        {
            Everyone.Add(person.Name, person);
        }

        public void AddPeople(params Person[] people)
        {
            foreach (Person p in people)
                AddPerson(p);
        }

        public void RemovePerson(string person)
        {
            Everyone.Remove(person);
        }

        public void AddThing(Thing thing)
        {
            Everything.Add(thing.Name, thing);
        }

        public void AddThings(params Thing[] things)
        {
            foreach (Thing t in things)
                AddThing(t);
        }

        public void RemoveThing(string thing)
        {
            Everything.Remove(thing);
        }

        public void AddPlace(Place place)
        {
            Everywhere.Add(place.Name, place);
        }

        public void AddPlaces(params Place[] places)
        {
            foreach (Place p in places)
                AddPlace(p);
        }

        public void RemovePlace(string place)
        {
            Everywhere.Remove(place);
        }

        public Place GetClosestPlace(Vector2Int location)
        {
            Place closestPlace = null;
            double closestDistance = double.MaxValue;

            foreach (var place in Everywhere.Values)
            {
                double distance = location.DistanceTo(place.Location);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestPlace = place;
                }
            }

            return closestPlace;
        }

        public Place[] OrderPlacesByDistance(Vector2Int location)
        {
            return Everywhere
                .Values
                .OrderBy
                (place => location.DistanceTo(place.Location))
                .ToArray();
        }

        public Place WithinBordersOf(Vector2Int location)
        {
            Place closestPlace = GetClosestPlace(location);
            if (closestPlace.Location.DistanceTo(location) < closestPlace.Size)
                return closestPlace;
            return null;
        }

        public void AddHistoricalEvent(string historicalEvent)//should have title too? or use title as key??
        {
            if (History.ContainsKey(CurrentDateTime))
            {
                History.Add(CurrentDateTime, History.TryGetValue(CurrentDateTime, out string value) + "\n" + historicalEvent);
            }
            else
                History.Add(CurrentDateTime, historicalEvent);
        }

        public void AddHistoricalEvent(DateTime dateTime, string historicalEvent)
        {
            if (History.ContainsKey(dateTime))
            {
                History.Add(dateTime, History.TryGetValue(dateTime, out string value) + "\n" + historicalEvent);
            }
            else
                History.Add(dateTime, historicalEvent);
        }

        public List<string> GetEventsAbout(string subject)
        {
            return History.Where(entry => entry.Value.Contains(subject))
                          .Select(entry => $"{entry.Key.ToShortDateString()}: {entry.Value}")
                          .ToList();
        }

        public void Save()
        {
            if (!Directory.Exists(saveFolderPath))
            {
                Directory.CreateDirectory(saveFolderPath);
            }

            string fileName = $"{Name}.json";
            string filePath = Path.Combine(saveFolderPath, fileName);

            // Serialize all data
            string json = JsonConvert.SerializeObject(this, Formatting.Indented);

            // Write JSON to file
            File.WriteAllText(filePath, json);
        }

        public static World Load(string worldName)
        {
            string fileName = $"{worldName}.json";
            string filePath = Path.Combine(saveFolderPath, fileName);

            // Read JSON from file
            string json = File.ReadAllText(filePath);

            // Deserialize JSON to object
            World world = JsonConvert.DeserializeObject<World>(json);

            return world;
        }
    }
}
