using System;
using Microwave.Queries;

namespace Seasons.ReadHost.Matches.Events
{
    public class MatchFinished : ISubscribedDomainEvent
    {
        public Guid MatchId { get; set; }
        public string EntityId => MatchId.ToString();
        public GameResult GameResult { get; set; }
    }
}