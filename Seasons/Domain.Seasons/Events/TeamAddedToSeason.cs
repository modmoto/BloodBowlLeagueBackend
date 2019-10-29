using System;
using Microwave.Domain;
using Microwave.Domain.EventSourcing;

namespace Domain.Seasons.Events
{
    public class TeamAddedToSeason : IDomainEvent
    {
        public TeamAddedToSeason(Guid seasonId, Guid teamId)
        {
            SeasonId = seasonId;
            TeamId = teamId;
        }

        public string EntityId => SeasonId.ToString();
        public Guid SeasonId { get; }
        public Guid TeamId { get; }
    }
}