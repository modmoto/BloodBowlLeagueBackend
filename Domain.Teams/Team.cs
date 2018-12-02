using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Teams.DomainErrors;
using Domain.Teams.DomainEvents;
using Microwave.Domain;

namespace Domain.Teams
{
    public class Team : IApply<TeamCreated>, IApply<PlayerBought>
    {
        public Guid Id { get; private set; }
        public Guid RaceId { get; private set; }

        public GoldCoins TeamMoney { get; private set; } = new GoldCoins(1000000);
        public string TeamName { get; private set; }
        public string TrainerName { get; private set; }
        public IEnumerable<PlayerDto> Players { get; private set; } = new List<PlayerDto>();
        public IEnumerable<AllowedPlayer> AllowedPlayers { get; private set; } = new List<AllowedPlayer>();

        public static DomainResult Create(Guid raceId, string teamName, string trainerName, IEnumerable<AllowedPlayer> allowedPlayers)
        {
            return DomainResult.Ok(new TeamCreated(Guid.NewGuid(), raceId, teamName, trainerName, allowedPlayers));
        }

        public DomainResult BuyPlayer(Guid playerTypeId)
        {
            var play = AllowedPlayers.FirstOrDefault(ap => ap.PlayerTypeId == playerTypeId);
            if (play == null) return DomainResult.Error(new CanNotUsePlayerInThisRaceError(playerTypeId, RaceId));
            int ammount = Players.Count(p => p.PlayerType == playerTypeId);

            var canUsePlayer = play.CanUsePlayer(ammount);
            if (canUsePlayer.Failed) return DomainResult.Error(canUsePlayer.DomainErrors);

            if (play.Cost.LessThan(TeamMoney))
            {
                var playerId = Guid.NewGuid();
                var playerDto = new PlayerDto(playerTypeId, playerId);
                Players.Append(playerDto);
                TeamMoney = TeamMoney.Minus(play.Cost);
                var playerBought = new PlayerBought(Id, playerTypeId, TeamMoney, playerDto.PlayerId);
                Apply(playerBought);
                return DomainResult.Ok(playerBought);
            }

            return DomainResult.Error(new FewMoneyInTeamChestError(play.Cost.Value, TeamMoney.Value));
        }

        public void Apply(TeamCreated teamCreated)
        {
            Id = teamCreated.EntityId;
            RaceId = teamCreated.RaceId;
            TeamName = teamCreated.TeamName;
            TrainerName = teamCreated.TrainerName;
            AllowedPlayers = teamCreated.AllowedPlayers;
        }

        public void Apply(PlayerBought playerBought)
        {
            TeamMoney = playerBought.NewTeamChestBalance;
            Players = Players.Append(new PlayerDto(playerBought.PlayerTypeId, playerBought.PlayerId));
        }
    }

    public class PlayerDto
    {
        public Guid PlayerType { get; }
        public Guid PlayerId { get; }

        public PlayerDto(Guid playerType, Guid playerId)
        {
            PlayerType = playerType;
            PlayerId = playerId;
        }
    }
}