using Microwave.Domain;

namespace Domain.Matches.ForeignEvents
{
    public class PlayerBought : IDomainEvent
    {
        public PlayerBought(GuidIdentity entityId, GuidIdentity playerId)
        {
            EntityId = entityId;
            PlayerId = playerId;
        }

        public Identity EntityId { get; }
        public GuidIdentity PlayerId { get; }
    }
}