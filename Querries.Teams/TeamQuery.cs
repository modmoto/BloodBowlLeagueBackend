using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Teams.DomainEvents;
using Microwave.Application;

namespace Querries.Teams
{
    public class TeamQuery : IdentifiableQuery, IHandle<TeamCreated>, IHandle<PlayerBought>
    {
        public IEnumerable<PlayerDto> PlayerList { get; } = new List<PlayerDto>();
        public Guid RaceId { get; private set; }
        public string TrainerName { get; private set; }
        public string TeamName { get; private set; }

        public void Handle(TeamCreated domainEvent)
        {
            Id = domainEvent.EntityId;
            RaceId = domainEvent.RaceId;
            TeamName = domainEvent.TeamName;
            TrainerName = domainEvent.TrainerName;
        }

        public void Handle(PlayerBought domainEvent)
        {
            PlayerList.Append(new PlayerDto(domainEvent.PlayerTypeId));
        }
    }

    public class PlayerDto
    {
        public Guid PlayerTypeId { get; }

        public PlayerDto(Guid PlayerTypeId)
        {
            this.PlayerTypeId = PlayerTypeId;
        }
    }
}