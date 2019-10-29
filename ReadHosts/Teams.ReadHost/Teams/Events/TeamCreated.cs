using System;
using System.Collections.Generic;
using Microwave.Queries;

namespace Teams.ReadHost.Teams.Events
{
    public class TeamCreated : ISubscribedDomainEvent
    {
        public TeamCreated(
            Guid teamId,
            string raceId,
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

        public string EntityId => TeamId.ToString();
        public string RaceId { get; }
        public string TeamName { get; }
        public string TrainerName { get; }
        public IEnumerable<AllowedPlayer> AllowedPlayers { get; }
        public GoldCoins StartingMoney { get; }
        public Guid TeamId { get; }
    }
}