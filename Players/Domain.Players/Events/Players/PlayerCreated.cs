using Microwave.Domain;

namespace Domain.Players.Events.Players
{
    public class PlayerCreated : IDomainEvent
    {
        public PlayerCreated(
            GuidIdentity playerId,
            GuidIdentity teamId,
            StringIdentity playerTypeId,
            PlayerConfig playerConfig)
        {
            PlayerId = playerId;
            TeamId = teamId;
            PlayerTypeId = playerTypeId;
            PlayerConfig = playerConfig;
        }

        public Identity EntityId => PlayerId;
        public GuidIdentity TeamId { get; }
        public StringIdentity PlayerTypeId { get; }
        public PlayerConfig PlayerConfig { get; }
        public GuidIdentity PlayerId { get; }
    }
}