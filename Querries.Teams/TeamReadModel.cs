using System;
using System.Collections.Generic;
using System.Linq;
using Microwave.Queries;
using Querries.Teams.DomainEvents;

namespace Querries.Teams
{
    [CreateReadmodelOn(typeof(TeamCreated))]
    public class TeamReadModel : ReadModel, IHandle<TeamCreated>, IHandle<PlayerBought>
    {
        public IEnumerable<Guid> PlayerList { get; set; }
        public Guid RaceId { get; set; }
        public string TrainerName { get; set; }
        public string TeamName { get; set; }
        public Guid TeamId { get; set; }

        public GoldCoins TeamChest { get; set; }

        public void Handle(TeamCreated domainEvent)
        {
            TeamId = domainEvent.EntityId;
            RaceId = domainEvent.RaceId;
            TeamName = domainEvent.TeamName;
            TrainerName = domainEvent.TrainerName;
            PlayerList = new List<Guid>();
        }

        public void Handle(PlayerBought domainEvent)
        {
            TeamChest = domainEvent.NewTeamChestBalance;
            PlayerList = PlayerList.Append(domainEvent.PlayerTypeId);
        }
    }
}