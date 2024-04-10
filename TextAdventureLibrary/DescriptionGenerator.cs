using System.Collections.Generic;
using System.Text;

namespace TextAdventureLibrary
{
    public class DescriptionGenerator<T> where T : Noun, new()
    {
        private Dictionary<string, Tracery> attributeRules = new Dictionary<string, Tracery>();

        // Add a rule for a specific attribute
        public void AddAttributeRule(string attributeName, Tracery rule)
        {
            attributeRules[attributeName] = rule;
        }

        public string GenerateDescription(T noun)
        {
            StringBuilder description = new StringBuilder();

            // Apply rules for attributes based on the type
            foreach (var rule in attributeRules)
            {
                string attributeName = rule.Key;
                Tracery attributeRule = rule.Value;

                string attributeValue = noun.GetAttributeValue<string>(attributeName);
                string expandedRule = attributeRule.Expand(attributeValue);

                description.Append(expandedRule).Append(" ");
            }

            return description.ToString().Trim();
        }
    }
}