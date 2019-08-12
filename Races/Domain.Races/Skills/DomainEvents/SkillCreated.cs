using Microwave.Domain.EventSourcing;
using Microwave.Domain.Identities;

namespace Domain.Races.Skills.DomainEvents
{
    public class SkillCreated : IDomainEvent
    {
        public SkillCreated(StringIdentity skillId, SkillType skillType)
        {
            SkillId = skillId;
            SkillType = skillType;
        }
        
        public StringIdentity SkillId { get; }
        public SkillType SkillType { get; }
        public Identity EntityId => SkillId;
    }
}