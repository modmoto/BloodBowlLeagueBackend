using System;
using Microwave.Domain.EventSourcing;
using Microwave.Domain.Identities;

namespace Domain.Seasons.Events
{
    public class SeasonCreated : IDomainEvent
    {
        public Identity EntityId => SeasonId;
        public GuidIdentity SeasonId { get; }
        public string SeasonName { get; }
        public DateTimeOffset CreationDate { get; }

        public SeasonCreated(GuidIdentity seasonId, string seasonName, DateTimeOffset creationDate)
        {
            SeasonId = seasonId;
            SeasonName = seasonName;
            CreationDate = creationDate;
        }
    }
}