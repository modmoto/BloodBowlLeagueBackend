using System;
using System.Collections.Generic;
using System.Linq;
using Microwave.Queries;
using Teams.ReadHost.Teams.Events;

namespace Teams.ReadHost.Teams
{
    public class TeamReadModel : ReadModel<TeamDraftCreated>,
        IHandle<TeamDraftCreated>
    {
        public string RaceId { get; set; }
        public string TrainerName { get; set; }
        public string TeamName { get; set; }
        public Guid TeamId { get; set; }
        public bool IsFinished { get; set; }

        public void Handle(TeamDraftCreated domainEvent)
        {
            TeamId = domainEvent.TeamId;
            RaceId = domainEvent.RaceId;
            TeamName = domainEvent.TeamName;
            TrainerName = domainEvent.TrainerName;
        }
    }
}