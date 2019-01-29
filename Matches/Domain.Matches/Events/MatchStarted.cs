using Microwave.Domain;

namespace Domain.Matches.Events
{
    public class MatchStarted : IDomainEvent
    {
        public TeamReadModel HomeTeam { get; }
        public TeamReadModel GuestTeam { get; }

        public Identity EntityId { get; }

        public MatchStarted(GuidIdentity entityId, TeamReadModel homeTeam, TeamReadModel guestTeam)
        {
            EntityId = entityId;
            HomeTeam = homeTeam;
            GuestTeam = guestTeam;
        }

    }
}