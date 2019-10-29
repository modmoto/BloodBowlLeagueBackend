namespace Domain.Players
{
    public class SkillCreated : ISubscribedDomainEvent
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