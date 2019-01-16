using Microwave.Domain;

namespace Domain.Players.Events.Players
{
    public class PlayerCreated : IDomainEvent
    {
        public PlayerCreated(
            GuidIdentity entityId,
            StringIdentity playerTypeId,
            PlayerConfig playerConfig)
        {
            EntityId = entityId;
            PlayerTypeId = playerTypeId;
            PlayerConfig = playerConfig;
        }

        public Identity EntityId { get; }
        public StringIdentity PlayerTypeId { get; }
        public PlayerConfig PlayerConfig { get; }
    }
}