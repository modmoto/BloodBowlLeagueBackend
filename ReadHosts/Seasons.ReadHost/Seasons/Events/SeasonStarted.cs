namespace Seasons.ReadHost.Seasons.Events
{
    public class SeasonStarted : ISubscribedDomainEvent
    {
        public SeasonStarted(Guid seasonId, IEnumerable<GameDayDto> gameDays, DateTimeOffset startDate)
        {
            SeasonId = seasonId;
            GameDays = gameDays;
            StartDate = startDate;
        }

        public string EntityId => SeasonId;
        public Guid SeasonId { get; }
        public IEnumerable<GameDayDto> GameDays { get; }
        public DateTimeOffset StartDate { get; }
    }
}