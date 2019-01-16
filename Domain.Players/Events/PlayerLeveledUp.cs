using System.Collections.Generic;
using Microwave.Domain;

namespace Domain.Players.Events
{
    public class PlayerLeveledUp : IDomainEvent
    {
        public PlayerLeveledUp(GuidIdentity entityId, IEnumerable<LevelUpType> freeSkillPoints)
        {
            EntityId = entityId;
            FreeSkillPoints = freeSkillPoints;
        }

        public Identity EntityId { get; }
        public IEnumerable<LevelUpType> FreeSkillPoints { get; }
    }
}