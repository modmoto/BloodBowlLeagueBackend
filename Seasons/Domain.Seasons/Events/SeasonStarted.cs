using System;
using System.Collections.Generic;
using Microwave.Domain.EventSourcing;
using Microwave.Domain.Identities;

namespace Domain.Seasons.Events
{
    public class SeasonStarted : IDomainEvent
    {
        public SeasonStarted(GuidIdentity seasonId, IEnumerable<GameDay> gameDays, DateTimeOffset startDate)
        {
            SeasonId = seasonId;
            GameDays = gameDays;
            StartDate = startDate;
        }

        public Identity EntityId => SeasonId;
        public GuidIdentity SeasonId { get; }
        public IEnumerable<GameDay> GameDays { get; }
        public DateTimeOffset StartDate { get; }
    }
}