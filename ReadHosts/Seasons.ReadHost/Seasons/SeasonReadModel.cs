using Seasons.ReadHost.Seasons.Events;

namespace Seasons.ReadHost.Seasons
{
    public class SeasonReadModel : ReadModel<SeasonCreated>,
        IHandle<SeasonCreated>,
        IHandle<SeasonStarted>,
        IHandle<TeamAddedToSeason>
    {
        public Guid SeasonId { get; set; }
        public IEnumerable<GameDayDto> GameDays { get; set; }
        public IList<Guid> Teams { get; } = new List<Guid>();

        public string SeasonName { get; set; }

        public bool IsStarted { get; set; }

        public void Handle(SeasonCreated domainEvent)
        {
            SeasonId = domainEvent.SeasonId;
            SeasonName = domainEvent.SeasonName;
        }

        public void Handle(SeasonStarted domainEvent)
        {
            IsStarted = true;
            GameDays = domainEvent.GameDays;
        }

        public void Handle(TeamAddedToSeason domainEvent)
        {
            Teams.Add(domainEvent.TeamId);
        }
    }
}