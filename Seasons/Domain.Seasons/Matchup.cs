using Microwave.Domain;

namespace Domain.Seasons
{
    public class Matchup
    {
        public Matchup(GuidIdentity matchId, GuidIdentity homeTeam, GuidIdentity guestTeam)
        {
            MatchId = matchId;
            HomeTeam = homeTeam;
            GuestTeam = guestTeam;
        }

        public GuidIdentity MatchId { get; }
        public GuidIdentity HomeTeam { get; }
        public GuidIdentity GuestTeam { get; }

        public override string ToString()
        {
            return $"{HomeTeam} vs {GuestTeam}";
        }
    }
}