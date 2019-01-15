using Microwave.Domain;

namespace Domain.Players.Events
{
    public class PlayerCreated : IDomainEvent
    {
        public PlayerCreated(
            GuidIdentity entityId,
            StringIdentity playerTypeId,
            GuidIdentity teamId,
            PlayerConfig playerConfig)
        {
            EntityId = entityId;
            PlayerTypeId = playerTypeId;
            TeamId = teamId;
            PlayerConfig = playerConfig;
        }

        public Identity EntityId { get; }
        public StringIdentity PlayerTypeId { get; }
        public GuidIdentity TeamId { get; }
        public PlayerConfig PlayerConfig { get; }
    }
}