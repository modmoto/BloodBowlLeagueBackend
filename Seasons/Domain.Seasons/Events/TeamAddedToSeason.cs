using System;
using Microwave.Domain;

namespace Domain.Seasons.Events
{
    public class TeamAddedToSeason : IDomainEvent
    {
        public TeamAddedToSeason(Guid seasonId, Guid teamId)
        {
            SeasonId = seasonId;
            TeamId = teamId;
        }

        public string EntityId => SeasonId;
        public Guid SeasonId { get; }
        public Guid TeamId { get; }
    }
}