using Microwave.Domain;

namespace Domain.Seasons.ForeignEvents
{
    public class TeamCreated : IDomainEvent
    {
        public TeamCreated(GuidIdentity teamId)
        {
            TeamId = teamId;
        }

        public Identity EntityId => TeamId;
        public GuidIdentity TeamId { get; }
    }
}