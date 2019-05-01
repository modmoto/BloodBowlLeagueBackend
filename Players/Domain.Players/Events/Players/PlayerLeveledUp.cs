using System.Collections.Generic;
using Microwave.Domain;

namespace Domain.Players.Events.Players
{
    public class PlayerLeveledUp : IDomainEvent
    {
        public PlayerLeveledUp(GuidIdentity playerId, IEnumerable<FreeSkillPoint> freeSkillPoints, int newLevel)
        {
            PlayerId = playerId;
            FreeSkillPoints = freeSkillPoints;
            NewLevel = newLevel;
        }

        public Identity EntityId => PlayerId;
        public GuidIdentity PlayerId { get; }
        public IEnumerable<FreeSkillPoint> FreeSkillPoints { get; }
        public int NewLevel { get; }
    }
}