using System.Linq;
using System.Reflection;
using Microwave.Domain.Identities;

namespace Domain.Players
{
    public class Skill
    {
        private Skill(StringIdentity skillId, SkillType skillType)
        {
            SkillId = skillId;
            SkillType = skillType;
        }

        public StringIdentity SkillId { get; }
        public SkillType SkillType { get; }

        public override bool Equals(object obj)
        {
            return Equals(obj as Skill);
        }

        private bool Equals(Skill other)
        {
            return Equals(SkillId, other.SkillId);
        }

        public override int GetHashCode()
        {
            return SkillId != null ? SkillId.GetHashCode() : 0;
        }

        public static Skill Block => new Skill(StringIdentity.Create(nameof(Block)), SkillType.General);
        public static Skill Dodge => new Skill(StringIdentity.Create(nameof(Dodge)), SkillType.Agility);
        public static Skill Pass => new Skill(StringIdentity.Create(nameof(Pass)), SkillType.Passing);
        public static Skill PlusOneStrength => new Skill(StringIdentity.Create(nameof(PlusOneStrength)), SkillType.PlusOneStrength);

        public static Skill Create(StringIdentity skillId)
        {
            var skillClassType = typeof(Skill);
            var staticSkillCreates = skillClassType.GetProperties(BindingFlags.Static);
            var foundSkill = staticSkillCreates.SingleOrDefault(p => p.Name == skillId.Id);
            if (foundSkill == null) return NullSkill;
            var createdSkill = foundSkill.GetValue(null, null) as Skill;
            return createdSkill;
        }

        private static Skill NullSkill => new Skill(StringIdentity.Create("NotFoundSkill"), default(SkillType));
    }
}