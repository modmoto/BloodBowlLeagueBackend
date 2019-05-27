using Microwave.Domain.Identities;
using Microwave.Queries;

namespace Domain.Matches.ForeignEvents
{
    public class PlayerBought : ISubscribedDomainEvent
    {
        public PlayerBought(GuidIdentity matchId, GuidIdentity playerId)
        {
            MatchId = matchId;
            PlayerId = playerId;
        }

        public GuidIdentity MatchId { get; }
        public GuidIdentity PlayerId { get; }
        public Identity EntityId => MatchId;
    }
}