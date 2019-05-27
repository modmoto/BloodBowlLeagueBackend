using Microwave.Domain.Identities;

namespace Domain.Seasons
{
    public class MatchupReadModel
    {
        public GuidIdentity TeamAtHome { get; }
        public GuidIdentity TeamAsGuest { get; }

        public MatchupReadModel(GuidIdentity teamAtHome, GuidIdentity teamAsGuest)
        {
            TeamAtHome = teamAtHome;
            TeamAsGuest = teamAsGuest;
        }
    }
}