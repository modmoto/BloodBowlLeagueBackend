using System;
using System.Collections.Generic;
using Microwave.Domain.Identities;
using Microwave.Queries;
using Seasons.ReadHost.Seasons.Events;

namespace Seasons.ReadHost.Seasons
{
    public class AllSeasonsOverview : Query, IHandle<SeasonCreated>
    {
        public void Handle(SeasonCreated domainEvent)
        {
            var seasonOverviewDto = new SeasonOverviewDto
            {
                SeasonId = domainEvent.SeasonId,
                SeasonName = domainEvent.SeasonName,
                CreationDate = domainEvent.CreationDate
            };
            AllSeasons.Add(seasonOverviewDto);
        }

        public IList<SeasonOverviewDto> AllSeasons { get; set; } = new List<SeasonOverviewDto>();
    }

    public class SeasonOverviewDto
    {
        public GuidIdentity SeasonId { get; set; }
        public string SeasonName { get; set; }
        public DateTimeOffset CreationDate { get; set; }
    }
}