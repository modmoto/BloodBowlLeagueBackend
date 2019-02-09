using System.Collections.Generic;
using Microwave.Domain;

namespace Teams.ReadHost.Teams.PlayerReadModels.Events
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