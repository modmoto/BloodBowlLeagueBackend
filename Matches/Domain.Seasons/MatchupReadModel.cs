using System;
using Domain.Seasons.Events;
using Microwave.Domain;
using Microwave.Queries;

namespace Domain.Seasons
{
    public class MatchupReadModel : ReadModel, IHandle<MatchCreated>
    {
        public GuidIdentity MatchId { get; private set; }
        public GuidIdentity TeamAtHome { get; private set; }
        public GuidIdentity TeamAsGuest { get; private set; }

        public override Type GetsCreatedOn => typeof(MatchCreated);

        public void Handle(MatchCreated domainEvent)
        {
            MatchId = (GuidIdentity) domainEvent.EntityId;
            TeamAtHome = domainEvent.TrainerAtHome;
            TeamAsGuest = domainEvent.TrainerAsGuest;
        }
    }
}