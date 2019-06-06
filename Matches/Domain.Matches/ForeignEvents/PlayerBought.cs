using Microwave.Domain.Identities;
using Microwave.Queries;

namespace Domain.Matches.ForeignEvents
{
    public class PlayerBought : ISubscribedDomainEvent
    {
        public PlayerBought(GuidIdentity teamId, GuidIdentity playerId)
        {
            TeamId = teamId;
            PlayerId = playerId;
        }

        public GuidIdentity TeamId { get; }
        public GuidIdentity PlayerId { get; }
        public Identity EntityId => TeamId;
    }
}