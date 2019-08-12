using System.Linq;
using System.Reflection;
using Domain.Races.Skills.DomainEvents;
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
            var createdSkill = foundSkill.GetValue(null, null) as SkillCreated;
            return new Skill(createdSkill.SkillId, createdSkill.SkillType);
        }

        public static Skill Create(SkillCreated created)
        {
            return new Skill(created.SkillId, created.SkillType);
        }

        public static SkillCreated NullSkill => new SkillCreated(StringIdentity.Create("NotFoundSkill"), default(SkillType));
        public static SkillCreated Catch => new SkillCreated(StringIdentity.Create(nameof(Catch)), SkillType.Agility);
        public static SkillCreated Block => new SkillCreated(StringIdentity.Create(nameof(Block)), SkillType.General);
        public static SkillCreated Dodge => new SkillCreated(StringIdentity.Create(nameof(Dodge)), SkillType.Agility);
        public static SkillCreated Pass => new SkillCreated(StringIdentity.Create(nameof(Pass)), SkillType.Passing);
        public static SkillCreated PlusOneStrength => new SkillCreated(StringIdentity.Create(nameof(PlusOneStrength)), SkillType.PlusOneStrength);
        public static SkillCreated PickUp => new SkillCreated(StringIdentity.Create(nameof(PickUp)), SkillType.Agility);
        public static SkillCreated Throw => new SkillCreated(StringIdentity.Create(nameof(Throw)), SkillType.Passing);
        public static SkillCreated MightyBlow => new SkillCreated(StringIdentity.Create(nameof(MightyBlow)), SkillType.Strength);
    }
}