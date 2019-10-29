using System;
using System.Collections.Generic;
using Microwave.Queries;

namespace Teams.ReadHost.Players.Events
{
    public class PlayerLeveledUp : ISubscribedDomainEvent
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

        public string EntityId => PlayerId.ToString();
        public Guid PlayerId { get; }
        public IEnumerable<FreeSkillPoint> NewFreeSkillPoints { get; }
        public int NewLevel { get; }
    }

    public enum FreeSkillPoint
    {
        Normal, Double, PlusOneArmorOrMovement, PlusOneAgility, PlusOneStrength
    }
}