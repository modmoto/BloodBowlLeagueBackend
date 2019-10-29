using System;
using Microwave.Queries;

namespace Seasons.ReadHost.Matches.Events
{
    public class MatchProgressed : ISubscribedDomainEvent
    {
        public string EntityId => MatchId.ToString();
        public Guid MatchId { get; set; }
        public PlayerProgression PlayerProgression { get; set; }
        public GameResult GameResult { get; set; }
    }
}