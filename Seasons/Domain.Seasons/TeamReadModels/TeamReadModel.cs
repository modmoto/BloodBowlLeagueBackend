using Microwave.Domain.Identities;
using Microwave.Queries;

namespace Domain.Seasons.TeamReadModels
{
    public class TeamReadModel : ReadModel<TeamCreated>,
        IHandle<TeamCreated>,
        IHandle<TeamFinished>
    {
        public GuidIdentity TeamId { get; set; }
        public bool IsFinished { get; set; }

        public void Handle(TeamCreated domainEvent)
        {
            TeamId = domainEvent.TeamId;
        }

        public void Handle(TeamFinished domainEvent)
        {
            IsFinished = true;
        }
    }
}