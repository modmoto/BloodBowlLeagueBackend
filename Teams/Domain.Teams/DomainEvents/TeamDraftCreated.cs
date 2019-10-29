using System;
using System.Collections.Generic;
using Microwave.Domain.EventSourcing;

namespace Domain.Teams.DomainEvents
{
    public class TeamDraftCreated : IDomainEvent
    {
        public string EntityId => TeamId.ToString();
        public Guid TeamId { get; }
        public string RaceId { get; }
        public string TeamName { get; }
        public string TrainerName { get; }
        public IEnumerable<AllowedPlayer> AllowedPlayers { get; }
        public GoldCoins StartingMoney { get; }

        public TeamDraftCreated(
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
    }
}