using Microwave.Domain;

namespace Domain.Matches.ForeignEvents
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