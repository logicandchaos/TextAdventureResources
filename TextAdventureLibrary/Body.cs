namespace TextAdventureLibrary
{
    public class BodyPart
    {
        public string Label { get; }
        public float DamageModifier { get; }
        public float HitChance { get; }

        public BodyPart(string label, float damageModifier, float hitChance)
        {
            Label = label;
            DamageModifier = damageModifier;
            HitChance = hitChance;
        }
    }

    public struct Body
    {
        private readonly BodyPart[] _bodyParts;

        public Body(params BodyPart[] bodyParts)
        {
            _bodyParts = bodyParts;
        }

        public BodyPart[] GetBodyParts()
        {
            return _bodyParts;
        }
    }
}
