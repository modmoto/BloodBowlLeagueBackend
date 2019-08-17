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
    public class Team : Entity,
        IApply<TeamCreated>,
        IApply<PlayerBought>,
        IApply<PlayerRemoved>,
        IApply<TeamFinished>
    {
        public GuidIdentity TeamId { get; private set; }
        public GoldCoins TeamMoney { get; private set; }
        public IEnumerable<PlayerReadModel> Players { get; private set; } = new List<PlayerReadModel>();
        public IEnumerable<AllowedPlayer> AllowedPlayers { get; private set; } = new List<AllowedPlayer>();

        public bool IsFinished { get; private set; }

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

            var amountOfPlayerTypeToBuy = Players.Count(p => p.PlayerTypeId.Equals(playerTypeId));
            if (amountOfPlayerTypeToBuy >= playerBuyConfig.MaximumPlayers) return DomainResult.Error(new TeamFullError(playerBuyConfig.MaximumPlayers));

            if (playerBuyConfig.Cost.MoreThan(TeamMoney))
                return DomainResult.Error(new FewMoneyInTeamChestError(playerBuyConfig.Cost.Value, TeamMoney.Value));

            var newTeamMoney = TeamMoney.Minus(playerBuyConfig.Cost);
            var playerBought = new PlayerBought(TeamId, playerTypeId, GuidIdentity.Create(), newTeamMoney);
            return DomainResult.Ok(playerBought);
        }
        public DomainResult Finish()
        {
            return Players.Count() < 11
                ? DomainResult.Error(new TeamDoesNeedMorePlayersToFinish(Players.Count()))
                : DomainResult.Ok(new TeamFinished(TeamId));
        }

        public DomainResult RemovePlayer(GuidIdentity playerId)
        {
            if (IsFinished) return DomainResult.Error(new CanOnlyRemovePlayerFromDraftTeam());

            var playerReadModel = Players.Single(p => p.PlayerId == playerId);
            var playerBuyConfig = AllowedPlayers.Single(ap => ap.PlayerTypeId.Equals(playerReadModel.PlayerTypeId));
            var newTeamMoney = TeamMoney.Plus(playerBuyConfig.Cost);
            return DomainResult.Ok(new PlayerRemoved(TeamId, playerId, newTeamMoney));
        }

        public void Apply(PlayerRemoved domainEvent)
        {
            var playerReadModels = Players.Where(p => p.PlayerId != domainEvent.PlayerId);
            Players = playerReadModels;
            TeamMoney = domainEvent.NewTeamChestBalance;
        }

        public void Apply(TeamFinished domainEvent)
        {
            IsFinished = true;
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
            var playerReadModel = new PlayerReadModel(domainEvent.PlayerId, domainEvent.PlayerTypeId);
            Players = Players.Append(playerReadModel);
        }
    }
}