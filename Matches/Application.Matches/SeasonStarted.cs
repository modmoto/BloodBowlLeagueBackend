using System.Collections.Generic;
using Microwave.Domain.Identities;
using Microwave.Queries;

namespace Application.Matches
{
    public class SeasonStarted : ISubscribedDomainEvent
    {
        public SeasonStarted(GuidIdentity seasonId, IEnumerable<GameDayDto> gameDays)
        {
            SeasonId = seasonId;
            GameDays = gameDays;
        }

        public GuidIdentity SeasonId { get; }
        public IEnumerable<GameDayDto> GameDays { get; }
        public Identity EntityId => SeasonId;
    }

    public class GameDayDto
    {
        public IEnumerable<MatchupReadDto> Matchups { get; set; }
    }

    public class MatchupReadDto
    {
        public GuidIdentity MatchId { get; set; }
        public GuidIdentity TeamAtHome { get; set; }
        public GuidIdentity TeamAsGuest { get; set; }
    }
}