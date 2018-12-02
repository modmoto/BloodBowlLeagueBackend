using System;
using System.Collections.Generic;
using Microwave.Domain;

namespace Domain.Teams.DomainEvents
{
    public class TeamCreated : IDomainEvent
    {
        [ActualPropertyName("Id")]
        public Guid EntityId { get; }
        public Guid RaceId { get; }
        public string TeamName { get; }
        public string TrainerName { get; }
        public IEnumerable<AllowedPlayer> AllowedPlayers { get; }

        public TeamCreated(Guid teamId, Guid raceId, string teamName, string trainerName, IEnumerable<AllowedPlayer> allowedPlayers)
        {
            EntityId = teamId;
            RaceId = raceId;
            TeamName = teamName;
            TrainerName = trainerName;
            AllowedPlayers = allowedPlayers;
        }
    }
}