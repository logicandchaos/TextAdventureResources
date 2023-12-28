using System;
using System.Collections.Generic;
using System.Text;

namespace TextAdventureLibrary
{
    public class DescriptionGenerator<T> where T : Noun, new()
    {
        //textGenerationRules
        //maybe have a dictionary of attributes and how to describe it, like height =< 5'6 return "short"
        //what to use Delegates?? Action?? RegEx??

        public DescriptionGenerator(/*textGenerationRules*/)
        {
        }

        public string GenerateDescription(T noun)
        {
            string description = "";
            //get attributes from noun
            //describe each attribute based on rules
            return description;
        }
    }
}
