using System;
using System.Collections.Generic;
using System.Text;

namespace TextAdventureLibrary
{
    public class MathFunctions
    {
        public static float Clamp(float value, float min, float max)
        {
            return Math.Max(min, Math.Min(max, value));
        }
    }
}
