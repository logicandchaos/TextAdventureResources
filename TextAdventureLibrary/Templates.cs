using System;
using System.Collections.Generic;
using System.Text;

namespace TextAdventureLibrary
{
    public static class Templates
    {
        public static BodyPart Head { get; } = new BodyPart(2, 1);
        public static BodyPart Body { get; } = new BodyPart(1, 50);
        public static BodyPart LeftHand { get; } = new BodyPart(1, 5);
        public static BodyPart RightHand { get; } = new BodyPart(1, 5);
        public static BodyPart LeftArm { get; } = new BodyPart(1, 10);
        public static BodyPart RightArm { get; } = new BodyPart(1, 10);
        public static BodyPart LeftLeg { get; } = new BodyPart(1, 10);
        public static BodyPart RightLeg { get; } = new BodyPart(1, 10);
        public static BodyPart FrontLeftLeg { get; } = new BodyPart(1, 10);
        public static BodyPart FrontRightLeg { get; } = new BodyPart(1, 10);
        public static BodyPart BackLeftLeg { get; } = new BodyPart(1, 10);
        public static BodyPart BackRightLeg { get; } = new BodyPart(1, 10);
        public static BodyPart Tail { get; } = new BodyPart(1, 10);
        //could add teeth and claws for different species
        /*public static BodyPart Wings { get; } = new BodyPart(1, 10);
        public static BodyPart Claws { get; } = new BodyPart(1, 10);
        public static BodyPart Fangs { get; } = new BodyPart(1, 10);
        public static BodyPart Stinger { get; } = new BodyPart(1, 10);*/

        public static Dictionary<string, object> human = new Dictionary<string, object>();
        public static Dictionary<string, object> slime = new Dictionary<string, object>();
        public static Dictionary<string, object> wolf = new Dictionary<string, object>();
        public static Dictionary<string, object> bandit = new Dictionary<string, object>();//could use human?
        public static Dictionary<string, object> briggand = new Dictionary<string, object>();
        public static Dictionary<string, object> ogre = new Dictionary<string, object>();
        public static Dictionary<string, object> goblin = new Dictionary<string, object>();
        public static Dictionary<string, object> hobgoblin = new Dictionary<string, object>();
        public static Dictionary<string, object> bugbear = new Dictionary<string, object>();
        public static Dictionary<string, object> troll = new Dictionary<string, object>();
        public static Dictionary<string, object> wyvern = new Dictionary<string, object>();
        public static Dictionary<string, object> manticore = new Dictionary<string, object>();
        public static Dictionary<string, object> skeleton = new Dictionary<string, object>();
        public static Dictionary<string, object> skeletonWarrior = new Dictionary<string, object>();
        public static Dictionary<string, object> mummy = new Dictionary<string, object>();
        public static Dictionary<string, object> jackal = new Dictionary<string, object>();

        //Personality Templates
        public static Dictionary<Utility, float> cautious = new Dictionary<Utility, float>();
        public static Dictionary<Utility, float> reckless = new Dictionary<Utility, float>();

        static Templates()
        {
            //add equip methods?

            //add body parts
            human.Add("head", Head);
            human.Add("torso", Body);
            human.Add("leftHand", LeftHand);
            human.Add("rightHand", RightHand);
            human.Add("leftArm", LeftArm);
            human.Add("rightArm", RightArm);
            human.Add("leftLeg", LeftLeg);
            human.Add("rightLeg", RightLeg);
            //stats
            human.Add("strength", new Stat(3, 9));
            human.Add("vitality", new Stat(3, 9));
            human.Add("dexterity", new Stat(3, 9));
            human.Add("speed", new Stat(3, 9));
            human.Add("intelligence", new Stat(3, 9));
            human.Add("charisma", new Stat(3, 9));
            //human.Add("body", humanoid);//if I want to add as a dictionary

            //utilities
            Utility health = new Utility(10, 10);
            human.Add("health", health);

            Thing sword = new ThingBuilder().Build();

            Predicate<Person> hasSwordEquiped = person => person.GetAttributeValue<Thing>("equipedItem") == sword;

            human.Add("punch", health);
        }
    }
}