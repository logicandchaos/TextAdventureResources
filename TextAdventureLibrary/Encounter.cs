using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// Encounter is for interactions between 2 or more characters, incuding battles and trading among other things.
/// </summary>

namespace TextAdventureLibrary
{
    public class Encounter
    {
        List<Person> people;
        int turn = 0;
        Menu encounterMenu;
        //different ways to order list?
        //turns
        public void Run()
        {
            while (true)
            {
                //build menu
                //display menu
                //make slection
                //end turn
                turn++;
                if (turn > people.Count)
                    turn = 0;
                //test
                break;
            }
            //results
        }
    }
}
