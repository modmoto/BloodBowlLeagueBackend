using System;
using Microwave.Domain.Identities;
using Microwave.Queries;

namespace Application.Matches.TeamReadModels
{
    public class TeamReadModel : ReadModel, IHandle<TeamCreated>
    {
        public GuidIdentity TeamId { get; private set; }

        public override Type GetsCreatedOn => typeof(TeamCreated);

        public void Handle(TeamCreated domainEvent)
        {
            TeamId = domainEvent.TeamId;
        }
    }
}