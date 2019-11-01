using System;

namespace Seasons.ReadHost.Matches
{
    public class MinimalMatchHto
    {
        public Guid MatchId { get; }
        public string GuestTeamName { get; }
        public string HomeTeamName { get; }

        public MinimalMatchHto(Guid matchId, string guestTeamName, string homeTeamName)
        {
            MatchId = matchId;
            GuestTeamName = guestTeamName;
            HomeTeamName = homeTeamName;
        }
    }
}