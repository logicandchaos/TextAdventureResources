using System;
using System.Collections.Generic;

namespace TextAdventureLibrary
{
    public class Tracery
    {
        private Dictionary<string, List<string>> grammar = new Dictionary<string, List<string>>();

        public void AddRule(string key, string value)
        {
            if (grammar.ContainsKey(key))
            {
                grammar[key].Add(value);
            }
            else
            {
                grammar[key] = new List<string> { value };
            }
        }

        public string Expand(string input)
        {
            Random random = new Random();

            while (input.Contains("#"))
            {
                int start = input.IndexOf("#");
                int end = input.IndexOf("#", start + 1);
                if (end == -1)
                {
                    break;
                }

                string key = input.Substring(start + 1, end - start - 1);
                List<string> values;
                if (grammar.TryGetValue(key, out values))
                {
                    string value = values[random.Next(values.Count)];
                    input = input.Substring(0, start) + value + input.Substring(end + 1);
                }
            }

            return input;
        }
    }
}
