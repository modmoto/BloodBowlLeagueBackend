using System.Collections.Generic;
using Microwave.Domain;

namespace Domain.Matches.Seasons.Events
{
    public class SeasonStarted : IDomainEvent
    {
        public SeasonStarted(GuidIdentity entityId, IEnumerable<GameDay> gameDays)
        {
            EntityId = entityId;
            GameDays = gameDays;
        }

        public Identity EntityId { get; }
        public IEnumerable<GameDay> GameDays { get; }
    }
}