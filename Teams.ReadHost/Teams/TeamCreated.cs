using System.Collections.Generic;
using Microwave.Domain;

namespace Teams.ReadHost.Teams
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
        public Identity EntityId{ get; set; }
        public Identity RaceId{ get; set; }
        public string TeamName{ get; set; }
        public string TrainerName{ get; set; }
        public IEnumerable<AllowedPlayer> AllowedPlayers{ get; set; }
    }
}