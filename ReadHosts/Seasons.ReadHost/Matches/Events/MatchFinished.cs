namespace Seasons.ReadHost.Matches.Events
{
    public class MatchFinished : ISubscribedDomainEvent
    {
        public Guid MatchId { get; set; }
        public string EntityId => MatchId;
        public GameResult GameResult { get; set; }
    }
}