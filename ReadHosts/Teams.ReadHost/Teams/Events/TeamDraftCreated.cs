using System.Collections.Generic;
using Microwave.Domain.Identities;
using Microwave.Queries;

namespace Teams.ReadHost.Teams.Events
{
    public class TeamDraftCreated : ISubscribedDomainEvent
    {
        public TeamDraftCreated(
            GuidIdentity teamId,
            StringIdentity raceId,
            string teamName,
            string trainerName,
            IEnumerable<AllowedPlayer> allowedPlayers,
            GoldCoins startingMoney)
        {
            TeamId = teamId;
            RaceId = raceId;
            TeamName = teamName;
            TrainerName = trainerName;
            AllowedPlayers = allowedPlayers;
            StartingMoney = startingMoney;
        }

        public Identity EntityId => TeamId;
        public StringIdentity RaceId { get; }
        public string TeamName { get; }
        public string TrainerName { get; }
        public IEnumerable<AllowedPlayer> AllowedPlayers { get; }
        public GoldCoins StartingMoney { get; }
        public GuidIdentity TeamId { get; }
    }
}