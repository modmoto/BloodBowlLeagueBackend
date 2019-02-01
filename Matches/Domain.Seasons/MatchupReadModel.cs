using Domain.Seasons.Events;
using Microwave.Domain;

namespace Domain.Seasons
{
    public class MatchupReadModel : Entity, IApply<MatchCreated>
    {
        public GuidIdentity MatchId { get; private set; }
        public GuidIdentity TeamAtHome { get; private set; }
        public GuidIdentity TeamAsGuest { get; private set; }

        public void Apply(MatchCreated domainEvent)
        {
            MatchId = (GuidIdentity) domainEvent.EntityId;
            TeamAtHome = domainEvent.TrainerAtHome;
            TeamAsGuest = domainEvent.TrainerAsGuest;
        }
    }
}