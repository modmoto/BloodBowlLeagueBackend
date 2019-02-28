using System;
using System.Collections.Generic;
using System.Linq;
using Microwave.Domain;
using Microwave.Queries;
using Teams.ReadHost.Teams.Events;

namespace Teams.ReadHost.Teams
{
    public class TeamReadModel : ReadModel, IHandleVersioned<TeamCreated>, IHandleVersioned<PlayerBought>
    {
        public IEnumerable<PlayerDto> PlayerList { get; set; } = new List<PlayerDto>();
        public Identity RaceId { get; set; }
        public string TrainerName { get; set; }
        public string TeamName { get; set; }
        public Identity TeamId { get; set; }
        public GoldCoins TeamChest { get; set; }

        public void Handle(TeamCreated domainEvent, long version)
        {
            TeamId = domainEvent.EntityId;
            RaceId = domainEvent.RaceId;
            TeamName = domainEvent.TeamName;
            TrainerName = domainEvent.TrainerName;
            TeamChest = domainEvent.StartingMoney;
            TeamVersion = version;
        }

        public long TeamVersion { get; private set; }

        public void Handle(PlayerBought domainEvent, long version)
        {
            TeamChest = domainEvent.NewTeamChestBalance;
            var playerDto = new PlayerDto(domainEvent.PlayerId, domainEvent.PlayerTypeId);
            PlayerList = PlayerList.Append(playerDto);
            TeamVersion = version;
        }

        public override Type GetsCreatedOn => typeof(TeamCreated);
        public override Identity EntityId => TeamId;
    }
}