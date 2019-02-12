using Microwave.Domain;

namespace Domain.Players.Events.Players
{
    public class PlayerCreated : IDomainEvent
    {
        public PlayerCreated(
            GuidIdentity entityId,
            GuidIdentity teamId,
            StringIdentity playerTypeId,
            PlayerConfig playerConfig)
        {
            EntityId = entityId;
            TeamId = teamId;
            PlayerTypeId = playerTypeId;
            PlayerConfig = playerConfig;
        }

        public Identity EntityId { get; }
        public GuidIdentity TeamId { get; }
        public StringIdentity PlayerTypeId { get; }
        public PlayerConfig PlayerConfig { get; }
    }
}