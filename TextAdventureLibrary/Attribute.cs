using System;
using System.Collections.Generic;
using System.Text;

namespace TextAdventureLibrary
{
    public class Attribute
    {
        public string Name { get; private set; }
        public object Value { get; set; }

        public Attribute(string name, object value)
        {
            Name = name;
            Value = value;
        }

        public int GetValue(int defaultValue = 0)
        {
            if (Value is int intValue)
            {
                return intValue;
            }
            else
            {
                return defaultValue;
            }
        }

        public string GetValue(string defaultValue = "")
        {
            if (Value is string stringValue)
            {
                return stringValue;
            }
            else
            {
                return defaultValue;
            }
        }

        public float GetValue(float defaultValue = 0)
        {
            if (Value is float floatValue)
            {
                return floatValue;
            }
            else
            {
                return defaultValue;
            }
        }

    }
}
