using System.Collections.Generic;
using System.Linq;
using Domain.Matches.Errors;
using Domain.Matches.Events;
using Microwave.Domain.EventSourcing;
using Microwave.Domain.Identities;
using Microwave.Domain.Validation;

namespace Domain.Matches
{
    public class Matchup : Entity,
        IApply<MatchFinished>,
        IApply<MatchStarted>,
        IApply<MatchCreated>
    {
        public GuidIdentity MatchId { get; private set; }
        public IEnumerable<GuidIdentity> HomeTeamPlayers { get; private set; }
        public IEnumerable<GuidIdentity> GuestTeamPlayers { get; private set; }
        public GuidIdentity TeamAsGuest { get; private set; }
        public GuidIdentity TeamAtHome { get; private set; }
        private bool _isFinished;
        private bool _isStarted;


        public static DomainResult Create(
            GuidIdentity matchId,
            TeamReadModel teamAtHome,
            TeamReadModel teamAsGuest)
        {
            if (teamAtHome == teamAsGuest) return DomainResult.Error(new TeamsCanNotBeTheSame(
                teamAtHome.TeamId, teamAsGuest.TeamId));

            var domainEvents = new MatchCreated(matchId, teamAtHome.TeamId, teamAsGuest.TeamId);
            return DomainResult.Ok(domainEvents);
        }

        public static DomainResult Create(
            TeamReadModel teamAtHome,
            TeamReadModel teamAsGuest)
        {
            return Create(GuidIdentity.Create(), teamAtHome, teamAsGuest);
        }

        public DomainResult Start(TeamReadModel teamAtHome, TeamReadModel teamAsGuest)
        {
            var matchStarted = new MatchStarted(MatchId, teamAtHome.Players, teamAsGuest.Players);
            return DomainResult.Ok(matchStarted);
        }

        public DomainResult Finish(IEnumerable<PlayerProgression> playerProgressions)
        {
            if (_isStarted) return DomainResult.Error(new MatchDidNotStartYet());
            if (_isFinished) return DomainResult.Error(new MatchAllreadyFinished());
            var progressions = playerProgressions.ToList();

            var homeTeamProgression = progressions.Where(p => HomeTeamPlayers.Contains(p.PlayerId));
            var guestTeamProgression = progressions.Where(p => GuestTeamPlayers.Contains(p.PlayerId));
            var unrelatedPlayers = progressions.Where(p =>
                !HomeTeamPlayers.Contains(p.PlayerId) && !GuestTeamPlayers.Contains(p.PlayerId)).Select(p => p.PlayerId).ToList();
            if (unrelatedPlayers.Any()) return DomainResult.Error(new PlayerWasNotPartOfTheTeamWhenStartingTheMatch(unrelatedPlayers));

            var homeTouchDowns = CountTouchDowns(homeTeamProgression);
            var guestTouchDowns = CountTouchDowns(guestTeamProgression);

            var homeResult = new PointsOfTeam(TeamAtHome, homeTouchDowns);
            var guestResult = new PointsOfTeam(TeamAsGuest, guestTouchDowns);

            var gameResult = GameResult.CreatGameResult(homeResult, guestResult);

            var matchResultUploaded = new MatchFinished(MatchId, progressions, gameResult);
            return DomainResult.Ok(matchResultUploaded);
        }

        private static int CountTouchDowns(IEnumerable<PlayerProgression> trainerResults)
        {
            return trainerResults.Sum(playerProgression => playerProgression.ProgressionEvents.Count(ev => ev == ProgressionEvent.PlayerMadeTouchdown));
        }

        public void Apply(MatchFinished domainEvent)
        {
            _isFinished = true;
        }

        public void Apply(MatchStarted domainEvent)
        {
            _isStarted = true;
            HomeTeamPlayers = domainEvent.HomeTeam;
            GuestTeamPlayers = domainEvent.GuestTeam;
        }

        public void Apply(MatchCreated domainEvent)
        {
            MatchId = domainEvent.MatchId;
            TeamAtHome = domainEvent.TeamAtHome;
            TeamAsGuest = domainEvent.TeamAsGuest;
        }
    }
}