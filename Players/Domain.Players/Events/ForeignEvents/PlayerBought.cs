using Microwave.Domain;

namespace Domain.Players.Events.ForeignEvents
{
    public class PlayerBought : IDomainEvent
    {
        public PlayerBought(
            GuidIdentity entityId,
            StringIdentity playerTypeId,
            GuidIdentity playerId)
        {
            EntityId = entityId;
            PlayerTypeId = playerTypeId;
            PlayerId = playerId;
        }

        public Identity EntityId { get; }
        public StringIdentity PlayerTypeId { get; }
        public GuidIdentity PlayerId { get; }
    }
}