using System;
using Microwave.Domain.Identities;
using Microwave.Queries;

namespace Seasons.ReadHost.Seasons.Events
{
    public class SeasonCreated : ISubscribedDomainEvent
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