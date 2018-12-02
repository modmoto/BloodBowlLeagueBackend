using System;
using System.Collections.Generic;
using Microwave.Domain;

namespace Domain.Teams.DomainEvents
{
    public class TeamCreated : IDomainEvent
    {
        public Guid EntityId { get; }
        public Guid RaceId { get; }
        public string TeamName { get; }
        public string TrainerName { get; }
        public IEnumerable<AllowedPlayer> AllowedPlayersOnCreation { get; }

        public TeamCreated(Guid teamId, Guid raceId, string teamName, string trainerName, IEnumerable<AllowedPlayer> allowedPlayersOnCreation)
        {
            EntityId = teamId;
            RaceId = raceId;
            TeamName = teamName;
            TrainerName = trainerName;
            AllowedPlayersOnCreation = allowedPlayersOnCreation;
        }
    }
}