using Microwave.Domain;

namespace Teams.ReadHost.Players.Events
{
    public class PlayerCreated
    {
        public PlayerCreated(
            GuidIdentity playerId,
            StringIdentity playerTypeId,
            GuidIdentity teamId)
        {
            PlayerId = playerId;
            PlayerTypeId = playerTypeId;
            TeamId = teamId;
        }

        public GuidIdentity PlayerId { get; }
        public StringIdentity PlayerTypeId { get; }
        public GuidIdentity TeamId { get; }
    }
}