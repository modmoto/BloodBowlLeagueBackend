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

        public SkillChosen(GuidIdentity playerId, SkillReadModel newSkill)
        {
            PlayerId = playerId;
            NewSkill = newSkill;
        }
    }
}