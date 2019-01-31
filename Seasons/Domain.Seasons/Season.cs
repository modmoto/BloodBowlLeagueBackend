using System.Collections.Generic;
using System.Linq;
using Domain.Seasons.Events;
using Microwave.Domain;

namespace Domain.Seasons
{
    public class Season : IApply<TeamAddedToSeason>, IApply<SeasonCreated>, IApply<SeasonStarted>
    {
        public GuidIdentity SeasonId { get; private set; }
        public IEnumerable<GuidIdentity> Teams { get; private set; } = new List<GuidIdentity>();
        public IEnumerable<Matchup> Matchups { get; private set; }
        public bool SeasonIsStarted { get; private set; }

        public static DomainResult Create()
        {
            return DomainResult.Ok(new SeasonCreated(GuidIdentity.Create()));
        }

        public DomainResult AddTeam(GuidIdentity teamId)
        {
            if (SeasonIsStarted) return DomainResult.Error(new SeasonAllreadyStarted());
            return DomainResult.Ok(new TeamAddedToSeason(SeasonId, teamId));
        }

        public DomainResult StartSeason()
        {
            if (TeamCountIsUneven()) return DomainResult.Error(new CanNotStartSeasonWithUnevenTeamCount(Teams.Count()));

            var matchPairingService = new MatchPairingService();
            var gameDays = matchPairingService.ComputePairings(Teams);
            return DomainResult.Ok(new SeasonStarted(SeasonId, gameDays));
        }

        private bool TeamCountIsUneven()
        {
            return Teams.Count() % 2 != 0;
        }

        public void Apply(TeamAddedToSeason domainEvent)
        {
            Teams = Teams.Append(domainEvent.TeamId);
        }

        public void Apply(SeasonCreated domainEvent)
        {
            SeasonId = (GuidIdentity) domainEvent.EntityId;
        }

        public void Apply(SeasonStarted domainEvent)
        {
            SeasonIsStarted = true;
        }
    }
}
