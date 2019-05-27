using Microwave.Domain.EventSourcing;
using Microwave.Domain.Identities;

namespace Domain.Players.Events.Skills
{
    public class SkillCreated : IDomainEvent
    {
        public SkillCreated(StringIdentity skillId, SkillType skillType)
        {
            SkillId = skillId;
            SkillType = skillType;
        }

        public Identity EntityId => SkillId;
        public SkillType SkillType { get; }
        public StringIdentity SkillId { get; }
    }
}