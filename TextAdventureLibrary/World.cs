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
        /*private static readonly Lazy<World> lazyInstance = new Lazy<World>(() => new World());
        public static World Instance => lazyInstance.Value;
        private World() { }*/

        public string Name { get; private set; }
        public Matrix Map { get; set; }
        public Die die;
        public DateTime DateTimeCurrent { get; private set; }
        public List<Person> Everyone { get; set; }
        public List<Place> Everywhere { get; set; }
        public List<Thing> Everything { get; set; }
        public Dictionary<DateTime, string> History { get; private set; }

        public World(string name)
        {
            Name = name;
            //create die
            die = new Die(name.GetHashCode());
            Everyone = new List<Person>();
            Everywhere = new List<Place>();
            Everything = new List<Thing>();
            History = new Dictionary<DateTime, string>();
        }

        public void AddTimeSpan(TimeSpan timeSpan)
        {
            DateTimeCurrent += timeSpan;
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
            return JsonConvert.DeserializeObject<World>(json);
        }
    }
}
