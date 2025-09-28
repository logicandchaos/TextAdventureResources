using System;
using System.Collections.Generic;
using System.Text;

namespace TextAdventureLibrary
{
    public class Encounter
    {
        private List<Person> participants;
        private int whosTurn = 0;
        private int currentTurn = 0;
        private Dictionary<Person, Menu> personMenus = new Dictionary<Person, Menu>();
        private bool end = false;

        public Encounter(params Person[] participants)//in order
        {
            this.participants.AddRange(participants);
            //Loop();? need to setup menus 1st.. which will be done in the encounter creator
            //I could probably initiate the loop from the encounter creator
        }

        public void SetMenuForPerson(Person person, Menu menu)
        {
            personMenus[person] = menu;
            //I also want to attack EndTurn to the menus selections to trigger the end of turn.
            //Although I could try just calling EndTurn after, should work, but if not I'll try the above method.
            //I also need to make sure it works with submenues, I think I might need to add a collection of menues.
            //might make a SetMenusForPerson as well with an array of menues.
            //I can also add EndEncounter to menuItem action.
            //Although.. everyone will have the same menu and options.. unless I want to give the ability to give
            //different playsers different options, like advanced skills.. so might keeps as is.
        }

        public void Loop()
        {
            while(!end)
            {
                //participants[whosTurn]
                //checked if human or npc
                //if human input comes from keyboard otherwise comes from npc, unless I fake human input as an npc..
                //if I simulate real inputs for my npcs then I don't have to check if it's a human or npc.
                //However this could cause issues where the player can control the npc which would suck!
                //so what I should do is get the brain component, have a human one that gets keyboard input and an Ai
                //one that will use decision making algorithms, maybe different ones based on creature intelligence.
                //show menu
                //characyer make choice.
                EndTurn();
            }
        }

        public void EndTurn()
        {
            currentTurn++;
            whosTurn = (currentTurn + 1) % participants.Count;
        }

        public void EndEncounter()
        {
            end = true;
        }

        public void RemoveParticipant(Person person)
        {
            if (participants.Contains(person))
            {
                participants.Remove(person);
            }
        }
    }
}
