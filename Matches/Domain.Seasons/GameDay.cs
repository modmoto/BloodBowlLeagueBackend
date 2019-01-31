using System.Collections.Generic;
using Domain.Matches;
using Microwave.Domain;

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

    public class GameDayCreated : IDomainEvent
    {
        public GameDayCreated(GuidIdentity entityId, GuidIdentity gameDayId, List<Matchup> matchups)
        {
            EntityId = entityId;
            GameDayId = gameDayId;
            Matchups = matchups;
        }

        public Identity EntityId { get; }
        public GuidIdentity GameDayId { get; }
        public List<Matchup> Matchups { get; }
    }
}