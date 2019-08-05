using Microwave.Domain.Identities;
using Microwave.Queries;

namespace Seasons.ReadHost.Matches.Events
{
    public class MatchFinished : ISubscribedDomainEvent
    {
        public GuidIdentity MatchId { get; set; }
        public Identity EntityId => MatchId;
        public GameResult GameResult { get; set; }
    }
}