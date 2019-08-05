using Microwave.Domain.Identities;

namespace Seasons.ReadHost.Matches
{
    public class GameResult
    {
        public bool IsDraw { get; set; }
        public PointsOfTeam HomeTeam { get; set; }
        public PointsOfTeam GuestTeam { get; set; }
        public Identity Winner { get; set; }
        public Identity Looser { get; set; }
    }

    public class PointsOfTeam
    {
        public Identity TeamId { get; set; }
        public long TouchDowns { get; set; }
    }
}