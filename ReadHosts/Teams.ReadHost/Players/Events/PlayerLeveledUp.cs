using System;
using System.Collections.Generic;
using Microwave.Queries;

namespace Teams.ReadHost.Players.Events
{
    public class PlayerLeveledUp : ISubscribedDomainEvent
    {
        public PlayerLeveledUp(
            Guid playerId,
            int newLevel)
        {
            PlayerId = playerId;
            NewLevel = newLevel;
        }

        public string EntityId => PlayerId.ToString();
        public Guid PlayerId { get; }
        public int NewLevel { get; }
    }

    public enum FreeSkillPoint
    {
        Normal, Double, PlusOneArmorOrMovement, PlusOneAgility, PlusOneStrength
    }
}