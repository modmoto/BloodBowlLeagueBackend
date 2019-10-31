using System;
using Microwave.Domain.EventSourcing;

namespace Domain.Seasons.Events
{
    public class SeasonCreated : IDomainEvent
    {
        public string EntityId => SeasonId.ToString();
        public Guid SeasonId { get; }
        public string SeasonName { get; }
        public DateTimeOffset CreationDate { get; }

        public SeasonCreated(Guid seasonId, string seasonName, DateTimeOffset creationDate)
        {
            SeasonId = seasonId;
            SeasonName = seasonName;
            CreationDate = creationDate;
        }
    }
}