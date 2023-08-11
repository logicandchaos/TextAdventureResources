using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace TextAdventureLibrary
{
    class NameGenerator
    {
        private readonly Dictionary<string, List<string>> _chain = new Dictionary<string, List<string>>();
        private readonly List<string> _vowels = new List<string> { "a", "e", "i", "o", "u", "y" };

        public NameGenerator(List<string> names)
        {
            BuildChain(names);
        }

        private void BuildChain(List<string> names)
        {
            foreach (var name in names)
            {
                // Split the name into syllables
                var syllables = new List<string>();
                var currentSyllable = "";
                for (int i = 0; i < name.Length; i++)
                {
                    var c = name[i];
                    if (_vowels.Contains(c.ToString()))
                    {
                        // Found a vowel, so end the current syllable and start a new one
                        syllables.Add(currentSyllable);
                        currentSyllable = c.ToString();
                    }
                    else
                    {
                        // Found a consonant, so add it to the current syllable
                        currentSyllable += c.ToString();
                    }
                }
                // Add the final syllable
                syllables.Add(currentSyllable);

                // Build the Markov chain using the list of syllables for the name
                for (int i = 0; i < syllables.Count - 1; i++)
                {
                    var key = syllables[i];
                    var nextSyllable = syllables[i + 1];
                    if (!_chain.ContainsKey(key))
                    {
                        _chain[key] = new List<string>();
                    }
                    _chain[key].Add(nextSyllable);
                }
            }
        }

        public string GenerateName(Random random)
        {
            var currentKey = _chain.Keys.ElementAt(random.Next(_chain.Keys.Count));
            var name = currentKey;

            while (true)
            {
                if (!_chain.TryGetValue(currentKey, out List<string> nextSyllables))
                {
                    break;
                }

                var nextSyllable = nextSyllables[random.Next(nextSyllables.Count)];
                name += nextSyllable;

                if (name.Length >= 20)
                {
                    break;
                }

                // Find the last syllable in the name
                var lastSyllable = "";
                for (int i = name.Length - 1; i >= 0; i--)
                {
                    var c = name[i];
                    if (_vowels.Contains(c.ToString()))
                    {
                        lastSyllable = name.Substring(i);
                        break;
                    }
                }

                currentKey = lastSyllable;
            }

            // Capitalize the first letter of the name
            return char.ToUpper(name[0]) + name.Substring(1);
        }
    }
}
