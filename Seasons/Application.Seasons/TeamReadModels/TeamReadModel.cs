using Microwave.Domain.Identities;
using Microwave.Queries;

namespace Application.Matches.TeamReadModels
{
    public class TeamReadModel : ReadModel<TeamCreated>, IHandle<TeamCreated>
    {
        public GuidIdentity TeamId { get; private set; }

        public void Handle(TeamCreated domainEvent)
        {
            TeamId = domainEvent.TeamId;
        }
    }
}