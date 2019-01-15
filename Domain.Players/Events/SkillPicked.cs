using System.Collections.Generic;
using Microwave.Domain;

namespace Domain.Players.Events
{
    public class SkillPicked : IDomainEvent
    {
        public Identity EntityId { get; }
        public StringIdentity NewSkill { get; }
        public IEnumerable<SkillType> RemainingLevelUps { get; }

        public SkillPicked(GuidIdentity entityId, StringIdentity newSkill, IEnumerable<SkillType> remainingLevelUps)
        {
            EntityId = entityId;
            NewSkill = newSkill;
            RemainingLevelUps = remainingLevelUps;
        }
    }
}