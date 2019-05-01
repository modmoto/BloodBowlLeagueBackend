﻿using System;
using System.Collections.Generic;
using Microwave.Domain;
using Microwave.Queries;
using Teams.ReadHost.Teams;

namespace Teams.ReadHost.Races
{
    public class RaceReadModel : ReadModel, IHandle<RaceCreated>
    {
        public StringIdentity RaceId { get; set; }
        public string RaceDescription { get; set; }
        public IEnumerable<AllowedPlayer> AllowedPlayers { get; set; }

        public void Handle(RaceCreated domainEvent)
        {
            RaceId = domainEvent.RaceId;
            AllowedPlayers = domainEvent.AllowedPlayers;
            RaceDescription = domainEvent.RaceDescription;
        }

        public override Type GetsCreatedOn => typeof(RaceCreated);
    }
}