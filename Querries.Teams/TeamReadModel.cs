using System;
using System.Collections.Generic;
using Microwave.Queries;
using Querries.Teams.DomainEvents;

namespace Querries.Teams
{
    public class TeamReadModel : ReadModel
    {
        public IEnumerable<PlayerDto> PlayerList { get; set; }
        public Guid RaceId { get; set; }
        public string TrainerName { get; set; }
        public string TeamName { get; set; }
        public Guid Id { get; set; }

        public void Handle(TeamCreated domainEvent)
        {
            Id = domainEvent.EntityId;
            RaceId = domainEvent.RaceId;
            TeamName = domainEvent.TeamName;
            TrainerName = domainEvent.TrainerName;
        }

        public void Handle(PlayerBought domainEvent)
        {
            var playerDtos = new List<PlayerDto>();
            playerDtos.Add(new PlayerDto { PlayerTypeId = domainEvent.PlayerTypeId });
            playerDtos.AddRange(PlayerList ?? new List<PlayerDto>());
            PlayerList = playerDtos;
        }
    }

    public class PlayerDto
    {
        public Guid PlayerTypeId { get; set; }
    }
}