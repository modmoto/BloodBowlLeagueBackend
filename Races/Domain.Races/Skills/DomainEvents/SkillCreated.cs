using Microwave.Domain.EventSourcing;

namespace Domain.Races.Skills.DomainEvents
{
    public class SkillCreated : IDomainEvent
    {
        public SkillCreated(string skillId, SkillType skillType)
        {
            SkillId = skillId;
            SkillType = skillType;
        }
        
        public string SkillId { get; }
        public SkillType SkillType { get; }
        public string EntityId => SkillId;
    }
}