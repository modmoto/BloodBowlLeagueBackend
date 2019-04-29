using Microwave.Domain;

namespace Domain.Seasons.Events
{
    public class Alalla : IDomainEvent
    {
        public Identity EntityId { get; }
        public GuidIdentity TrainerAtHome { get; }
        public GuidIdentity TrainerAsGuest { get; }

        public Alalla(GuidIdentity entityId, GuidIdentity trainerAtHome, GuidIdentity trainerAsGuest)
        {
            EntityId = entityId;
            TrainerAtHome = trainerAtHome;
            TrainerAsGuest = trainerAsGuest;
        }
    }
}