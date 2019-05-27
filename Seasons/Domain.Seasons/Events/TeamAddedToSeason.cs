using Microwave.Domain.EventSourcing;
using Microwave.Domain.Identities;

namespace Domain.Seasons.Events
{
    public class TeamAddedToSeason : IDomainEvent
    {
        public TeamAddedToSeason(GuidIdentity seasonId, GuidIdentity teamId)
        {
            SeasonId = seasonId;
            TeamId = teamId;
        }

        public Identity EntityId => SeasonId;
        public GuidIdentity SeasonId { get; }
        public GuidIdentity TeamId { get; }
    }
}