using System;
using System.Collections.Generic;
using System.Text;

namespace TextAdventureLibrary
{
    public class Encounter
    {
        public Encounter(List<Person> party1, List<Person> party2)
        {
            //initiative
            //roll for each Person
            //put into a list
            List<Person> people = new List<Person>();
            int turn = 0;
            //turn based loop
            while (true)
            {
                //people[turn].brain.MakeChoice(menu);
                turn = turn < people.Count ? turn++ : 0;
                break;
            }
        }
    }
}
