namespace Seasons.ReadHost.Matches
{
    public class GameResult
    {
        public bool IsDraw { get; set; }
        public PointsOfTeam HomeTeam { get; set; }
        public PointsOfTeam GuestTeam { get; set; }
        public string Winner { get; set; }
        public string Looser { get; set; }
    }

    public class PointsOfTeam
    {
        public string TeamId { get; set; }
        public long TouchDowns { get; set; }
    }
}