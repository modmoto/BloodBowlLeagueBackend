using System;
using System.Collections.Generic;
using Microwave.Queries;

namespace Application.Matches
{
    public class SeasonStarted : ISubscribedDomainEvent
    {
        public SeasonStarted(Guid seasonId, IEnumerable<GameDayDto> gameDays)
        {
            SeasonId = seasonId;
            GameDays = gameDays;
        }

        public Guid SeasonId { get; }
        public IEnumerable<GameDayDto> GameDays { get; }
        public string EntityId => SeasonId.ToString();
    }

    public class GameDayDto
    {
        public IEnumerable<MatchupReadDto> Matchups { get; set; }
    }

    public class MatchupReadDto
    {
        public Guid MatchId { get; set; }
        public Guid TeamAtHome { get; set; }
        public Guid TeamAsGuest { get; set; }
    }
}