using System;
using System.Collections.Generic;
using System.Text;

namespace TextAdventureLibrary
{
    public class Template
    {
        public Dictionary<string, object> attributes;

        public Template(string name)
        {
            attributes.Add("name", name);
        }
    }
}
