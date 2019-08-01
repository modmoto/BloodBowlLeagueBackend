using Microwave.Domain.Identities;
using Microwave.Queries;

namespace Application.Matches.TeamReadModels
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