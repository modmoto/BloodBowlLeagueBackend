namespace Seasons.ReadHost.Seasons.Events
{
    public class TeamAddedToSeason : ISubscribedDomainEvent
    {
        public TeamAddedToSeason(Guid seasonId, Guid teamId)
        {
            SeasonId = seasonId;
            TeamId = teamId;
        }

        public string EntityId => SeasonId;
        public Guid SeasonId { get; }
        public Guid TeamId { get; }
    }
}