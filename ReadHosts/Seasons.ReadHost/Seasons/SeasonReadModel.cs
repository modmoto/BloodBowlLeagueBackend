using System.Collections.Generic;
using Microwave.Domain.Identities;
using Microwave.Queries;
using Seasons.ReadHost.Seasons.Events;

namespace Seasons.ReadHost.Seasons
{
    public class SeasonReadModel : ReadModel<SeasonCreated>,
        IHandle<SeasonCreated>,
        IHandle<SeasonStarted>,
        IHandle<TeamAddedToSeason>
    {
        public GuidIdentity SeasonId { get; set; }
        public IEnumerable<GameDayDto> GameDays { get; set; }
        public IList<GuidIdentity> Teams { get; } = new List<GuidIdentity>();

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