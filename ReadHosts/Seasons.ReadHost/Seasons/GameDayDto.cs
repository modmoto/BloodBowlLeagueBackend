namespace Seasons.ReadHost.Seasons
{
    public class GameDayDto
    {
        public Guid Id { get; set; }
        public IEnumerable<MatchupDto> Matchups { get; set; }
    }

    public class MatchupDto
    {
        public Guid MatchId { get; set; }
    }
}