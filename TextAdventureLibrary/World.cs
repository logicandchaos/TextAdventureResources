using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
        public Matrix Map { get; private set; }
        public Die Die { get; private set; }
        public DateTime CurrentDateTime { get; private set; }
        public HashSet<Person> Everyone { get; private set; }
        public HashSet<Place> Everywhere { get; private set; }
        public HashSet<Thing> Everything { get; private set; }
        public Dictionary<DateTime, string> History { get; private set; }

        public World(string name)
        {
            Name = name;
            Die = new Die(name.GetHashCode());
            Everyone = new HashSet<Person>();
            Everywhere = new HashSet<Place>();
            Everything = new HashSet<Thing>();
            History = new Dictionary<DateTime, string>();
            //CurrentWorld = this;
        }

        public void AddTimeSpan(TimeSpan timeSpan)
        {
            CurrentDateTime += timeSpan;
        }

        public void AddPerson(Person person)
        {
            Everyone.Add(person);
        }

        public void AddPeople(params Person[] people)
        {
            Everyone.UnionWith(people);
        }

        public void RemovePerson(Person person)
        {
            Everyone.Remove(person);
        }

        public void AddThing(Thing thing)
        {
            Everything.Add(thing);
        }

        public void AddThings(params Thing[] things)
        {
            Everything.UnionWith(things);
        }

        public void RemoveThing(Thing thing)
        {
            Everything.Remove(thing);
        }

        public void AddPlace(Place place)
        {
            Everywhere.Add(place);
        }

        public void AddPlaces(params Place[] places)
        {
            Everywhere.UnionWith(places);
        }

        public void RemovePlace(Place place)
        {
            Everywhere.Remove(place);
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
