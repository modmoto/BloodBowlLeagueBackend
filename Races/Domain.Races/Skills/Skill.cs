using System.Linq;
using System.Reflection;

namespace Domain.Races.Skills
{
    public class Skill
    {
        private Skill(string skillId, SkillType skillType)
        {
            SkillId = skillId;
            SkillType = skillType;
        }

        public string SkillId { get; }
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

        public static Skill Create(string skillId)
        {
            var skillClassType = typeof(Skill);
            var staticSkillCreates = skillClassType.GetProperties(BindingFlags.Public | BindingFlags.Static);
            var foundSkill = staticSkillCreates.SingleOrDefault(p => p.Name == skillId.Id);
            if (foundSkill == null) return new Skill(NullSkill.SkillId, NullSkill.SkillType);
            var createdSkill = foundSkill.GetValue(null, null) as Skill;
            return new Skill(createdSkill.SkillId, createdSkill.SkillType);
        }

        public static Skill NullSkill => new Skill(string.Create("NotFoundSkill"), default(SkillType));
        public static Skill Catch => new Skill(string.Create(nameof(Catch)), SkillType.Agility);
        public static Skill Block => new Skill(string.Create(nameof(Block)), SkillType.General);
        public static Skill Dodge => new Skill(string.Create(nameof(Dodge)), SkillType.Agility);
        public static Skill Pass => new Skill(string.Create(nameof(Pass)), SkillType.Passing);
        public static Skill SureHands => new Skill(string.Create(nameof(SureHands)), SkillType.General);
        public static Skill MightyBlow => new Skill(string.Create(nameof(MightyBlow)), SkillType.Strength);
        public static Skill PlusOneMovement => new Skill(string.Create(nameof(PlusOneMovement)), SkillType.PlusOneArmorOrMovement);
        public static Skill PlusOneArmor => new Skill(string.Create(nameof(PlusOneArmor)), SkillType.PlusOneArmorOrMovement);
        public static Skill PlusOneAgility => new Skill(string.Create(nameof(PlusOneAgility)), SkillType.PlusOneAgility);
        public static Skill PlusOneStrength => new Skill(string.Create(nameof(PlusOneStrength)), SkillType.PlusOneStrength);
        public static Skill Shadowing => new Skill(string.Create(nameof(Shadowing)), SkillType.Extraordinary);
        public static Skill Stab => new Skill(string.Create(nameof(Stab)), SkillType.Extraordinary);
        public static Skill JumpUp => new Skill(string.Create(nameof(JumpUp)), SkillType.Agility);
        public static Skill Frenzy => new Skill(string.Create(nameof(Frenzy)), SkillType.Strength);
        public static Skill DumpOff => new Skill(string.Create(nameof(DumpOff)), SkillType.Passing);
    }
}