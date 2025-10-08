using System.Collections.Generic;
using TextAdventureLibrary;

public static class QuestTemplates
{
    public static QuestTemplate SlayMonster = new QuestTemplate(
        "Slay {monster} for {giver}",
        "{giver} is worried about the {monster} terrorizing the {location}.",
        "{giver} is relieved you defeated the {monster}!",
        new List<string> { "Defeat the {monster}" }
    ).WithUtilityReward("courage", 5);

    public static QuestTemplate RescuePerson = new QuestTemplate(
        "Rescue {person} from {location}",
        "{giver} fears for {person} who is trapped in {location}.",
        "You successfully rescued {person} from {location}!",
        new List<string> { "Rescue {person}" }
    ).WithUtilityReward("heroism", 10);

    public static QuestTemplate FetchItem = new QuestTemplate(
        "Retrieve {item} for {giver}",
        "{giver} needs the {item} from {location}.",
        "You delivered {item} to {giver}.",
        new List<string> { "Collect the {item}" }
    ).WithUtilityReward("favor", 3);
}
