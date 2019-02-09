using System;
using System.Collections.Generic;
using Microwave.Queries;
using Teams.ReadHost.Teams.PlayerReadModels;
using Teams.ReadHost.Teams.TeamReadmodels.Events;

namespace Teams.ReadHost.Teams.TeamReadmodels
{
    public class TeamReadModelFull : ReadModel
    {
        public IEnumerable<PlayerReadModel> PlayerList { get; set; }
        public TeamReadModel Team { get; set; }

        public override Type GetsCreatedOn => typeof(TeamCreated);
    }
}