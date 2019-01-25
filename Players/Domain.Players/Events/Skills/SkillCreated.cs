using Microwave.Domain;

namespace Domain.Players.Events.Skills
{
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