using System;
using System.Collections.Generic;
using System.Linq;
using Microwave.Domain.Identities;
using Microwave.Queries;
using Seasons.ReadHost.Seasons.Events;

namespace Seasons.ReadHost.Seasons
{
    public class AllSeasonsOverview : Query,
        IHandle<SeasonStarted>,
        IHandle<SeasonCreated>
    {
        public IList<SeasonOverviewDto> AllSeasons { get; set; } = new List<SeasonOverviewDto>();

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

        public void Handle(SeasonStarted domainEvent)
        {
            var seasonOverviewDto = AllSeasons.Single(s => s.SeasonId == domainEvent.SeasonId);
            seasonOverviewDto.StartDate = domainEvent.StartDate;
            seasonOverviewDto.Started = true;
        }
    }

    public class SeasonOverviewDto
    {
        public GuidIdentity SeasonId { get; set; }
        public string SeasonName { get; set; }
        public DateTimeOffset CreationDate { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public bool Started { get; set; }
    }
}