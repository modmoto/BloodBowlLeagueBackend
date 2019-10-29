using System;
using System.Collections.Generic;
using Microwave.Domain;
using Microwave.Domain.EventSourcing;

namespace Domain.Matches.Events
{
    public class MatchStarted : IDomainEvent
    {
        public IEnumerable<Guid>  HomeTeam { get; }
        public IEnumerable<Guid>  GuestTeam { get; }

        public string EntityId => MatchId;
        public Guid MatchId { get; }

        public MatchStarted(Guid matchId, IEnumerable<Guid> homeTeam, IEnumerable<Guid>
        guestTeam)
        {
            MatchId = matchId;
            HomeTeam = homeTeam;
            GuestTeam = guestTeam;
        }

    }
}