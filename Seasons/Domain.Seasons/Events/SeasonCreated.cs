using Microwave.Domain.EventSourcing;
using Microwave.Domain.Identities;

namespace Domain.Seasons.Events
{
    public class SeasonCreated : IDomainEvent
    {
        public Identity EntityId => SeasonId;
        public GuidIdentity SeasonId { get; }

        public SeasonCreated(GuidIdentity seasonId)
        {
            SeasonId = seasonId;
        }
    }
}