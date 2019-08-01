using Microwave.Domain.Identities;
using Microwave.Queries;

namespace Domain.Seasons.ForeignEvents
{
    public class TeamCreated : ISubscribedDomainEvent
    {
        public TeamCreated(GuidIdentity teamId)
        {
            TeamId = teamId;
        }
        public GuidIdentity TeamId { get; }
        public Identity EntityId => TeamId;
    }
}