using Microwave.Domain;

namespace Domain.Matches.Seasons.Events
{
    public class TeamAddedToSeason : IDomainEvent
    {
        public TeamAddedToSeason(GuidIdentity entityId, GuidIdentity teamId)
        {
            EntityId = entityId;
            TeamId = teamId;
        }

        public Identity EntityId { get; }
        public GuidIdentity TeamId { get; }
    }
}