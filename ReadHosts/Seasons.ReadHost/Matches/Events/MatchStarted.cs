using System.Collections.Generic;
using Microwave.Domain.Identities;
using Microwave.Queries;

namespace Seasons.ReadHost.Matches.Events
{
    public class MatchStarted : ISubscribedDomainEvent
    {
        public IEnumerable<GuidIdentity>  HomeTeam { get; set; }
        public IEnumerable<GuidIdentity>  GuestTeam { get; set; }

        public Identity EntityId => MatchId;
        public GuidIdentity MatchId { get; set; }
    }
}