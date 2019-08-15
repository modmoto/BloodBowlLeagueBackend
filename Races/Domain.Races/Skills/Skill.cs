using System.Linq;
using System.Reflection;
using Microwave.Domain.Identities;

namespace Domain.Races.Skills
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
            var skill = obj as Skill;
            return SkillId == skill?.SkillId;
        }
        public override int GetHashCode()
        {
            return SkillId != null ? SkillId.GetHashCode() : 0;
        }

        public static Skill Create(StringIdentity skillId)
        {
            var skillClassType = typeof(Skill);
            var staticSkillCreates = skillClassType.GetProperties(BindingFlags.Public | BindingFlags.Static);
            var foundSkill = staticSkillCreates.SingleOrDefault(p => p.Name == skillId.Id);
            if (foundSkill == null) return new Skill(NullSkill.SkillId, NullSkill.SkillType);
            var createdSkill = foundSkill.GetValue(null, null) as Skill;
            return new Skill(createdSkill.SkillId, createdSkill.SkillType);
        }

        public static Skill NullSkill => new Skill(StringIdentity.Create("NotFoundSkill"), default(SkillType));
        public static Skill Catch => new Skill(StringIdentity.Create(nameof(Catch)), SkillType.Agility);
        public static Skill Block => new Skill(StringIdentity.Create(nameof(Block)), SkillType.General);
        public static Skill Dodge => new Skill(StringIdentity.Create(nameof(Dodge)), SkillType.Agility);
        public static Skill Pass => new Skill(StringIdentity.Create(nameof(Pass)), SkillType.Passing);
        public static Skill SureHands => new Skill(StringIdentity.Create(nameof(SureHands)), SkillType.Passing);
        public static Skill MightyBlow => new Skill(StringIdentity.Create(nameof(MightyBlow)), SkillType.Strength);
        public static Skill PlusOneMovement => new Skill(StringIdentity.Create(nameof(PlusOneMovement)), SkillType.PlusOneArmorOrMovement);
        public static Skill PlusOneArmor => new Skill(StringIdentity.Create(nameof(PlusOneArmor)), SkillType.PlusOneArmorOrMovement);
        public static Skill PlusOneAgility => new Skill(StringIdentity.Create(nameof(PlusOneAgility)), SkillType.PlusOneAgility);
        public static Skill PlusOneStrength => new Skill(StringIdentity.Create(nameof(PlusOneStrength)), SkillType.PlusOneStrength);
        public static Skill Shadowing => new Skill(StringIdentity.Create(nameof(Shadowing)), SkillType.Extraordinary);
        public static Skill Stab => new Skill(StringIdentity.Create(nameof(Stab)), SkillType.Extraordinary);
        public static Skill JumUp => new Skill(StringIdentity.Create(nameof(JumUp)), SkillType.Agility);
        public static Skill Frenzy => new Skill(StringIdentity.Create(nameof(Frenzy)), SkillType.Strength);
        public static Skill DumpOff => new Skill(StringIdentity.Create(nameof(DumpOff)), SkillType.Passing);
    }
}