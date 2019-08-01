using System.Collections.Generic;
using Microwave.Domain.EventSourcing;
using Microwave.Domain.Identities;

namespace Domain.Seasons.Events
{
    public class GameDayCreated : IDomainEvent
    {
        public GameDayCreated(GuidIdentity seasonId, GuidIdentity gameDayId, List<Matchup> matchups)
        {
            SeasonId = seasonId;
            GameDayId = gameDayId;
            Matchups = matchups;
        }

        public Identity EntityId => SeasonId;
        public GuidIdentity SeasonId { get; }
        public GuidIdentity GameDayId { get; }
        public List<Matchup> Matchups { get; }
    }
}