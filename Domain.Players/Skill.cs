using Microwave.Domain;

namespace Domain.Players
{
    public class Skill : Entity
    {
        public StringIdentity SkillId { get; private set; }
        public SkillType SkillType { get; private set; }

        public void Apply(SkillCreated skillCreated)
        {
            SkillId = (StringIdentity) skillCreated.EntityId;
            SkillType = skillCreated.SkillType;
        }
    }

    public class SkillCreated : IDomainEvent
    {
        public SkillCreated(Identity entityId, SkillType skillType)
        {
            EntityId = entityId;
            SkillType = skillType;
        }

        public Identity EntityId { get; }
        public SkillType SkillType { get; }
    }
}