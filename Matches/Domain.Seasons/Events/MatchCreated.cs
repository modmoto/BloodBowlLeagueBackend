using Microwave.Domain;

namespace Domain.Seasons.Events
{
    public class MatchCreated : IDomainEvent
    {
        public Identity EntityId { get; }
        public GuidIdentity TrainerAtHome { get; }
        public GuidIdentity TrainerAsGuest { get; }

        public MatchCreated(GuidIdentity entityId, GuidIdentity trainerAtHome, GuidIdentity trainerAsGuest)
        {
            EntityId = entityId;
            TrainerAtHome = trainerAtHome;
            TrainerAsGuest = trainerAsGuest;
        }
    }
}