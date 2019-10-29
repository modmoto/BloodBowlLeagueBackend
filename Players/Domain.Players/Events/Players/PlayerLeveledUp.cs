using System;
using System.Collections.Generic;
using Microwave.Domain;

namespace Domain.Players.Events.Players
{
    public class PlayerLeveledUp : IDomainEvent
    {
        public PlayerLeveledUp(
            Guid playerId,
            IEnumerable<FreeSkillPoint> newFreeSkillPoints,
            int newLevel)
        {
            PlayerId = playerId;
            NewFreeSkillPoints = newFreeSkillPoints;
            NewLevel = newLevel;
        }

        public string EntityId => PlayerId;
        public Guid PlayerId { get; }
        public IEnumerable<FreeSkillPoint> NewFreeSkillPoints { get; }
        public int NewLevel { get; }
    }
}