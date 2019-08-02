using Microwave.Domain.Identities;
using Microwave.Queries;

namespace Seasons.ReadHost.Seasons.Events
{
    public class TeamAddedToSeason : ISubscribedDomainEvent
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