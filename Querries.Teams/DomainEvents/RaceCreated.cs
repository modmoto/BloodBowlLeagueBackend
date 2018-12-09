using System;
using System.Collections.Generic;
using Microwave.Domain;

namespace Querries.Teams.DomainEvents
{
    public class RaceCreated : IDomainEvent
    {
        public RaceCreated(Guid entityId, IEnumerable<AllowedPlayer> allowedPlayers, string raceDescription)
        {
            EntityId = entityId;
            AllowedPlayers = allowedPlayers;
            RaceDescription = raceDescription;
        }

        public IEnumerable<AllowedPlayer> AllowedPlayers{ get; set; }
        public string RaceDescription{ get; set; }
        public Guid EntityId{ get; set; }
    }
}