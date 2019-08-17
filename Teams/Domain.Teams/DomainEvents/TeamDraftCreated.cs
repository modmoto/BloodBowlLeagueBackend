using System.Collections.Generic;
using Microwave.Domain.EventSourcing;
using Microwave.Domain.Identities;

namespace Domain.Teams.DomainEvents
{
    public class TeamDraftCreated : IDomainEvent
    {
        public Identity EntityId => TeamId;
        public GuidIdentity TeamId { get; }
        public StringIdentity RaceId { get; }
        public string TeamName { get; }
        public string TrainerName { get; }
        public IEnumerable<AllowedPlayer> AllowedPlayers { get; }
        public GoldCoins StartingMoney { get; }

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
    }
}