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
        IApply<MatchCreated>,
        IApply<MatchProgressed>
    {
        public GuidIdentity MatchId { get; private set; }
        public IEnumerable<GuidIdentity> HomeTeamPlayers { get; private set; }
        public IEnumerable<GuidIdentity> GuestTeamPlayers { get; private set; }
        public GuidIdentity TeamAsGuest { get; private set; }
        public IEnumerable<PlayerProgression> PlayerProgressions { get; private set; } = new List<PlayerProgression>();

        private List<PlayerProgression> HomeTeamProgressions => PlayerProgressions.Where(p => HomeTeamPlayers.Contains(p.PlayerId)).ToList();
        private List<PlayerProgression> GuestTeamProgressions => PlayerProgressions.Where(p => GuestTeamPlayers.Contains(p.PlayerId)).ToList();

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

        public DomainResult Finish()
        {
            if (!_isStarted) return DomainResult.Error(new MatchDidNotStartYet());
            if (_isFinished) return DomainResult.Error(new MatchAllreadyFinished());

            var gameResult = CreateGameResult();

            var matchResultUploaded = new MatchFinished(MatchId, PlayerProgressions, gameResult);
            return DomainResult.Ok(matchResultUploaded);
        }

        public DomainResult ProgressMatch(PlayerProgression playerProgression)
        {
            if (!_isStarted) return DomainResult.Error(new MatchDidNotStartYet());
            if (_isFinished) return DomainResult.Error(new MatchAllreadyFinished());
            
            var playerIsNotInHomeOrGuestTeam =
                !HomeTeamPlayers.Contains(playerProgression.PlayerId)
                && !GuestTeamPlayers.Contains(playerProgression.PlayerId);
            if (playerIsNotInHomeOrGuestTeam) return DomainResult.Error(new PlayerWasNotPartOfTheTeamWhenStartingTheMatch(playerProgression.PlayerId));

            var gameResult = CreateGameResult(playerProgression);

            var matchResultUploaded = new MatchProgressed(MatchId, playerProgression, gameResult);
            return DomainResult.Ok(matchResultUploaded);
        }

        private GameResult CreateGameResult()
        {
            var homeTouchDowns = CountTouchDowns(HomeTeamProgressions);
            var guestTouchDowns = CountTouchDowns(GuestTeamProgressions);

            var homeResult = new PointsOfTeam(TeamAtHome, homeTouchDowns);
            var guestResult = new PointsOfTeam(TeamAsGuest, guestTouchDowns);

            var gameResult = GameResult.CreatGameResult(homeResult, guestResult);
            return gameResult;
        }

        private GameResult CreateGameResult(PlayerProgression playerProgression)
        {
            if (HomeTeamPlayers.Contains(playerProgression.PlayerId))
            {
                HomeTeamProgressions.Add(playerProgression);
            }
            else
            {
                GuestTeamProgressions.Add(playerProgression);
            }

            return CreateGameResult();
        }

        private static int CountTouchDowns(IEnumerable<PlayerProgression> trainerResults)
        {
            return trainerResults.Count(playerProgression =>
                playerProgression.ProgressionEvent == ProgressionEvent.PlayerMadeTouchdown);
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

        public void Apply(MatchProgressed domainEvent)
        {
            PlayerProgressions = PlayerProgressions.Append(domainEvent.PlayerProgression);
        }
    }
}