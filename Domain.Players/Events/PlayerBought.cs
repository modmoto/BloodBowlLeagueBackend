using Microwave.Domain;

namespace Domain.Players.Events
{
    public class PlayerBought : IDomainEvent
    {
        public Identity EntityId { get; }
        public GuidIdentity PlayerId { get; }
        public StringIdentity PlayerTypeId { get; }

        public PlayerBought(GuidIdentity entityId, StringIdentity playerTypeId, GuidIdentity playerId)
        {
            EntityId = entityId;
            PlayerTypeId = playerTypeId;
            PlayerId = playerId;
        }
    }
}