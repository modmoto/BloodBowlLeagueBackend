using Microwave.Domain;

namespace Domain.Seasons.ForeignEvents
{
    public class TeamCreated : IDomainEvent
    {
        public TeamCreated(GuidIdentity entityId)
        {
            EntityId = entityId;
        }

        public Identity EntityId{ get; }
    }
}