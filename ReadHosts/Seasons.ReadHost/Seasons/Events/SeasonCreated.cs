using Microwave.Domain.Identities;
using Microwave.Queries;

namespace Seasons.ReadHost.Seasons.Events
{
    public class SeasonCreated : ISubscribedDomainEvent
    {
        public Identity EntityId => SeasonId;
        public GuidIdentity SeasonId { get; }

        public SeasonCreated(GuidIdentity seasonId)
        {
            SeasonId = seasonId;
        }
    }
}