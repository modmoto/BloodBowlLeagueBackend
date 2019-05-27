using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Matches.ForeignEvents;
using Microwave.Domain.Identities;
using Microwave.Queries;

namespace Domain.Matches
{
    public class TeamReadModel : ReadModel, IHandle<PlayerBought>, IHandle<TeamCreated>
    {
        public GuidIdentity TeamId { get; private set; }
        public IEnumerable<GuidIdentity> Players { get; private set; } = new List<GuidIdentity>();

        public override Type GetsCreatedOn => typeof(TeamCreated);

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