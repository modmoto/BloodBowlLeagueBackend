using System.Collections.Generic;
using System.Linq;
using Domain.Seasons.Errors;
using Domain.Seasons.Events;
using Microwave.Domain.EventSourcing;
using Microwave.Domain.Identities;
using Microwave.Domain.Validation;

namespace Domain.Seasons
{
    public class Season : Entity, IApply<TeamAddedToSeason>, IApply<SeasonCreated>, IApply<SeasonStarted>
    {
        public GuidIdentity SeasonId { get; private set; }
        public IEnumerable<GuidIdentity> Teams { get; private set; } = new List<GuidIdentity>();
        public IEnumerable<GameDay> GameDays { get; private set; } = new List<GameDay>();
        public bool SeasonIsStarted { get; private set; }

        public static DomainResult Create()
        {
            return DomainResult.Ok(new SeasonCreated(GuidIdentity.Create()));
        }

        public DomainResult AddTeam(GuidIdentity teamId)
        {
            if (SeasonIsStarted) return DomainResult.Error(new SeasonAllreadyStarted());
            if (Teams.Contains(teamId)) return DomainResult.Error(new CanNotAddTeamTwice(teamId));
            return DomainResult.Ok(new TeamAddedToSeason(SeasonId, teamId));
        }

        public DomainResult StartSeason()
        {
            if (TeamCountIsUneven()) return DomainResult.Error(new CanNotStartSeasonWithUnevenTeamCount(Teams.Count()));
            if (SeasonIsStarted) return DomainResult.Error(new CanNotStartSeasonASecondTime());

            var matchPairingService = new MatchPairingService();
            var domainEvents = matchPairingService.ComputePairings(SeasonId, Teams).ToList();
            var gameDayCreatedEvents = domainEvents.Where(d => d.GetType() == typeof(GameDayCreated)).Select(d => (GameDayCreated) d);

            var gameDays = new List<GameDay>();
            foreach (var gameDayCreated in gameDayCreatedEvents)
            {
                var gameDay = new GameDay();
                gameDay.Apply(gameDayCreated);
                gameDays.Add(gameDay);
            }

            var seasonStarted = new SeasonStarted(SeasonId, gameDays);
            var allEvents = domainEvents.Append(seasonStarted);
            return DomainResult.Ok(allEvents);
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

    public class CanNotStartSeasonASecondTime : DomainError
    {
        public CanNotStartSeasonASecondTime() : base("Season is allready started, can not start a season a second time")
        {
        }
    }

    public class CanNotAddTeamTwice : DomainError
    {
        public CanNotAddTeamTwice(GuidIdentity teamId) : base($"Can not add the team {teamId} twice")
        {
        }
    }
}
