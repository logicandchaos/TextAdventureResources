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
        }
        /// VALUES
        //Value Spectums based on Schwartz’s Theory of Basic Human Values
        // -100 = left/extreme of spectrum, +100 = right/extreme
        public int IndependenceVsConformity { get; private set; }
        public int NoveltyVsStability { get; private set; }
        public int PleasureVsRestraint { get; private set; }
        public int AmbitionVsCompassion { get; private set; }
        public int DominanceVsEquality { get; private set; }

        // Trait Helpers
        public bool IsCourageous => IndependenceVsConformity > 30 && NoveltyVsStability > 30;
        public bool IsGreedy => PleasureVsRestraint > 30 && AmbitionVsCompassion > 30;
        public bool IsLoyal => IndependenceVsConformity < -30 && NoveltyVsStability < -30;
        public bool IsAltruistic => AmbitionVsCompassion < -30 && DominanceVsEquality < -30;
        public bool IsAdventurous => NoveltyVsStability > 40 && IndependenceVsConformity > 0;
        public bool IsDisciplined => PleasureVsRestraint < -20 && NoveltyVsStability < 20;
        public bool IsDominant => DominanceVsEquality > 40 && AmbitionVsCompassion > 0;
        public bool IsFair => DominanceVsEquality < -20 && AmbitionVsCompassion < 0;

        // Archetype Methods
        public void SetRebel()
        {
            IndependenceVsConformity = 80;
            NoveltyVsStability = 70;
            PleasureVsRestraint = 0;
            AmbitionVsCompassion = 20;
            DominanceVsEquality = 0;
        }

        public void SetTyrant()
        {
            IndependenceVsConformity = 20;
            NoveltyVsStability = 0;
            PleasureVsRestraint = 20;
            AmbitionVsCompassion = 80;
            DominanceVsEquality = 90;
        }

        public void SetCaregiver()
        {
            IndependenceVsConformity = -20;
            NoveltyVsStability = 10;
            PleasureVsRestraint = -10;
            AmbitionVsCompassion = -80;
            DominanceVsEquality = -70;
        }

        public void SetTraditionalist()
        {
            IndependenceVsConformity = -80;
            NoveltyVsStability = -70;
            PleasureVsRestraint = -10;
            AmbitionVsCompassion = 10;
            DominanceVsEquality = -30;
        }

        public void SetHedonist()
        {
            IndependenceVsConformity = 10;
            NoveltyVsStability = 20;
            PleasureVsRestraint = 80;
            AmbitionVsCompassion = 40;
            DominanceVsEquality = 10;
        }

        public void SetVisionary()
        {
            IndependenceVsConformity = 80;
            NoveltyVsStability = 40;
            PleasureVsRestraint = 0;
            AmbitionVsCompassion = -60;
            DominanceVsEquality = -20;
        }

        public void SetSurvivor()
        {
            IndependenceVsConformity = -10;
            NoveltyVsStability = -80;
            PleasureVsRestraint = -60;
            AmbitionVsCompassion = 10;
            DominanceVsEquality = -10;
        }

        public void SetCoreValues(int IvC, int NvS, int PvR, int AvC, int DvE)
        {
            IndependenceVsConformity = IvC;
            NoveltyVsStability = NvS;
            PleasureVsRestraint = PvR;
            AmbitionVsCompassion = AvC;
            DominanceVsEquality = DvE;
        }

        /// EMOTION        
        public Emotion CurrentEmotionalState { get; private set; }
        public Emotion Mood { get; private set; }
        public List<Emotion> EmotionHistory { get; private set; }
        public Emotion GetLastEmotion() => EmotionHistory.Count > 0 ? EmotionHistory[EmotionHistory.Count] : null;

        /// RELATIONSHIPS
        private Dictionary<Person, Relationship> relationships;

        // Generate emotion from utility change
        public Emotion ProcessUtilityChange(Utility utility)
        {
            if (utility.HasChanged)
            {
                float normalizedChange = utility.GetNormalizedChange() * utility.Weight;
                var emotion = new Emotion(normalizedChange, Emotion.EmotionTimeFrame.Present);
                EmotionHistory.Add(emotion);
                return emotion;
            }
            return null;
        }

        // Anticipate future emotion from expected change
        public Emotion AnticipateChange(float expectedChange, Dictionary<string, Utility> utilities)
        {
            var emotion = new Emotion(expectedChange, Emotion.EmotionTimeFrame.Future);
            EmotionHistory.Add(emotion);
            return emotion;
        }

        // Reflect on past change
        public Emotion ReflectOnChange(float pastChange)
        {
            var emotion = new Emotion(pastChange, Emotion.EmotionTimeFrame.Past);
            EmotionHistory.Add(emotion);
            return emotion;
        }

        // Choose best decision from options
        public void MakeChoice(Menu menu)
        {
            //make decision based on estimated change in utility, but also emotion, values and relationship
            menu.SelectOption(1);
            menu.Execute();
        }

        public void React(Person person, List<Utility> utilitiesChanged)
        {
            foreach (Utility u in utilitiesChanged)
            {
                //EmotionHistory.Add();
                //relationships[person].AddEmotion();
            }
        }

        public void GenerateMood()
        {
            //moods are random
        }

        public void UpdateCurrentEmotionalState()
        {
            if (Mood == null)
                GenerateMood();

            // Take last 3 emotions plus mood for averaging
            var recent = EmotionHistory.Skip(Math.Max(0, EmotionHistory.Count - 3)).ToList();
            recent.Add(Mood);

            float sum = 0f;
            foreach (var e in recent)
            {
                sum += e.Type == Emotion.EmotionType.Positive ? e.Intensity : -e.Intensity;
            }

            float average = recent.Count > 0 ? sum / recent.Count : 0f;
            CurrentEmotionalState = new Emotion(average, Emotion.EmotionTimeFrame.Present);
        }
    }
}