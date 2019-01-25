using System.Collections.Generic;
using Microwave.Domain;

namespace Domain.Teams.DomainEvents
{
    public class TeamCreated : IDomainEvent
    {
        public TeamCreated(GuidIdentity entityId, StringIdentity raceId, string teamName, string trainerName,
        IEnumerable<AllowedPlayer> allowedPlayers)
        {
            EntityId = entityId;
            RaceId = raceId;
            TeamName = teamName;
            TrainerName = trainerName;
            AllowedPlayers = allowedPlayers;
        }

        public Identity EntityId { get; }
        public Identity RaceId { get; }
        public string TeamName { get; }
        public string TrainerName { get; }
        public IEnumerable<AllowedPlayer> AllowedPlayers { get; }
    }
}