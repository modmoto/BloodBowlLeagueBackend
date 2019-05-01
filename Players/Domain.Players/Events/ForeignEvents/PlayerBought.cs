using Microwave.Domain;

namespace Domain.Players.Events.ForeignEvents
{
    public class PlayerBought : IDomainEvent
    {
        public PlayerBought(
            GuidIdentity teamId,
            StringIdentity playerTypeId,
            GuidIdentity playerId)
        {
            TeamId = teamId;
            PlayerTypeId = playerTypeId;
            PlayerId = playerId;
        }

        public Identity EntityId => TeamId;
        public StringIdentity PlayerTypeId { get; }
        public GuidIdentity PlayerId { get; }
        public GuidIdentity TeamId { get; }
    }
}