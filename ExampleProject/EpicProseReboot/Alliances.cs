﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpicProseReboot
{
    public static class Alliances
    {
        public static Dictionary<(string, string), int> speciesAlliances = new Dictionary<(string, string), int>()
        {
            { ("human","slime"),-1 },
            { ("human","wolf"),-1 },
            { ("human","bandit"),-1 },
            { ("human","briggand"),-1 },
            { ("human","ogre"),-1 },
            { ("human","goblin"),-1 },
            { ("human","hobgoblin"),-1 },
            { ("human","bugbear"),-1 },
            { ("human","troll"),-1 },
            { ("human","wyvern"),-1 },
            { ("human","maniticore"),-1 },
            { ("human","skeleton"),-1 },
            { ("human","skeletonWarrior"),-1 },
            { ("human","mummy"),-1 },
            { ("human","jackal"),-1 },
            { ("human","iceGiant"),-1 },
            { ("human","dragon"),-1 },

            { ("slime","wolf"),1 },
            { ("slime","bandit"),1 },
            { ("slime","briggand"),1 },
            { ("slime","ogre"),1 },
            { ("slime","goblin"),1 },
            { ("slime","hobgoblin"),1 },
            { ("slime","bugbear"),1 },
            { ("slime","troll"),1 },
            { ("slime","wyvern"),1 },
            { ("slime","maniticore"),1 },
            { ("slime","skeleton"),1 },
            { ("slime","skeletonWarrior"),1 },
            { ("slime","mummy"),1 },
            { ("slime","jackal"),1 },
            { ("slime","iceGiant"),1 },
            { ("slime","dragon"),1 },

            { ("wolf","bandit"),1 },
            { ("wolf","briggand"),1 },
            { ("wolf","ogre"),1 },
            { ("wolf","goblin"),1 },
            { ("wolf","hobgoblin"),1 },
            { ("wolf","bugbear"),1 },
            { ("wolf","troll"),1 },
            { ("wolf","wyvern"),1 },
            { ("wolf","maniticore"),1 },
            { ("wolf","skeleton"),1 },
            { ("wolf","skeletonWarrior"),1 },
            { ("wolf","mummy"),1 },
            { ("wolf","jackal"),1 },
            { ("wolf","iceGiant"),1 },
            { ("wolf","dragon"),1 },

            { ("bandit","briggand"),1 },
            { ("bandit","ogre"),1 },
            { ("bandit","goblin"),1 },
            { ("bandit","hobgoblin"),1 },
            { ("bandit","bugbear"),1 },
            { ("bandit","troll"),1 },
            { ("bandit","wyvern"),1 },
            { ("bandit","maniticore"),1 },
            { ("bandit","skeleton"),1 },
            { ("bandit","skeletonWarrior"),1 },
            { ("bandit","mummy"),1 },
            { ("bandit","jackal"),1 },
            { ("bandit","iceGiant"),1 },
            { ("bandit","dragon"),1 },

            { ("briggand","ogre"),1 },
            { ("briggand","goblin"),1 },
            { ("briggand","hobgoblin"),1 },
            { ("briggand","bugbear"),1 },
            { ("briggand","troll"),1 },
            { ("briggand","wyvern"),1 },
            { ("briggand","maniticore"),1 },
            { ("briggand","skeleton"),1 },
            { ("briggand","skeletonWarrior"),1 },
            { ("briggand","mummy"),1 },
            { ("briggand","jackal"),1 },
            { ("briggand","iceGiant"),1 },
            { ("briggand","dragon"),1 },

            { ("ogre","goblin"),1 },
            { ("ogre","hobgoblin"),1 },
            { ("ogre","bugbear"),1 },
            { ("ogre","troll"),1 },
            { ("ogre","wyvern"),1 },
            { ("ogre","maniticore"),1 },
            { ("ogre","skeleton"),1 },
            { ("ogre","skeletonWarrior"),1 },
            { ("ogre","mummy"),1 },
            { ("ogre","jackal"),1 },
            { ("ogre","iceGiant"),1 },
            { ("ogre","dragon"),1 },

            { ("goblin","hobgoblin"),1 },
            { ("goblin","bugbear"),1 },
            { ("goblin","troll"),1 },
            { ("goblin","wyvern"),1 },
            { ("goblin","maniticore"),1 },
            { ("goblin","skeleton"),1 },
            { ("goblin","skeletonWarrior"),1 },
            { ("goblin","mummy"),1 },
            { ("goblin","jackal"),1 },
            { ("goblin","iceGiant"),1 },
            { ("goblin","dragon"),1 },

            { ("hobgoblin","bugbear"),1 },
            { ("hobgoblin","troll"),1 },
            { ("hobgoblin","wyvern"),1 },
            { ("hobgoblin","maniticore"),1 },
            { ("hobgoblin","skeleton"),1 },
            { ("hobgoblin","skeletonWarrior"),1 },
            { ("hobgoblin","mummy"),1 },
            { ("hobgoblin","jackal"),1 },
            { ("hobgoblin","iceGiant"),1 },
            { ("hobgoblin","dragon"),1 },

            { ("bugbear","troll"),1 },
            { ("bugbear","wyvern"),1 },
            { ("bugbear","maniticore"),1 },
            { ("bugbear","skeleton"),1 },
            { ("bugbear","skeletonWarrior"),1 },
            { ("bugbear","mummy"),1 },
            { ("bugbear","jackal"),1 },
            { ("bugbear","iceGiant"),1 },
            { ("bugbear","dragon"),1 },

            { ("troll","wyvern"),1 },
            { ("troll","maniticore"),1 },
            { ("troll","skeleton"),1 },
            { ("troll","skeletonWarrior"),1 },
            { ("troll","mummy"),1 },
            { ("troll","jackal"),1 },
            { ("troll","iceGiant"),1 },
            { ("troll","dragon"),1 },

            { ("wyvern","maniticore"),1 },
            { ("wyvern","skeleton"),1 },
            { ("wyvern","skeletonWarrior"),1 },
            { ("wyvern","mummy"),1 },
            { ("wyvern","jackal"),1 },
            { ("wyvern","iceGiant"),1 },
            { ("wyvern","dragon"),1 },

            { ("maniticore","skeleton"),1 },
            { ("maniticore","skeletonWarrior"),1 },
            { ("maniticore","mummy"),1 },
            { ("maniticore","jackal"),1 },
            { ("maniticore","iceGiant"),1 },
            { ("maniticore","dragon"),1 },

            { ("skeleton","skeletonWarrior"),1 },
            { ("skeleton","mummy"),1 },
            { ("skeleton","jackal"),1 },
            { ("skeleton","iceGiant"),1 },
            { ("skeleton","dragon"),1 },

            { ("skeletonWarrior","mummy"),1 },
            { ("skeletonWarrior","jackal"),1 },
            { ("skeletonWarrior","iceGiant"),1 },
            { ("skeletonWarrior","dragon"),1 },

            { ("mummy","jackal"),1 },
            { ("mummy","iceGiant"),1 },
            { ("mummy","dragon"),1 },

            { ("jackal","iceGiant"),1 },
            { ("jackal","dragon"),1 },

            { ("iceGiant","dragon"),1 }
        };

        public static Dictionary<(string, string), int> characterAlliances = new Dictionary<(string, string), int>()
        {

        }; 
    }
}