using Microwave.Domain;

namespace Teams.ReadHost.Players.Events
{
    public class PlayerCreated : IDomainEvent
    {
        public PlayerCreated(
            GuidIdentity entityId,
            StringIdentity playerTypeId,
            GuidIdentity teamId)
        {
            EntityId = entityId;
            PlayerTypeId = playerTypeId;
            TeamId = teamId;
        }

        public Identity EntityId { get; }
        public StringIdentity PlayerTypeId { get; }
        public GuidIdentity TeamId { get; }
    }
}