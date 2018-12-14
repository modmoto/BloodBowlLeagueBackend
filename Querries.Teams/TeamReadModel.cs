using System;
using System.Collections.Generic;
using Microwave.Queries;
using Querries.Teams.DomainEvents;

namespace Querries.Teams
{
    [CreateReadmodelOn(typeof(TeamCreated))]
    public class TeamReadModel : ReadModel, IHandle<TeamCreated>, IHandle<PlayerBought>
    {
        public IEnumerable<Guid> PlayerList { get; set; } = new List<Guid>();
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
            var newPlayers = new List<Guid>();
            newPlayers.Add(domainEvent.PlayerTypeId);
            PlayerList = newPlayers;
        }
    }
}