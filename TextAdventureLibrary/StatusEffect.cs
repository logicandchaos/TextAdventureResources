using System;
using System.Collections.Generic;
using System.Text;

namespace TextAdventureLibrary
{
    public class StatusEffect : Attribute
    {
        public StatusEffect(string name, object value) : base(name, value)
        {
        }
    }
}
