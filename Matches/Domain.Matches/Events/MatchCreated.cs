using Microwave.Domain.EventSourcing;
using Microwave.Domain.Identities;

namespace Domain.Matches.Events
{
    public class MatchCreated : IDomainEvent
    {
        public Identity EntityId => MatchId;
        public GuidIdentity TrainerAtHome { get; }
        public GuidIdentity TrainerAsGuest { get; }
        public GuidIdentity MatchId { get; }

        public MatchCreated(GuidIdentity matchId, GuidIdentity trainerAtHome, GuidIdentity trainerAsGuest)
        {
            MatchId = matchId;
            TrainerAtHome = trainerAtHome;
            TrainerAsGuest = trainerAsGuest;
        }
    }
}