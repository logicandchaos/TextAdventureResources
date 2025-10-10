using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace TextAdventureLibrary
{
    public class Brain
    {
        public Brain()
        {
            EmotionHistory = new List<Emotion>();
            relationships = new Dictionary<Person, Relationship>();
            utilityWeights = new Dictionary<UtilityCategory, float>();

            // Default human-like values
            SetCoreValues(0, 0, 0, 0, 0);
        }

        /// VALUES - Schwartz's Theory of Basic Human Values
        // -100 = left/extreme of spectrum, +100 = right/extreme
        public int IndependenceVsConformity { get; private set; }
        public int NoveltyVsStability { get; private set; }
        public int PleasureVsRestraint { get; private set; }
        public int AmbitionVsCompassion { get; private set; }
        public int DominanceVsEquality { get; private set; }

        /// EMOTION        
        public Emotion CurrentEmotionalState { get; private set; }
        public Emotion Mood { get; private set; }
        public List<Emotion> EmotionHistory { get; private set; }
        public Emotion GetLastEmotion() => EmotionHistory.Count > 0 ? EmotionHistory[EmotionHistory.Count - 1] : null;

        /// RELATIONSHIPS
        private Dictionary<Person, Relationship> relationships;

        /// UTILITY WEIGHTS
        private Dictionary<UtilityCategory, float> utilityWeights;

        // Trait Helpers
        public bool IsCourageous => IndependenceVsConformity > 30 && NoveltyVsStability > 30;
        public bool IsGreedy => PleasureVsRestraint > 30 && AmbitionVsCompassion > 30;
        public bool IsLoyal => IndependenceVsConformity < -30 && NoveltyVsStability < -30;
        public bool IsAltruistic => AmbitionVsCompassion < -30 && DominanceVsEquality < -30;
        public bool IsAdventurous => NoveltyVsStability > 40 && IndependenceVsConformity > 0;
        public bool IsDisciplined => PleasureVsRestraint < -20 && NoveltyVsStability < 20;
        public bool IsDominant => DominanceVsEquality > 40 && AmbitionVsCompassion > 0;
        public bool IsFair => DominanceVsEquality < -20 && AmbitionVsCompassion < 0;

        public void AddRelationship(Person person, Relationship relationship)
        {
            relationships[person] = relationship;
        }

        public Relationship GetRelationship(Person person)
        {
            return relationships.TryGetValue(person, out var relationship) ? relationship : null;
        }

        public void GenerateMood()
        {
            Random rand = new Random();
            float intensity = (float)rand.NextDouble();
            bool isPositive = rand.NextDouble() > 0.5;
            float valence = isPositive ? intensity : -intensity;
            Mood = new Emotion(valence, Emotion.EmotionTimeFrame.Present);
        }

        // Generate emotion from utility change
        public Emotion ProcessUtilityChange(Utility utility, float change)
        {
            float percentChanged = change / utility.Max;
            float changeImportance = percentChanged * utilityWeights[utility.Category];
            var emotion = new Emotion(changeImportance, Emotion.EmotionTimeFrame.Present);
            return emotion;
        }

        public void UpdateCurrentEmotionalState(Emotion[] emotion)
        {
            // Apply mood as a background offset that colors emotional responses
            if (Mood == null)
                GenerateMood();

            float moodOffset = Mood != null ?
                (Mood.Type == Emotion.EmotionType.Positive ? Mood.Intensity : -Mood.Intensity) * 0.3f :
                0f;

            float emotionSum = 0;
            foreach(Emotion e in emotion)
            {
                emotionSum += e.SignedValue();
            }

            float emotionalValue = emotionSum + moodOffset;
            CurrentEmotionalState = new Emotion(emotionalValue, Emotion.EmotionTimeFrame.Present);
        }

        private void InitializeUtilityWeights()
        {
            // Base weights from the paper: BasicNeeds > SocialDesires > Entertainment
            utilityWeights[UtilityCategory.BasicNeeds] = 1.0f;
            utilityWeights[UtilityCategory.SocialDesires] = 0.7f;
            utilityWeights[UtilityCategory.Entertainment] = 0.4f;

            // Modify based on personality values

            // PleasureVsRestraint affects Entertainment weighting
            utilityWeights[UtilityCategory.Entertainment] += PleasureVsRestraint * 0.003f;

            // AmbitionVsCompassion affects SocialDesires weighting
            utilityWeights[UtilityCategory.SocialDesires] += Math.Abs(AmbitionVsCompassion) * 0.002f;

            // NoveltyVsStability affects BasicNeeds (safety component)
            utilityWeights[UtilityCategory.BasicNeeds] += (NoveltyVsStability < 0 ? Math.Abs(NoveltyVsStability) * 0.002f : 0f);

            // Ensure weights don't go negative
            foreach (var key in utilityWeights.Keys.ToList())
            {
                utilityWeights[key] = Math.Max(0.1f, utilityWeights[key]);
            }
        }

        public float GetUtilityWeight(UtilityCategory category)
        {
            return utilityWeights.ContainsKey(category) ? utilityWeights[category] : 1.0f;
        }

        // Archetype configuration methods
        public void SetRebel()
        {
            SetCoreValues(80, 70, 0, 20, 0);
        }

        public void SetTyrant()
        {
            SetCoreValues(20, 0, 20, 80, 90);
        }

        public void SetCaregiver()
        {
            SetCoreValues(-20, 10, -10, -80, -70);
        }

        public void SetTraditionalist()
        {
            SetCoreValues(-80, -70, -10, 10, -30);
        }

        public void SetHedonist()
        {
            SetCoreValues(10, 20, 80, 40, 10);
        }

        public void SetVisionary()
        {
            SetCoreValues(80, 40, 0, -60, -20);
        }

        public void SetSurvivor()
        {
            SetCoreValues(-10, -80, -60, 10, -10);
        }

        public void SetHostileAnimal()
        {
            // Aggressive predator - wolf, bear
            SetCoreValues(50, -50, 20, 80, 60); // Independent, stable, some pleasure, ambitious, dominant
            SetUtilityWeights(2.0f, 0.1f, 0.0f); // Survival only
        }

        public void SetTimidAnimal()
        {
            // Prey animal - deer, rabbit
            SetCoreValues(-30, -70, -50, -20, -40); // Conforming, very stable, restrained, balanced, submissive
            SetUtilityWeights(2.5f, 0.05f, 0.0f); // Hyper-focused on survival
        }

        public void SetTerritorialAnimal()
        {
            // Defends territory but not inherently aggressive
            SetCoreValues(30, -60, 0, 40, 40); // Moderately independent, stable, balanced, somewhat ambitious/dominant
            SetUtilityWeights(2.0f, 0.15f, 0.0f); // Survival primary, slight social (pack behavior)
        }

        public void SetCoreValues(int IvC, int NvS, int PvR, int AvC, int DvE)
        {
            IndependenceVsConformity = (int)MathFunctions.Clamp(IvC, -100, 100);
            NoveltyVsStability = (int)MathFunctions.Clamp(NvS, -100, 100);
            PleasureVsRestraint = (int)MathFunctions.Clamp(PvR, -100, 100);
            AmbitionVsCompassion = (int)MathFunctions.Clamp(AvC, -100, 100);
            DominanceVsEquality = (int)MathFunctions.Clamp(DvE, -100, 100);

            InitializeUtilityWeights();
        }

        public void SetUtilityWeights(float basicNeeds, float socialDesires, float entertainment)
        {
            utilityWeights[UtilityCategory.BasicNeeds] = Math.Max(0f, basicNeeds);
            utilityWeights[UtilityCategory.SocialDesires] = Math.Max(0f, socialDesires);
            utilityWeights[UtilityCategory.Entertainment] = Math.Max(0f, entertainment);
        }

        // Main decision-making method based on utility prediction
        public void MakeChoice(Menu menu, Transaction[] transactions)
        {
            if (menu.Items.Length == 0) return;

            int bestChoice = 0;
            float bestEmotionalOutcome = float.MinValue;

            for (int i = 0; i < menu.Items.Length; i++)
            {
                float predictedEmotionalOutcome1 = transactions[i].NetValueChangeP1();
                float predictedEmotionalOutcome2 = transactions[i].NetValueChangeP2();

                var relationship = GetRelationship(transactions[i].Person2);
                float relationshipMod = relationship.OverAll();

                if (predictedEmotionalOutcome2 < -1f) //hostile
                {
                    if (relationshipMod < -1f)//enemy
                    {
                        predictedEmotionalOutcome1 += 10;//should be based on personalty
                    }
                    else if (relationshipMod > 1f)//ally
                    {
                        predictedEmotionalOutcome1 -= 10;//should be based on personalty
                    }
                }
                else if (predictedEmotionalOutcome2 > 1f) //helpful
                {
                    if (relationshipMod < -1f)//enemy
                    {
                        predictedEmotionalOutcome1 -= 10;//should be based on personalty
                    }
                    else if (relationshipMod > 1f)//ally
                    {
                        predictedEmotionalOutcome1 += 10;//should be based on personalty
                    }
                }

                predictedEmotionalOutcome1 += GetEmotionalModifier();

                if (predictedEmotionalOutcome1 > bestEmotionalOutcome)
                {
                    bestEmotionalOutcome = predictedEmotionalOutcome1;
                    bestChoice = i;
                }
            }

            menu.SelectOption(bestChoice);
            menu.Execute();
        }

        private float GetEmotionalModifier()
        {
            if (CurrentEmotionalState == null) return 1.0f;

            // Positive emotions make us more optimistic/risk-taking
            // Negative emotions make us more cautious
            if (CurrentEmotionalState.Type == Emotion.EmotionType.Positive)
                return 1.0f + (CurrentEmotionalState.Intensity * 0.2f);
            else
                return 1.0f - (CurrentEmotionalState.Intensity * 0.3f);
        }
    }
}