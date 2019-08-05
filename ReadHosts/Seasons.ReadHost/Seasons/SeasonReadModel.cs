using System;
using System.Collections.Generic;
using Microwave.Domain.Identities;
using Microwave.Queries;
using Seasons.ReadHost.Seasons.Events;

namespace Seasons.ReadHost.Seasons
{
    public class SeasonReadModel : ReadModel,
        IHandle<SeasonCreated>,
        IHandle<SeasonStarted>,
        IHandle<TeamAddedToSeason>
    {
        public override Type GetsCreatedOn => typeof(SeasonCreated);

        public GuidIdentity SeasonId { get; set; }
        public IEnumerable<GameDayDto> GameDays { get; set; }
        public IList<GuidIdentity> Teams { get; set; } = new List<GuidIdentity>();

        public string SeasonName { get; set; }

        public bool IsStarted { get; set; }

        public void Handle(SeasonCreated domainEvent)
        {
            SeasonId = domainEvent.SeasonId;
            SeasonName = domainEvent.SeasonName;
        }

        public void Handle(SeasonStarted domainEvent)
        {
            IsStarted = true;
            GameDays = domainEvent.GameDays;
        }

        public void Handle(TeamAddedToSeason domainEvent)
        {
            Teams.Add(domainEvent.TeamId);
        }
    }
}