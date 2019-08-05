using Microwave.Domain.Identities;
using Seasons.ReadHost.Matches;

namespace Seasons.ReadHost.Seasons
{
    public class MatchupDto
    {
        public GuidIdentity MatchId { get; set; }
        public GuidIdentity TeamAtHome { get; set; }
        public GuidIdentity TeamAsGuest { get; set; }
        public GameResult Result { get; set; }
    }
}