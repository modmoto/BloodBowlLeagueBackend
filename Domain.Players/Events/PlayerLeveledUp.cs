using System.Collections.Generic;
using Microwave.Domain;

namespace Domain.Players.Events
{
    public class PlayerLeveledUp : IDomainEvent
    {
        public PlayerLeveledUp(GuidIdentity entityId, IEnumerable<FreeSkillPoint> freeSkillPoints)
        {
            EntityId = entityId;
            FreeSkillPoints = freeSkillPoints;
        }

        public Identity EntityId { get; }
        public IEnumerable<FreeSkillPoint> FreeSkillPoints { get; }
    }
}