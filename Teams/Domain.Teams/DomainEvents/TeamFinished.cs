using Microwave.Domain.EventSourcing;
using Microwave.Domain.Identities;

namespace Domain.Teams.DomainEvents
{
    public class TeamFinished : IDomainEvent
    {
        public TeamFinished(GuidIdentity teamId)
        {
            TeamId = teamId;
        }

        public Identity EntityId => TeamId;
        public GuidIdentity TeamId { get; }
    }
}