using System.Collections.Generic;
using Microwave.Domain;

namespace Domain.Players.Events.Players
{
    public class SkillChosen : IDomainEvent
    {
        public Identity EntityId { get; }
        public StringIdentity NewSkill { get; }
        public IEnumerable<FreeSkillPoint> RemainingLevelUps { get; }

        public SkillChosen(GuidIdentity entityId, StringIdentity newSkill, IEnumerable<FreeSkillPoint> remainingLevelUps)
        {
            EntityId = entityId;
            NewSkill = newSkill;
            RemainingLevelUps = remainingLevelUps;
        }
    }
}