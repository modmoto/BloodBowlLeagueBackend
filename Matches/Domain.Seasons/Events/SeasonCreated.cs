using Microwave.Domain;

namespace Domain.Seasons.Events
{
    public class SeasonCreated : IDomainEvent
    {
        public Identity EntityId { get; }

        public SeasonCreated(GuidIdentity entityId)
        {
            EntityId = entityId;
        }
    }
}