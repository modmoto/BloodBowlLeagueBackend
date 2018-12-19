using System;
using System.Collections.Generic;
using Microwave.Domain;

namespace Domain.Teams.DomainEvents
{
    public class TeamCreated : IDomainEvent
    {
        public TeamCreated(Guid entityId, Guid raceId, string teamName, string trainerName, IEnumerable<AllowedPlayer> allowedPlayers)
        {
            EntityId = entityId;
            RaceId = raceId;
            TeamName = teamName;
            TrainerName = trainerName;
            AllowedPlayers = allowedPlayers;
        }

        public Guid EntityId { get; }
        public Guid RaceId { get; }
        public string TeamName { get; }
        public string TrainerName { get; }
        public IEnumerable<AllowedPlayer> AllowedPlayers { get; }
    }
}