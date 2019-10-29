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
            var foundSkill = staticSkillCreates.SingleOrDefault(p => p.Name == skillId);
            if (foundSkill == null) return new Skill(NullSkill.SkillId, NullSkill.SkillType);
            var createdSkill = foundSkill.GetValue(null, null) as Skill;
            return new Skill(createdSkill.SkillId, createdSkill.SkillType);
        }

        public static Skill NullSkill => new Skill("NotFoundSkill", default(SkillType));
        public static Skill Catch => new Skill(nameof(Catch), SkillType.Agility);
        public static Skill Block => new Skill(nameof(Block), SkillType.General);
        public static Skill Dodge => new Skill(nameof(Dodge), SkillType.Agility);
        public static Skill Pass => new Skill(nameof(Pass), SkillType.Passing);
        public static Skill SureHands => new Skill(nameof(SureHands), SkillType.General);
        public static Skill MightyBlow => new Skill(nameof(MightyBlow), SkillType.Strength);
        public static Skill PlusOneMovement => new Skill(nameof(PlusOneMovement), SkillType.PlusOneArmorOrMovement);
        public static Skill PlusOneArmor => new Skill(nameof(PlusOneArmor), SkillType.PlusOneArmorOrMovement);
        public static Skill PlusOneAgility => new Skill(nameof(PlusOneAgility), SkillType.PlusOneAgility);
        public static Skill PlusOneStrength => new Skill(nameof(PlusOneStrength), SkillType.PlusOneStrength);
        public static Skill Shadowing => new Skill(nameof(Shadowing), SkillType.Extraordinary);
        public static Skill Stab => new Skill(nameof(Stab), SkillType.Extraordinary);
        public static Skill JumpUp => new Skill(nameof(JumpUp), SkillType.Agility);
        public static Skill Frenzy => new Skill(nameof(Frenzy), SkillType.Strength);
        public static Skill DumpOff => new Skill(nameof(DumpOff), SkillType.Passing);
    }
}