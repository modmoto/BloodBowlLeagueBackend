using System;
using System.Collections.Generic;
using System.Linq;
using Microwave.Domain;
using Microwave.Queries;
using Querries.Teams.DomainEvents;

namespace Querries.Teams
{
    public class TeamReadModel : ReadModel, IHandle<TeamCreated>, IHandle<PlayerBought>
    {
        public IEnumerable<PlayerDto> PlayerList { get; set; }
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
            PlayerList = new List<PlayerDto>();
        }

        public void Handle(PlayerBought domainEvent)
        {
            TeamChest = domainEvent.NewTeamChestBalance;
            var playerDto = new PlayerDto(domainEvent.PlayerId, domainEvent.PlayerTypeId);
            PlayerList = PlayerList.Append(playerDto);
        }

        public override Type GetsCreatedOn => typeof(TeamCreated);
    }

    public class PlayerDto
    {
        public PlayerDto(GuidIdentity playerId, StringIdentity playerTypeId)
        {
            PlayerId = playerId;
            PlayerTypeId = playerTypeId;
        }

        public GuidIdentity PlayerId { get; }
        public StringIdentity PlayerTypeId{ get; }
    }
}