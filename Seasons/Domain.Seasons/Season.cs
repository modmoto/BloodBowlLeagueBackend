using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Seasons.Errors;
using Domain.Seasons.Events;
using Domain.Seasons.TeamReadModels;
using Microwave.Domain;
using Microwave.Domain.EventSourcing;
using Microwave.Domain.Validation;

namespace Domain.Seasons
{
    public class Season : Entity, IApply<TeamAddedToSeason>, IApply<SeasonCreated>, IApply<SeasonStarted>
    {
        public Guid SeasonId { get; private set; }
        public IEnumerable<Guid> Teams { get; private set; } = new List<Guid>();
        public IEnumerable<GameDay> GameDays { get; private set; } = new List<GameDay>();
        public bool SeasonIsStarted { get; private set; }

        public static DomainResult Create(string seasonName)
        {
            return DomainResult.Ok(new SeasonCreated(Guid.NewGuid(), seasonName, DateTimeOffset.UtcNow));
        }

        public DomainResult AddTeam(TeamReadModel teamId)
        {
            if (SeasonIsStarted) return DomainResult.Error(new SeasonAllreadyStarted());
            if (Teams.Any(t => t == teamId.TeamId)) return DomainResult.Error(new CanNotAddTeamTwice(teamId.TeamId));
            return DomainResult.Ok(new TeamAddedToSeason(SeasonId, teamId.TeamId));
        }

        public DomainResult StartSeason()
        {
            if (Teams.Count() < 2) return DomainResult.Error(new SeasonMustHaveAtLeast2Teams(Teams.Count()));
            if (TeamCountIsUneven()) return DomainResult.Error(new CanNotStartSeasonWithUnevenTeamCount(Teams.Count()));
            if (SeasonIsStarted) return DomainResult.Error(new CanNotStartSeasonASecondTime());

            var matchPairingService = new MatchPairingService();
            var gameDays = matchPairingService.ComputePairings(Teams).ToList();

            var seasonStarted = new SeasonStarted(SeasonId, gameDays, DateTimeOffset.UtcNow);
            return DomainResult.Ok(seasonStarted);
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
            SeasonId = domainEvent.SeasonId;
        }

        public void Apply(SeasonStarted domainEvent)
        {
            SeasonIsStarted = true;
            GameDays = domainEvent.GameDays;
        }
    }

    public class TeamHasToBeFinishedToAddToSeason : DomainError
    {
        public TeamHasToBeFinishedToAddToSeason()
            : base("Team has to be finished to add to season")
        {
        }
    }

    public class CanNotStartSeasonASecondTime : DomainError
    {
        public CanNotStartSeasonASecondTime() : base("Season is allready started, can not start a season a second time")
        {
        }
    }

    public class CanNotAddTeamTwice : DomainError
    {
        public CanNotAddTeamTwice(Guid teamId) : base($"Can not add the team {teamId} twice")
        {
        }
    }
}
