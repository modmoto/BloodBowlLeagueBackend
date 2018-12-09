using System;
using System.Collections.Generic;
using Microwave.Domain;

namespace Querries.Teams.DomainEvents
{
    public class TeamCreated : IDomainEvent
    {
        public Guid EntityId{ get; set; }
        public Guid RaceId{ get; set; }
        public string TeamName{ get; set; }
        public string TrainerName{ get; set; }
        public IEnumerable<AllowedPlayer> AllowedPlayers{ get; set; }
    }
}