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
        List<Person> people = new List<Person>();
        int turn = 0;
        Menu encounterMenu;
        World world;

        public Encounter(World world, Action<List<Person>> Sort = null, params Person[] people)
        {
            if (people.Length < 2)
                return;
            this.people.AddRange(people);
            Sort?.Invoke(this.people);
            this.world = world;
            Run();
        }
        //different ways to order list? maybe inject a sorting method or null for none?
        //turns
        void Run()
        {
            while (true)
            {
                //build menu with ActionSystem                
                //encounterMenu ActionSystem.GetAvailableActions(this.people[turn], world);
                //encounterMenu=
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
