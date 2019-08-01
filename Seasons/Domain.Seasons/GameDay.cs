using System.Collections.Generic;
using Domain.Seasons.Events;
using Microwave.Domain.Identities;
using Microwave.Domain.Validation;

namespace Domain.Seasons
{
    public class GameDay
    {
        public GuidIdentity Id { get; private set; }
        public IEnumerable<Matchup> Matchups { get; private set; }

        public static DomainResult Create(GuidIdentity seasonId, List<Matchup> matchups)
        {
            return DomainResult.Ok(new GameDayCreated(seasonId, GuidIdentity.Create(), matchups));
        }

        public void Apply(GameDayCreated domainEvent)
        {
            Id = domainEvent.GameDayId;
            Matchups = domainEvent.Matchups;
        }
    }
}