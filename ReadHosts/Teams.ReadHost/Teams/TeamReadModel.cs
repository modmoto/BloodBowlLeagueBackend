using System;
using System.Collections.Generic;
using System.Linq;
using Microwave.Domain;
using Microwave.Queries;
using Teams.ReadHost.Teams.Events;

namespace Teams.ReadHost.Teams
{
    public class TeamReadModel : ReadModel, IHandle<TeamCreated>, IHandle<PlayerBought>
    {
        public IEnumerable<PlayerDto> PlayerList { get; set; } = new List<PlayerDto>();
        public Identity RaceId { get; set; }
        public string TrainerName { get; set; }
        public string TeamName { get; set; }
        public Identity TeamId { get; set; }
        public GoldCoins TeamChest { get; set; }

        public void Handle(TeamCreated domainEvent)
        {
            TeamId = domainEvent.EntityId;
            RaceId = domainEvent.RaceId;
            TeamName = domainEvent.TeamName;
            TrainerName = domainEvent.TrainerName;
            TeamChest = domainEvent.StartingMoney;
        }

        public void Handle(PlayerBought domainEvent)
        {
            TeamChest = domainEvent.NewTeamChestBalance;
            var playerDto = new PlayerDto(domainEvent.PlayerId, domainEvent.PlayerTypeId);
            PlayerList = PlayerList.Append(playerDto);
        }

        public override Type GetsCreatedOn => typeof(TeamCreated);
    }
}