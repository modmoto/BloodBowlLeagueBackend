using System.Collections.Generic;
using Microwave.Domain;

namespace Domain.Matches.Matches.Events
{
    public class MatchStarted : IDomainEvent
    {
        public IEnumerable<GuidIdentity>  HomeTeam { get; }
        public IEnumerable<GuidIdentity>  GuestTeam { get; }

        public Identity EntityId { get; }

        public MatchStarted(GuidIdentity entityId, IEnumerable<GuidIdentity> homeTeam, IEnumerable<GuidIdentity> guestTeam)
        {
            EntityId = entityId;
            HomeTeam = homeTeam;
            GuestTeam = guestTeam;
        }

    }
}