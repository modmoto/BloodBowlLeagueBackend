using System.Collections.Generic;
using System.Linq;
using Microwave.Domain.Identities;
using Microwave.Queries;
using Teams.ReadHost.Teams.Events;

namespace Teams.ReadHost.Teams
{
    public class TeamReadModel : ReadModel<TeamCreated>,
        IHandle<TeamCreated>,
        IHandle<PlayerBought>,
        IHandle<PlayerRemoved>,
        IHandle<TeamFinished>
    {
        public IEnumerable<PlayerDto> PlayerList { get; set; } = new List<PlayerDto>();
        public StringIdentity RaceId { get; set; }
        public IEnumerable<AllowedPlayer> AllowedPlayers { get; private set; } = new List<AllowedPlayer>();
        public string TrainerName { get; set; }
        public string TeamName { get; set; }
        public Identity TeamId { get; set; }
        public GoldCoins TeamChest { get; set; }
        public bool IsFinished { get; set; }

        public void Handle(TeamCreated domainEvent)
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
            var playerDto = new PlayerDto(domainEvent.PlayerId, domainEvent.PlayerTypeId);
            PlayerList = PlayerList.Append(playerDto);
        }

        public void Handle(PlayerRemoved domainEvent)
        {
            var playerDtos = PlayerList.Where(p => p.PlayerId != domainEvent.PlayerId);
            PlayerList = playerDtos;
            TeamChest = domainEvent.NewTeamChestBalance;
        }

        public void Handle(TeamFinished domainEvent)
        {
            IsFinished = true;
        }
    }
}