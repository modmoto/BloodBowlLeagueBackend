using Microwave.Domain.Identities;
using Microwave.Queries;

namespace Seasons.ReadHost.Matches.Events
{
    public class MatchProgressed : ISubscribedDomainEvent
    {
        public Identity EntityId => MatchId;
        public GuidIdentity MatchId { get; set; }
        public PlayerProgression PlayerProgression { get; set; }
        public GameResult GameResult { get; set; }
    }
}