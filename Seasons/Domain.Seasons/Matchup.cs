using Microwave.Domain.Identities;

namespace Domain.Seasons
{
    public class Matchup
    {
        public GuidIdentity TeamAtHome { get; }
        public GuidIdentity TeamAsGuest { get; }

        public Matchup(GuidIdentity teamAtHome, GuidIdentity teamAsGuest)
        {
            TeamAtHome = teamAtHome;
            TeamAsGuest = teamAsGuest;
        }
    }
}