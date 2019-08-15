using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Teams.DomainErrors;
using Domain.Teams.DomainEvents;
using Microwave.Domain.EventSourcing;
using Microwave.Domain.Identities;
using Microwave.Domain.Validation;

namespace Domain.Teams
{
    public class Team : Entity, IApply<TeamCreated>, IApply<PlayerBought>
    {
        public GuidIdentity TeamId { get; private set; }

        public GoldCoins TeamMoney { get; private set; }
        public IEnumerable<StringIdentity> PlayerTypes { get; private set; } = new List<StringIdentity>();
        public IEnumerable<AllowedPlayer> AllowedPlayers { get; private set; } = new List<AllowedPlayer>();

        public static DomainResult Create(StringIdentity raceId, string teamName, string trainerName, IEnumerable<AllowedPlayer>
        allowedPlayers)
        {
            return DomainResult.Ok(new TeamCreated(
                GuidIdentity.Create(Guid.NewGuid()),
                raceId, 
                teamName,
                trainerName, 
                allowedPlayers, 
                new GoldCoins(1000000)));
        }

        public DomainResult BuyPlayer(StringIdentity playerTypeId)
        {
            var playerBuyConfig = AllowedPlayers.FirstOrDefault(ap => ap.PlayerTypeId.Equals(playerTypeId));
            if (playerBuyConfig == null) return DomainResult.Error(new CanNotUsePlayerInThisRaceError(playerTypeId));

            var amountOfPlayerTypeToBuy = PlayerTypes.Count(p => p.Equals(playerTypeId));
            if (amountOfPlayerTypeToBuy >= playerBuyConfig.MaximumPlayers) return DomainResult.Error(new TeamFullError(playerBuyConfig.MaximumPlayers));

            if (playerBuyConfig.Cost.MoreThan(TeamMoney))
                return DomainResult.Error(new FewMoneyInTeamChestError(playerBuyConfig.Cost.Value, TeamMoney.Value));

            var newTeamMoney = TeamMoney.Minus(playerBuyConfig.Cost);
            var playerBought = new PlayerBought(TeamId, playerTypeId, GuidIdentity.Create(), newTeamMoney);
            return DomainResult.Ok(playerBought);
        }

        public void Apply(TeamCreated domainEvent)
        {
            TeamId = domainEvent.TeamId;
            AllowedPlayers = domainEvent.AllowedPlayers;
            TeamMoney = domainEvent.StartingMoney;
        }

        public void Apply(PlayerBought domainEvent)
        {
            TeamMoney = domainEvent.NewTeamChestBalance;
            PlayerTypes = PlayerTypes.Append(domainEvent.PlayerTypeId);
        }
    }
}