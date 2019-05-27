using Microwave.Domain.Identities;
using Microwave.Queries;

namespace Teams.ReadHost.Players.Events
{
    public class PlayerLeveledUp : ISubscribedDomainEvent
    {
        public PlayerLeveledUp(GuidIdentity playerId, int newLevel)
        {
            PlayerId = playerId;
            NewLevel = newLevel;
        }

        public GuidIdentity PlayerId { get; }
        public int NewLevel { get; }
        public Identity EntityId => PlayerId;
    }
}