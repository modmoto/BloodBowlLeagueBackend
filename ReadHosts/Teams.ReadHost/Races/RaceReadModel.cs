using System;
using System.Collections.Generic;
using Microwave.Domain;
using Microwave.Queries;
using Teams.ReadHost.Teams;
using Teams.ReadHost.Teams.TeamReadmodels;

namespace Teams.ReadHost.Races
{
    public class RaceReadModel : ReadModel, IHandle<RaceCreated>
    {
        public Identity RaceId { get; set; }
        public string RaceDescription { get; set; }
        public IEnumerable<AllowedPlayer> AllowedPlayers { get; set; }

        public void Handle(RaceCreated domainEvent)
        {
            RaceId = domainEvent.EntityId;
            AllowedPlayers = domainEvent.AllowedPlayers;
            RaceDescription = domainEvent.RaceDescription;
        }

        public override Type GetsCreatedOn => typeof(RaceCreated);
    }
}