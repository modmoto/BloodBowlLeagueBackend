using System;
using Microwave.Domain;
using Microwave.Queries;
using Teams.ReadHost.Teams.TeamReadmodels.Events;

namespace Teams.ReadHost.Teams.TeamReadmodels
{
    public class TeamOverviewReadModel : ReadModel, IHandle<TeamCreated>
    {
        public Identity RaceId { get; set; }
        public string TrainerName { get; set; }
        public string TeamName { get; set; }
        public Identity TeamId { get; set; }

        public void Handle(TeamCreated domainEvent)
        {
            TeamId = domainEvent.EntityId;
            RaceId = domainEvent.RaceId;
            TeamName = domainEvent.TeamName;
            TrainerName = domainEvent.TrainerName;
        }

        public override Type GetsCreatedOn => typeof(TeamCreated);
    }
}