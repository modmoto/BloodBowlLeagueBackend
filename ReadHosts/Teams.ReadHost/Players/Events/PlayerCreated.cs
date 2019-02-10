using Microwave.Domain;

namespace Teams.ReadHost.Players.Events
{
    public class PlayerCreated : IDomainEvent
    {
        public PlayerCreated(
            GuidIdentity entityId,
            StringIdentity playerTypeId)
        {
            EntityId = entityId;
            PlayerTypeId = playerTypeId;
        }

        public Identity EntityId { get; }
        public StringIdentity PlayerTypeId { get; }
    }
}