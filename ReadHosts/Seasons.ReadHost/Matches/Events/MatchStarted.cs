using System;
using System.Collections.Generic;
using Microwave.Queries;

namespace Seasons.ReadHost.Matches.Events
{
    public class MatchStarted : ISubscribedDomainEvent
    {
        public IEnumerable<Guid>  HomeTeam { get; set; }
        public IEnumerable<Guid>  GuestTeam { get; set; }

        public string EntityId => MatchId.ToString();
        public Guid MatchId { get; set; }
    }
}