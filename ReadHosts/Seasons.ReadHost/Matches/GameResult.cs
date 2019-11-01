using System;

namespace Seasons.ReadHost.Matches
{
    public class GameResult
    {
        public bool IsDraw { get; set; }
        public PointsOfTeam HomeTeam { get; set; }
        public PointsOfTeam GuestTeam { get; set; }
        public Guid Winner { get; set; }
        public Guid Looser { get; set; }
    }

    public class PointsOfTeam
    {
        public Guid TeamId { get; set; }
        public long TouchDowns { get; set; }
    }
}