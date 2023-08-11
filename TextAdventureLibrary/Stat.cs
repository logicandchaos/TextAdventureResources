using System;
using System.Collections.Generic;
using System.Text;

namespace TextAdventureLibrary
{
    public enum Stats
    {
        Strength = 0,
        Vitality = 1,
        Dexterity = 2,
        Speed = 3,
        Intelligence = 4,
        Charisma = 5
    }

    public struct Stat
    {
        public readonly string name;
        public readonly int max;
        public float value;

        public Stat(string p_name, float p_value, int p_max)
        {
            name = p_name;
            max = p_max;
            value = p_value;
        }

        public void IncreaseStat(float p_amount)
        {
            value += p_amount;
            if (value > max)
                value = max;
        }

        public int StatCheck(int roll)
        {
            //if (Program.showRolls)
            //Console.Write(p_character.GetName() + " " + name + " check: ");
            if (roll == 10)
            {
                Console.Write(roll + "\n");
                Console.WriteLine("CRITICAL FAIL!!!");
            }
            else
            {
                if (roll > (int)value)
                {
                    roll = 0;
                }
                if (roll == (int)value)
                {
                    roll *= 2;
                    /*if (Program.showRolls)
                    {
                        Console.Write(roll + "\n");
                        Console.WriteLine("CRITICAL ROLL!!!");
                    }*/
                }
                else if (roll < 1)
                {
                    /*if (Program.showRolls)
                    {
                        Console.Write("FAILED\n");
                    }*/
                }
                //else if (Program.showRolls)
                //  Console.Write(roll + "\n");
            }
            return roll;
        }
    }
}