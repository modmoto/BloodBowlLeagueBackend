using Microwave.Domain;

namespace Teams.ReadHost.Players.Events
{
    public class PlayerLeveledUp : IDomainEvent
    {
        public PlayerLeveledUp(GuidIdentity playerId, int newLevel)
        {
            PlayerId = playerId;
            NewLevel = newLevel;
        }

        public Identity EntityId => PlayerId;
        public GuidIdentity PlayerId { get; }
        public int NewLevel { get; }
    }
}