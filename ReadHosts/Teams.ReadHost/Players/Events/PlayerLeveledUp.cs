using System.Collections.Generic;
using Microwave.Domain.Identities;
using Microwave.Queries;

namespace Teams.ReadHost.Players.Events
{
    public class PlayerLeveledUp : ISubscribedDomainEvent
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

    public enum FreeSkillPoint
    {
        Normal, Double, PlusOneArmorOrMovement, PlusOneAgility, PlusOneStrength
    }
}