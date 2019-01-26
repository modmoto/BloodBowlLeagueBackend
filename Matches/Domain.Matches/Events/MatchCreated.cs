using Microwave.Domain;

namespace Domain.Matches.Events
{
    public class MatchCreated : IDomainEvent
    {
        public GuidIdentity TrainerAtHome { get; }
        public GuidIdentity TrainerAsGuest { get; }

        public Identity EntityId { get; }

        public MatchCreated(GuidIdentity entityId, GuidIdentity trainerAtHome, GuidIdentity trainerAsGuest)
        {
            EntityId = entityId;
            TrainerAtHome = trainerAtHome;
            TrainerAsGuest = trainerAsGuest;
        }

    }
}