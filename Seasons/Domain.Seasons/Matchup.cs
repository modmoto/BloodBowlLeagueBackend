using System;

namespace Domain.Seasons
{
    public class Matchup
    {
        public Guid MatchId { get; }
        public Guid TeamAtHome { get; }
        public Guid TeamAsGuest { get; }

        public Matchup(Guid matchId, Guid teamAtHome, Guid teamAsGuest)
        {
            MatchId = matchId;
            TeamAtHome = teamAtHome;
            TeamAsGuest = teamAsGuest;
        }

        public static Matchup Create(Guid teamAtHome, Guid teamAsGuest)
        {
            return new Matchup(Guid.NewGuid(), teamAtHome, teamAsGuest);
        }
    }
}