using System;
using System.Collections.Generic;
using Microwave.Domain.EventSourcing;

namespace Domain.Seasons.Events
{
    public class SeasonStarted : IDomainEvent
    {
        public SeasonStarted(Guid seasonId, IEnumerable<GameDay> gameDays, DateTimeOffset startDate)
        {
            SeasonId = seasonId;
            GameDays = gameDays;
            StartDate = startDate;
        }

        public string EntityId => SeasonId.ToString();
        public Guid SeasonId { get; }
        public IEnumerable<GameDay> GameDays { get; }
        public DateTimeOffset StartDate { get; }
    }
}