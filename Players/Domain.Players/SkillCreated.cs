using Microwave.Domain.Identities;
using Microwave.Queries;

namespace Domain.Players
{
    public class SkillCreated : ISubscribedDomainEvent
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