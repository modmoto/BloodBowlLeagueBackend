using System;
using Microwave.Queries;
using Seasons.ReadHost.Teams.Events;

namespace Seasons.ReadHost.Teams
{
    public class TeamReadModel : ReadModel<TeamCreated>, IHandle<TeamCreated>
    {
        public string RaceId { get; set; }
        public string TrainerName { get; set; }
        public string TeamName { get; set; }
        public Guid TeamId { get; set; }

        public void Handle(TeamCreated domainEvent)
        {
            TeamId = domainEvent.TeamId;
            RaceId = domainEvent.RaceId;
            TeamName = domainEvent.TeamName;
            TrainerName = domainEvent.TrainerName;
        }
    }
}