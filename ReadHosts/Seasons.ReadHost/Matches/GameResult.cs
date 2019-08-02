using Microwave.Domain.Identities;

namespace Seasons.ReadHost.Matches
{
    public class GameResult
    {
        public bool IsDraw { get; set; }
        public PointsOfTeam Team { get; set; }
        public PointsOfTeam Looser { get; set; }
    }

    public class PointsOfTeam
    {
        public Identity TeamId { get; set; }
        public long TouchDowns { get; set; }
    }
}