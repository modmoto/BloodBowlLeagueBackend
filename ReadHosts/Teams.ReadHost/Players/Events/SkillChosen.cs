using Microwave.Domain;

namespace Teams.ReadHost.Players.Events
{
    public class SkillChosen : IDomainEvent
    {
        public Identity EntityId { get; }
        public StringIdentity NewSkill { get; }
        public SkillChosen(GuidIdentity entityId, StringIdentity newSkill)
        {
            EntityId = entityId;
            NewSkill = newSkill;
        }
    }
}