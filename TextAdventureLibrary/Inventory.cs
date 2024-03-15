    using System.Collections.Generic;

/// <summary>
/// This is the Inventory class for managing the inventory.
/// I made it a List rather than a dictionary, because I may need to add multiple of the same objets
/// and having an order could be useful for iterating aand other things.
/// I added in a weight and volume system if you want to limit either of these in your game.
/// weight is in pounds and volume is in cubic inches.
/// </summary>

namespace TextAdventureLibrary
{
    public class Inventory
    {
        public float MaxWeight { get; private set; }
        public float MaxVolume { get; private set; }
        public float CurrentWeight { get; private set; }
        public float VolumeUsed { get; private set; }
        public List<Thing> Things { get; private set; }

        public Inventory(float maxWeight, float space)
        {
            MaxWeight = maxWeight;
            MaxVolume = space;
            CurrentWeight = 0;
            VolumeUsed = 0;
        }

        public bool TryAddToInventory(Thing thing)
        {
            float weight = thing.GetAttributeValue<float>("weight");
            float volume = thing.GetAttributeValue<float>("volume");
            if (weight + CurrentWeight <= MaxWeight
                && volume + VolumeUsed <= MaxVolume)
            {
                Things.Add(thing);
                CurrentWeight += weight;
                VolumeUsed += volume;
                return true;
            }

            return false;
        }

        public bool TryRemoveFromInventory(Thing thing)
        {
            if (Things.Contains(thing))
            {
                float weight = thing.GetAttributeValue<float>("weight");
                float volume = thing.GetAttributeValue<float>("volume");
                Things.Remove(thing);
                CurrentWeight -= weight;
                VolumeUsed -= volume;
                return true;
            }

            return false;
        }

        public bool TryRemoveFromInventory(int index)
        {
            if (Things.Count > index)
            {
                float volume = Things[index].GetAttributeValue<float>("volume");
                float weight = Things[index].GetAttributeValue<float>("weight");
                Things.RemoveAt(index);
                CurrentWeight -= weight;
                VolumeUsed -= volume;
                return true;
            }

            return false;
        }
    }
}