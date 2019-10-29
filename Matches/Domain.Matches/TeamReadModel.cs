using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Matches.ForeignEvents;
using Microwave.Queries;

namespace Domain.Matches
{
    public class TeamReadModel : ReadModel<TeamCreated>,
        IHandle<PlayerBought>,
        IHandle<TeamCreated>
    {
        public Guid TeamId { get; private set; }
        public IEnumerable<Guid> Players { get; private set; } = new List<Guid>();

        public void Handle(PlayerBought domainEvent)
        {
            Players = Players.Append(domainEvent.PlayerId);
        }

        public void Handle(TeamCreated domainEvent)
        {
            TeamId = domainEvent.TeamId;
        }

    }
}