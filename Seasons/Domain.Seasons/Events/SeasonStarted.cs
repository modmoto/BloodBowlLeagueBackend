using System.Collections.Generic;
using Microwave.Domain;

namespace Domain.Seasons.Events
{
    public class SeasonStarted : IDomainEvent
    {
        public SeasonStarted(GuidIdentity seasonId, IEnumerable<GameDay> gameDays)
        {
            SeasonId = seasonId;
            GameDays = gameDays;
        }

        public Identity EntityId => SeasonId;
        public GuidIdentity SeasonId { get; }
        public IEnumerable<GameDay> GameDays { get; }
    }
}