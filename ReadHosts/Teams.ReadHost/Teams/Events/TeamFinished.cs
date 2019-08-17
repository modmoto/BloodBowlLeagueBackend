using Microwave.Domain.Identities;
using Microwave.Queries;

namespace Teams.ReadHost.Teams.Events
{
    public class TeamFinished : ISubscribedDomainEvent
    {
        public TeamFinished(GuidIdentity teamId)
        {
            TeamId = teamId;
        }

        public Identity EntityId => TeamId;
        public GuidIdentity TeamId { get; }
    }
}