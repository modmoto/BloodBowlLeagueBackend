using Microwave.Domain;

namespace Teams.ReadHost.Players.Events
{
    public class PlayerLeveledUp
    {
        public PlayerLeveledUp(GuidIdentity playerId, int newLevel)
        {
            PlayerId = playerId;
            NewLevel = newLevel;
        }

        public GuidIdentity PlayerId { get; }
        public int NewLevel { get; }
    }
}