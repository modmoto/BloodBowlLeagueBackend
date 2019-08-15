using Microwave.Domain.Identities;
using Microwave.Queries;

namespace Teams.ReadHost.Players.Events
{
    public class PlayerLeveledUp : ISubscribedDomainEvent
    {
        public PlayerLeveledUp(
            GuidIdentity playerId,
            FreeSkillPoint newFreeSkillPoint,
            int newLevel)
        {
            PlayerId = playerId;
            NewFreeSkillPoint = newFreeSkillPoint;
            NewLevel = newLevel;
        }

        public Identity EntityId => PlayerId;
        public GuidIdentity PlayerId { get; }
        public FreeSkillPoint NewFreeSkillPoint { get; }
        public int NewLevel { get; }
    }

    public enum FreeSkillPoint
    {
        Normal, Double, PlusOneArmorOrMovement, PlusOneAgility, PlusOneStrength
    }
}