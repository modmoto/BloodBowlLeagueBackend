using Microwave.Domain;

namespace Domain.Players
{
    public class Skill : Entity
    {
        public StringIdentity SkillId { get; private set; }
        public SkillType SkillType { get; private set; }

        public bool IsBiggerOrEqual(SkillType skillType)
        {
            if (SkillType == SkillType.Strength) return true;
            if (SkillType == SkillType.Agility) return true;
            return false;
        }
    }
}