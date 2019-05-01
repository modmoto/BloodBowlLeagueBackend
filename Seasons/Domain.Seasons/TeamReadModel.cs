using System;
using System.Collections.Generic;
using Domain.Seasons.ForeignEvents;
using Microwave.Domain;
using Microwave.Queries;

namespace Domain.Seasons
{
    public class TeamReadModel : ReadModel, IApply<TeamCreated>
    {
        public GuidIdentity TeamId { get; private set; }

        public override Type GetsCreatedOn => typeof(TeamCreated);

        public void Apply(TeamCreated domainEvent)
        {
            TeamId = domainEvent.TeamId;
        }
    }
}