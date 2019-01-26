using Microwave.Domain;

namespace Domain.Matches.Events
{
    public class MatchCreated : IDomainEvent
    {
        public TeamReadModel HomeTeam { get; }
        public TeamReadModel GuestTeam { get; }

        public Identity EntityId { get; }

        public MatchCreated(GuidIdentity entityId, TeamReadModel homeTeam, TeamReadModel guestTeam)
        {
            EntityId = entityId;
            HomeTeam = homeTeam;
            GuestTeam = guestTeam;
        }

    }
}