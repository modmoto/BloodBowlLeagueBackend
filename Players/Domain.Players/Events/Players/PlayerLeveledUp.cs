using Microwave.Domain.EventSourcing;
using Microwave.Domain.Identities;

namespace Domain.Players.Events.Players
{
    public class PlayerLeveledUp : IDomainEvent
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
}