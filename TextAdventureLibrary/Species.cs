using System;
using System.Collections.Generic;
using System.Text;

namespace TextAdventureLibrary
{
    public class Species
    {
        /*public enum Diet
        {
            HERBIVORE,
            CARNIVORE,
            OMNIVORE
        }

        public enum SleepCycle
        {
            DIURNAL, //active during the day and sleeping at night
            NOCTURNAL, //active at night and sleeping during the day
            CREPUSCULAR, //active during twilight (dawn and dusk)
            MATUTINAL, //early-rising and active in the morning
            VESPERTINE, //active in the evening or just after sunset
        }

        public static List<SleepCycle> GetActiveSleepCycles(DateTime time)
        {
            List<SleepCycle> activeCycles = new List<SleepCycle>();

            // Check if time falls within each sleep cycle's range
            if (time.TimeOfDay >= new TimeSpan(6, 0, 0) && time.TimeOfDay < new TimeSpan(18, 0, 0))
            {
                activeCycles.Add(SleepCycle.DIURNAL);
            }
            if (time.TimeOfDay >= new TimeSpan(18, 0, 0) || time.TimeOfDay < new TimeSpan(6, 0, 0))
            {
                activeCycles.Add(SleepCycle.NOCTURNAL);
            }
            if (time.TimeOfDay >= new TimeSpan(5, 0, 0) && time.TimeOfDay < new TimeSpan(8, 0, 0) ||
                time.TimeOfDay >= new TimeSpan(17, 0, 0) && time.TimeOfDay < new TimeSpan(20, 0, 0))
            {
                activeCycles.Add(SleepCycle.CREPUSCULAR);
            }
            if (time.TimeOfDay >= new TimeSpan(4, 0, 0) && time.TimeOfDay < new TimeSpan(7, 0, 0))
            {
                activeCycles.Add(SleepCycle.MATUTINAL);
            }
            if (time.TimeOfDay >= new TimeSpan(19, 0, 0) && time.TimeOfDay < new TimeSpan(22, 0, 0))
            {
                activeCycles.Add(SleepCycle.VESPERTINE);
            }

            return activeCycles;
        }

        public readonly string name;
        //lifespan
        public readonly int lifeExpectancy;
        public readonly Diet diet;
        public readonly SleepCycle sleepCycle;
        public readonly Tuple<int, int> grouping;
        public readonly List<Cell> habitat;
        public readonly int rarity;
        //stats
        //have a min and max for character generation
        //str, vit, dex, spe, tel, cha;
        public readonly Tuple<int, int> strengthRange;
        public readonly Tuple<int, int> vitalityRange;
        public readonly Tuple<int, int> dexterityRange;
        public readonly Tuple<int, int> speedRange;
        public readonly Tuple<int, int> intelligenceRange;
        public readonly Tuple<int, int> charismaRange;

        public readonly float regenRate;//regen per hour /60 in battle add only whole numbers, constitution may add to regen rate

        public readonly int alignment;

        public Body body;

        public Species(string p_name, int p_lifeExpectancy, Diet p_diet, SleepCycle p_sleepCycle,
            Tuple<int, int> p_grouping, List<Cell> p_habitat, int p_rarity, int p_alignment, Body p_body,
            float p_regen, params int[] p_stats)
        {
            if (p_stats == null || p_stats.Length != 12)
            {
                throw new ArgumentException("Array parameter must have a length of 12");
            }
            name = p_name;
            lifeExpectancy = p_lifeExpectancy;
            grouping = p_grouping;
            diet = p_diet;
            sleepCycle = p_sleepCycle;
            body = p_body;
            //set stat ranges
            strengthRange = new Tuple<int, int>(p_stats[0], p_stats[1]);
            vitalityRange = new Tuple<int, int>(p_stats[2], p_stats[3]);
            dexterityRange = new Tuple<int, int>(p_stats[4], p_stats[5]);
            speedRange = new Tuple<int, int>(p_stats[6], p_stats[7]);
            intelligenceRange = new Tuple<int, int>(p_stats[8], p_stats[9]);
            charismaRange = new Tuple<int, int>(p_stats[10], p_stats[11]);
            habitat = p_habitat;
            rarity = p_rarity;
            alignment = p_alignment;
            regenRate = p_regen;
        }*/
    }
}
