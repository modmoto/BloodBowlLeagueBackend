using Microwave.Domain;

namespace Domain.Seasons
{
    public class MatchupReadModel
    {
        public GuidIdentity MatchId { get; }
        public GuidIdentity TeamAtHome { get; }
        public GuidIdentity TeamAsGuest { get; }

        public MatchupReadModel(GuidIdentity matchId, GuidIdentity teamAtHome, GuidIdentity teamAsGuest)
        {
            MatchId = matchId;
            TeamAtHome = teamAtHome;
            TeamAsGuest = teamAsGuest;
        }
    }
}