using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace TextAdventureLibrary
{
    /// <summary>
    /// Quest status for tracking progression
    /// </summary>
    public enum QuestStatus
    {
        NotStarted,
        InProgress,
        Completed,
        Failed
    }

    /// <summary>
    /// Represents a single objective within a quest
    /// </summary>
    public class QuestObjective
    {
        public string Description { get; private set; }
        public string ProgressKey { get; private set; }
        public int RequiredAmount { get; private set; }
        public int CurrentAmount { get; private set; }
        public bool IsComplete => CurrentAmount >= RequiredAmount;
        public Dictionary<string, float> CompletionUtilityEffects { get; private set; }

        public QuestObjective(string description, string progressKey, int requiredAmount)
        {
            Description = description;
            ProgressKey = progressKey;
            RequiredAmount = requiredAmount;
            CurrentAmount = 0;
            CompletionUtilityEffects = new Dictionary<string, float>();
        }

        public void UpdateProgress(int amount)
        {
            CurrentAmount = Math.Min(CurrentAmount + amount, RequiredAmount);
        }

        public void SetProgress(int amount)
        {
            CurrentAmount = Math.Min(amount, RequiredAmount);
        }

        public string GetProgressText() => $"{Description} ({CurrentAmount}/{RequiredAmount})";
    }

    /// <summary>
    /// Quest template for Mad Lib-style quest creation
    /// </summary>
    public class QuestTemplate
    {
        public string NameTemplate { get; private set; }
        public string DescriptionTemplate { get; private set; }
        public string CompletionMessageTemplate { get; private set; }
        public List<string> ObjectiveTemplates { get; private set; }
        public Dictionary<string, float> UtilityRewards { get; private set; }

        public QuestTemplate(
            string nameTemplate,
            string descriptionTemplate,
            string completionMessageTemplate,
            List<string> objectiveTemplates = null)
        {
            NameTemplate = nameTemplate;
            DescriptionTemplate = descriptionTemplate;
            CompletionMessageTemplate = completionMessageTemplate;
            ObjectiveTemplates = objectiveTemplates ?? new List<string>();
            UtilityRewards = new Dictionary<string, float>();
        }

        public QuestTemplate WithUtilityReward(string utilityName, float amount)
        {
            UtilityRewards[utilityName] = amount;
            return this;
        }

        public Quest CreateQuest(Dictionary<string, object> variables, Person questGiver = null)
        {
            return new Quest(this, variables, questGiver);
        }
    }

    /// <summary>
    /// An instantiated quest with variables filled in
    /// </summary>
    public class Quest
    {
        public QuestTemplate Template { get; private set; }
        public Dictionary<string, object> Variables { get; private set; }
        public QuestStatus Status { get; private set; }
        public Person QuestGiver { get; private set; }
        public List<QuestObjective> Objectives { get; private set; }
        private Dictionary<string, object> _customProgress;

        public Quest(QuestTemplate template, Dictionary<string, object> variables, Person questGiver = null)
        {
            Template = template;
            Variables = variables;
            QuestGiver = questGiver;
            Status = QuestStatus.NotStarted;
            Objectives = new List<QuestObjective>();
            _customProgress = new Dictionary<string, object>();

            // Create objectives from template
            foreach (var objTemplate in Template.ObjectiveTemplates)
            {
                string filledObjective = FillTemplate(objTemplate);
                Objectives.Add(new QuestObjective(filledObjective, objTemplate, 1));
            }
        }

        private string FillTemplate(string template)
        {
            string result = template;
            foreach (var kvp in Variables)
            {
                result = result.Replace("{" + kvp.Key + "}", kvp.Value?.ToString() ?? "");
            }
            return result;
        }

        public string Name => FillTemplate(Template.NameTemplate);
        public string Description => FillTemplate(Template.DescriptionTemplate);
        public string CompletionMessage => FillTemplate(Template.CompletionMessageTemplate);

        public void Start()
        {
            if (Status == QuestStatus.NotStarted)
                Status = QuestStatus.InProgress;
        }

        public void UpdateObjectiveProgress(string progressKey, int amount)
        {
            var objective = Objectives.FirstOrDefault(o => o.ProgressKey == progressKey);
            if (objective == null) return;

            objective.UpdateProgress(amount);

            if (AreAllObjectivesComplete())
                Complete();
        }

        public bool AreAllObjectivesComplete() => Objectives.All(o => o.IsComplete);

        public void Complete() => Status = QuestStatus.Completed;
        public void Fail() => Status = QuestStatus.Failed;

        public void SetCustomProgress(string key, object value) => _customProgress[key] = value;
        public T GetCustomProgress<T>(string key) =>
            _customProgress.ContainsKey(key) ? (T)_customProgress[key] : default;

        public void AwardRewards(Person person)
        {
            foreach (var reward in Template.UtilityRewards)
            {
                var utility = person.GetAttributeValue<Utility>(reward.Key);
                if (utility != null) utility.ModifyValue(reward.Value);
            }
        }
    }

    /// <summary>
    /// Tracks all quests for a person
    /// </summary>
    public class QuestLog
    {
        private Person owner;
        private List<Quest> activeQuests;
        private List<Quest> completedQuests;
        private List<Quest> failedQuests;

        public IReadOnlyList<Quest> ActiveQuests => activeQuests.AsReadOnly();
        public IReadOnlyList<Quest> CompletedQuests => completedQuests.AsReadOnly();
        public IReadOnlyList<Quest> FailedQuests => failedQuests.AsReadOnly();

        public QuestLog(Person owner)
        {
            this.owner = owner;
            activeQuests = new List<Quest>();
            completedQuests = new List<Quest>();
            failedQuests = new List<Quest>();
        }

        public void AddQuest(Quest quest)
        {
            if (!activeQuests.Contains(quest) && quest.Status == QuestStatus.NotStarted)
            {
                quest.Start();
                activeQuests.Add(quest);
            }
        }

        public void CompleteQuest(Quest quest)
        {
            if (!activeQuests.Contains(quest)) return;

            quest.Complete();
            activeQuests.Remove(quest);
            completedQuests.Add(quest);

            quest.AwardRewards(owner);

            if (quest.QuestGiver != null)
            {
                var brain = owner.GetAttributeValue<Brain>("brain");
                var rel = brain?.GetRelationship(quest.QuestGiver);
                if (rel != null)
                {
                    rel.SetTrust(rel.Trust + 10);
                    rel.SetRespect(rel.Respect + 10);
                    rel.SetAffection(rel.Affection + 5);
                    rel.SetEmotionalDebt(rel.EmotionalDebt + 15);
                }
            }
        }

        public void FailQuest(Quest quest)
        {
            if (!activeQuests.Contains(quest)) return;

            quest.Fail();
            activeQuests.Remove(quest);
            failedQuests.Add(quest);

            if (quest.QuestGiver != null)
            {
                var brain = owner.GetAttributeValue<Brain>("brain");
                var rel = brain?.GetRelationship(quest.QuestGiver);
                if (rel != null)
                {
                    rel.SetTrust(rel.Trust - 15);
                    rel.SetRespect(rel.Respect - 10);
                }
            }
        }

        public Quest GetQuestByName(string name) =>
            activeQuests.FirstOrDefault(q => q.Name.ToLower().Contains(name.ToLower()));

        public bool HasActiveQuest(string name) => GetQuestByName(name) != null;
        public bool HasCompletedQuest(string name) =>
            completedQuests.Any(q => q.Name.ToLower().Contains(name.ToLower()));
    }

    /// <summary>
    /// Extension methods for easier quest handling
    /// </summary>
    public static class QuestExtensions
    {
        public static QuestLog GetQuestLog(this Person person)
        {
            var log = person.GetAttributeValue<QuestLog>("questLog");
            if (log == null)
            {
                log = new QuestLog(person);
                person.AddOrSetAttribute("questLog", log);
            }
            return log;
        }

        public static void GiveQuest(this Person questGiver, Person receiver, Quest quest)
        {
            var log = receiver.GetQuestLog();
            log.AddQuest(quest);

            var brain = questGiver.GetAttributeValue<Brain>("brain");
            var rel = brain?.GetRelationship(receiver);
            if (rel != null)
            {
                rel.SetTrust(rel.Trust + 5);
                rel.SetFamiliarity(rel.Familiarity + 10);
            }
        }
    }
}