using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Matches.ForeignEvents;
using Microwave.Domain;
using Microwave.Queries;

namespace Domain.Matches
{
    public class TeamReadModel : ReadModel, IApply<PlayerBought>, IApply<TeamCreated>
    {
        public GuidIdentity TeamId { get; private set; }
        public IEnumerable<GuidIdentity> Players { get; private set; } = new List<GuidIdentity>();

        public override Type GetsCreatedOn => typeof(TeamCreated);
        public override Identity EntityId => TeamId;

        public void Apply(PlayerBought domainEvent)
        {
            Players = Players.Append(domainEvent.PlayerId);
        }

        public void Apply(TeamCreated domainEvent)
        {
            TeamId = (GuidIdentity)domainEvent.EntityId;
        }
    }
}