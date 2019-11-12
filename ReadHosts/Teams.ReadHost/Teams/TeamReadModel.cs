using System;
using System.Collections.Generic;
using System.Linq;
using Microwave.Queries;
using Teams.ReadHost.Teams.Events;

namespace Teams.ReadHost.Teams
{
    public class TeamReadModel : ReadModel<TeamDraftCreated>,
        IHandle<TeamDraftCreated>,
        IHandle<PlayerBought>,
        IHandle<PlayerAddedToDraft>,
        IHandle<PlayerRemovedFromDraft>,
        IHandle<TeamCreated>
    {
        public IEnumerable<PlayerDto> PlayerList { get; set; } = new List<PlayerDto>();
        public string RaceId { get; set; }
        public IEnumerable<AllowedPlayer> AllowedPlayers { get; private set; } = new List<AllowedPlayer>();
        public string TrainerName { get; set; }
        public string TeamName { get; set; }
        public Guid TeamId { get; set; }
        public GoldCoins TeamChest { get; set; }
        public bool IsFinished { get; set; }

        public void Handle(TeamDraftCreated domainEvent)
        {
            TeamId = domainEvent.TeamId;
            RaceId = domainEvent.RaceId;
            TeamName = domainEvent.TeamName;
            TrainerName = domainEvent.TrainerName;
            TeamChest = domainEvent.StartingMoney;
            AllowedPlayers = domainEvent.AllowedPlayers;
        }

        public void Handle(PlayerBought domainEvent)
        {
            TeamChest = domainEvent.NewTeamChestBalance;
            var playerDto = new PlayerDto(domainEvent.PlayerId, domainEvent.PlayerTypeId, domainEvent.PlayerPositionNumber);
            PlayerList = PlayerList.Append(playerDto).OrderBy(p => p.PlayerPositionNumber);
        }

        public void Handle(PlayerRemovedFromDraft domainEvent)
        {
            var playerDtos = PlayerList.Where(p => p.PlayerId != domainEvent.PlayerId);
            PlayerList = playerDtos;
            TeamChest = domainEvent.NewTeamChestBalance;
        }

        public void Handle(TeamCreated domainEvent)
        {
            IsFinished = true;
            TeamId = domainEvent.TeamId;
            RaceId = domainEvent.RaceId;
            TeamName = domainEvent.TeamName;
            TrainerName = domainEvent.TrainerName;
            TeamChest = domainEvent.StartingMoney;
            PlayerList = new List<PlayerDto>();
            AllowedPlayers = domainEvent.AllowedPlayers;
        }

        public void Handle(PlayerAddedToDraft domainEvent)
        {
            TeamChest = domainEvent.NewTeamChestBalance;
            var playerDto = new PlayerDto(domainEvent.PlayerId, domainEvent.PlayerTypeId, domainEvent.PlayerPositionNumber);
            PlayerList = PlayerList.Append(playerDto).OrderBy(p => p.PlayerPositionNumber);
        }
    }
}