using System.Collections.Generic;
using Microwave.Domain.EventSourcing;
using Microwave.Domain.Identities;

namespace Domain.Players.Events.Players
{
    public class PlayerLeveledUp : IDomainEvent
    {
        public PlayerLeveledUp(
            GuidIdentity playerId,
            IEnumerable<FreeSkillPoint> newFreeSkillPoints,
            int newLevel)
        {
            PlayerId = playerId;
            NewFreeSkillPoints = newFreeSkillPoints;
            NewLevel = newLevel;
        }

        public Identity EntityId => PlayerId;
        public GuidIdentity PlayerId { get; }
        public IEnumerable<FreeSkillPoint> NewFreeSkillPoints { get; }
        public int NewLevel { get; }
    }
}