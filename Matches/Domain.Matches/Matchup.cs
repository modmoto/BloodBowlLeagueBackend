using System.Collections.Generic;
using System.Linq;
using Domain.Matches.Errors;
using Domain.Matches.Events;
using Microwave.Domain;

namespace Domain.Matches
{
    public class Matchup : Entity,
        IApply<MatchFinished>,
        IApply<MatchStarted>,
        IApply<MatchCreated>
    {
        public GuidIdentity MatchId { get; private set; }
        public IEnumerable<GuidIdentity> GuestTeamPlayers { get; private set; }
        public IEnumerable<GuidIdentity> HomeTeamPlayers { get; private set; }
        public GuidIdentity TeamAtHome { get; private set; }
        public GuidIdentity TeamAsGuest { get; private set; }
        public IEnumerable<PlayerProgression> PlayerProgressions { get; private set; }
        public bool IsFinished { get; private set; }

        public static DomainResult Create(GuidIdentity teamAtHome, GuidIdentity teamAsGuest)
        {
            var domainEvents = new MatchCreated(GuidIdentity.Create(), teamAtHome, teamAsGuest);
            return DomainResult.Ok(domainEvents);
        }

        public DomainResult Start(TeamReadModel teamAtHome, TeamReadModel teamAsGuest)
        {
            if (TeamAtHome != teamAtHome.TeamId || TeamAsGuest != teamAsGuest.TeamId) return DomainResult.Error
            (new TrainerHaveToBeTheSameAsOnGameCreation(TeamAtHome, TeamAsGuest));
            HomeTeamPlayers = teamAtHome.Players;
            GuestTeamPlayers = teamAsGuest.Players;
            return DomainResult.Ok(new MatchStarted(MatchId, HomeTeamPlayers, GuestTeamPlayers));
        }

        public DomainResult Finish(IEnumerable<PlayerProgression> playerProgressions)
        {
            if (IsFinished) return DomainResult.Error(new MatchAllreadyFinished());
            var progressions = playerProgressions.ToList();

            var homeTeamProgression = progressions.Where(p => HomeTeamPlayers.Contains(p.PlayerId));
            var guestTeamProgression = progressions.Where(p => GuestTeamPlayers.Contains(p.PlayerId));
            var unrelatedPlayers = progressions.Where(p =>
                !HomeTeamPlayers.Contains(p.PlayerId) && !GuestTeamPlayers.Contains(p.PlayerId)).Select(p => p.PlayerId).ToList();
            if (unrelatedPlayers.Any()) return DomainResult.Error(new PlayerWasNotPartOfTheTeamOnMatchCreation(unrelatedPlayers));

            var homeTouchDowns = CountTouchDowns(homeTeamProgression);
            var guestTouchDowns = CountTouchDowns(guestTeamProgression);

            var homeResult = new PointsOfTeam(TeamAtHome, homeTouchDowns);
            var guestResult = new PointsOfTeam(TeamAsGuest, guestTouchDowns);

            var gameResult = GameResult.CreatGameResult(homeResult, guestResult);

            var matchResultUploaded = new MatchFinished(MatchId, progressions, gameResult);
            IsFinished = true;
            return DomainResult.Ok(matchResultUploaded);
        }

        private static int CountTouchDowns(IEnumerable<PlayerProgression> trainerResults)
        {
            return trainerResults.Sum(playerProgression => playerProgression.ProgressionEvents.Count(ev => ev == ProgressionEvent.PlayerMadeTouchdown));
        }

        public void Apply(MatchFinished domainEvent)
        {
            IsFinished = true;
            PlayerProgressions = domainEvent.PlayerProgressions;
        }

        public void Apply(MatchStarted domainEvent)
        {
            MatchId = (GuidIdentity) domainEvent.EntityId;
            HomeTeamPlayers = domainEvent.HomeTeam;
            GuestTeamPlayers = domainEvent.GuestTeam;
        }

        public void Apply(MatchCreated domainEvent)
        {
            MatchId = (GuidIdentity) domainEvent.EntityId;
            TeamAtHome = domainEvent.TrainerAtHome;
            TeamAsGuest = domainEvent.TrainerAsGuest;
        }
    }
}