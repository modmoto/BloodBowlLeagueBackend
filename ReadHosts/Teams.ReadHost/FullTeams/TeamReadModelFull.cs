﻿using System;
using System.Collections.Generic;
using Microwave.Queries;
using Teams.ReadHost.Players;
using Teams.ReadHost.Teams;
using Teams.ReadHost.Teams.Events;

namespace Teams.ReadHost.FullTeams
{
    public class TeamReadModelFull : ReadModel
    {
        public IEnumerable<PlayerReadModel> PlayerList { get; set; }
        public TeamReadModel Team { get; set; }

        public override Type GetsCreatedOn => typeof(TeamCreated);
    }
}