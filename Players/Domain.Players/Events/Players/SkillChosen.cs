using System.Collections.Generic;
using Microwave.Domain.EventSourcing;
using Microwave.Domain.Identities;

namespace Domain.Players.Events.Players
{
    public class SkillChosen : IDomainEvent
    {
        public Identity EntityId => PlayerId;
        public GuidIdentity PlayerId { get; }
        public SkillReadModel NewSkill { get; }
        public IEnumerable<FreeSkillPoint> RemainingLevelUps { get; }

        public SkillChosen(GuidIdentity playerId, SkillReadModel newSkill, IEnumerable<FreeSkillPoint> remainingLevelUps)
        {
            PlayerId = playerId;
            NewSkill = newSkill;
            RemainingLevelUps = remainingLevelUps;
        }
    }
}