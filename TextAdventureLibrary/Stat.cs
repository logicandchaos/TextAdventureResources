using System;

namespace TextAdventureLibrary
{
    public class Stat
    {
        public readonly int max;
        public object Value { get; set; }

        public Stat(float value, int max)
        {
            this.max = max;
            Value = value;
        }

        public void IncreaseStat(float amount)
        {
            float value = (float)Value;
            value += amount;
            if (value > max)
                value = max;
            Value = value;
        }

        public int StatCheck(int roll)
        {
            //if (Program.showRolls)
            //Console.Write(p_character.GetName() + " " + name + " check: ");
            if (roll == 0)
            {
                Console.Write(roll + "\n");
                Console.WriteLine("CRITICAL FAIL!!!");
            }
            else
            {
                if (roll > (int)Value)
                {
                    roll = 0;
                }
                if (roll == (int)Value)
                {
                    roll *= 2;
                    /*if (Program.showRolls)
                    {
                        Console.Write(roll + "\n");
                        Console.WriteLine("CRITICAL ROLL!!!");
                    }*/
                }
                //else if (Program.showRolls)
                //  Console.Write(roll + "\n");
            }
            return roll;
        }
    }
}