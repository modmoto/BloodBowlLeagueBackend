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
        public IEnumerable<AllowedPlayer> AllowerPlayersOnCreation { get; }

        public TeamCreated(Guid teamId, Guid raceId, string teamName, string trainerName, IEnumerable<AllowedPlayer> allowerPlayersOnCreation)
        {
            EntityId = teamId;
            RaceId = raceId;
            TeamName = teamName;
            TrainerName = trainerName;
            AllowerPlayersOnCreation = allowerPlayersOnCreation;
        }
    }
}