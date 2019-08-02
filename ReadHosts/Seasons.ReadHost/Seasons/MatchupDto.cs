using Microwave.Domain.Identities;

namespace Seasons.ReadHost.Seasons
{
    public class MatchupDto
    {
        public GuidIdentity MatchId { get; set; }
        public GuidIdentity TeamAtHome { get; set; }
        public GuidIdentity TeamAsGuest { get; set; }
    }
}