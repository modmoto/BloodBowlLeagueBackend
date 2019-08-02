using Microwave.Domain.Identities;

namespace Domain.Seasons
{
    public class Matchup
    {
        public GuidIdentity MatchId { get; }
        public GuidIdentity TeamAtHome { get; }
        public GuidIdentity TeamAsGuest { get; }

        public Matchup(GuidIdentity matchId, GuidIdentity teamAtHome, GuidIdentity teamAsGuest)
        {
            MatchId = matchId;
            TeamAtHome = teamAtHome;
            TeamAsGuest = teamAsGuest;
        }

        public static Matchup Create(GuidIdentity teamAtHome, GuidIdentity teamAsGuest)
        {
            return new Matchup(GuidIdentity.Create(), teamAtHome, teamAsGuest);
        }
    }
}