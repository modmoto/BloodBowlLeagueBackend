using System.Collections.Generic;
using Microwave.Domain;

namespace Domain.Players.Events.Players
{
    public class PlayerLeveledUp : IDomainEvent
    {
        public PlayerLeveledUp(GuidIdentity entityId, IEnumerable<FreeSkillPoint> freeSkillPoints, int newLevel)
        {
            EntityId = entityId;
            FreeSkillPoints = freeSkillPoints;
            NewLevel = newLevel;
        }

        public Identity EntityId { get; }
        public IEnumerable<FreeSkillPoint> FreeSkillPoints { get; }
        public int NewLevel { get; }
    }
}