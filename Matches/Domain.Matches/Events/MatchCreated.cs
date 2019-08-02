using Microwave.Domain.EventSourcing;
using Microwave.Domain.Identities;

namespace Domain.Matches.Events
{
    public class MatchCreated : IDomainEvent
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