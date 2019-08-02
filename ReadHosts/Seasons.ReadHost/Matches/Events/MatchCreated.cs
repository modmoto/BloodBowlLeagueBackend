using Microwave.Domain.Identities;
using Microwave.Queries;

namespace Seasons.ReadHost.Matches.Events
{
    public class MatchCreated : ISubscribedDomainEvent
    {
        public Identity EntityId => MatchId;
        public GuidIdentity TeamAtHome { get; }
        public GuidIdentity TeamAsGuest { get; }
        public GuidIdentity MatchId { get; }

        public MatchCreated(GuidIdentity matchId, GuidIdentity teamAtHome, GuidIdentity teamAsGuest)
        {
            MatchId = matchId;
            TeamAtHome = teamAtHome;
            TeamAsGuest = teamAsGuest;
        }
    }
}