using System.Collections.Generic;

namespace TextAdventureLibrary
{
    public class Place : Noun
    {
        public Dictionary<string, Place> connected;

        public Place() { }

        public List<string> GetConnected()
        {
            return new List<string>(connected.Keys);
        }
    }
}
