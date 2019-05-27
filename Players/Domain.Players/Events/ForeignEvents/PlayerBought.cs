using Microwave.Domain.Identities;
using Microwave.Queries;

namespace Domain.Players.Events.ForeignEvents
{
    public class PlayerBought : ISubscribedDomainEvent
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

        public StringIdentity PlayerTypeId { get; }
        public GuidIdentity PlayerId { get; }
        public GuidIdentity TeamId { get; }
        public Identity EntityId => TeamId;
    }
}