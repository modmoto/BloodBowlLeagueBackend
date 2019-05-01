using System.Collections.Generic;
using Microwave.Domain;

namespace Domain.Matches.Events
{
    public class MatchStarted : IDomainEvent
    {
        public IEnumerable<GuidIdentity>  HomeTeam { get; }
        public IEnumerable<GuidIdentity>  GuestTeam { get; }

        public Identity EntityId => MatchId;
        public GuidIdentity MatchId { get; }

        public MatchStarted(GuidIdentity matchId, IEnumerable<GuidIdentity> homeTeam, IEnumerable<GuidIdentity>
        guestTeam)
        {
            MatchId = matchId;
            HomeTeam = homeTeam;
            GuestTeam = guestTeam;
        }

    }
}