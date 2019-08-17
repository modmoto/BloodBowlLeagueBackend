using Microwave.Domain.Identities;
using Microwave.Queries;

namespace Domain.Matches.ForeignEvents
{
    public class PlayerRemoved : ISubscribedDomainEvent
    {
        public GuidIdentity TeamId { get; }

        public Identity EntityId => TeamId;
        public GuidIdentity PlayerId { get; }

        public PlayerRemoved(GuidIdentity teamId, GuidIdentity playerId)
        {
            TeamId = teamId;
            PlayerId = playerId;
        }
    }
}