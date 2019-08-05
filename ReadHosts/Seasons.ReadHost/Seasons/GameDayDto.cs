using System.Collections.Generic;
using Microwave.Domain.Identities;

namespace Seasons.ReadHost.Seasons
{
    public class GameDayDto
    {
        public GuidIdentity Id { get; set; }
        public IEnumerable<MatchupDto> Matchups { get; set; }
    }

    public class MatchupDto
    {
        public GuidIdentity MatchId { get; set; }
    }
}