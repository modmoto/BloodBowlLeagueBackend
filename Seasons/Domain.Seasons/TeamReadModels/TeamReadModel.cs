using System;
using Microwave.Queries;

namespace Domain.Seasons.TeamReadModels
{
    public class TeamReadModel : ReadModel<TeamCreated>,
        IHandle<TeamCreated>
    {
        public Guid TeamId { get; set; }

        public void Handle(TeamCreated domainEvent)
        {
            TeamId = domainEvent.TeamId;
        }
    }
}