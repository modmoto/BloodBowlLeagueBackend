using System.Collections.Generic;
using Microwave.Domain.Identities;
using Microwave.Queries;

namespace Seasons.ReadHost.Seasons.Events
{
    public class SeasonStarted : ISubscribedDomainEvent
    {
        public SeasonStarted(GuidIdentity seasonId, IEnumerable<GameDayDto> gameDays)
        {
            SeasonId = seasonId;
            GameDays = gameDays;
        }

        public Identity EntityId => SeasonId;
        public GuidIdentity SeasonId { get; }
        public IEnumerable<GameDayDto> GameDays { get; }
    }
}