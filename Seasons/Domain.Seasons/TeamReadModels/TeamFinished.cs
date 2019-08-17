using Microwave.Domain.Identities;
using Microwave.Queries;

namespace Domain.Seasons.TeamReadModels
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