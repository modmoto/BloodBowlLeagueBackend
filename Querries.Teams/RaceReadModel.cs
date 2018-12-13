using System;
using System.Collections.Generic;
using Microwave.Queries;
using Querries.Teams.DomainEvents;

namespace Querries.Teams
{
    [CreateReadmodelOn(typeof(RaceCreated))]
    public class RaceReadModel : ReadModel, IHandle<RaceCreated>
    {
        public Guid RaceId { get; set; }
        public string RaceDescription { get; set; }
        public IEnumerable<AllowedPlayer> AllowedPlayers { get; set; }

        public void Handle(RaceCreated domainEvent)
        {
            RaceId = domainEvent.EntityId;
            AllowedPlayers = domainEvent.AllowedPlayers;
            RaceDescription = domainEvent.RaceDescription;
        }
    }
}